using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using UnityEngine;

namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class ProviderFillwordLevel : IProviderFillwordLevel
    {
        public GridFillWords LoadModel(int index)
        {
            //напиши реализацию не меняя сигнатуру функции
            GridFillWords model = null;
            try
            {
                if (!LevelSaver.TryInit(index))
                {
                    if (LevelSaver.IsStartLevel(index) && !LevelSaver.AnyLevelWasLoaded)
                        throw new Exception("Can't load any level");
                }
                
                string coordinatesPath = "Assets/App/Resources/FillWords/pack_0.txt";
                Dictionary<int, int[]> letterPoints = ParseCoordinates(index, coordinatesPath);

                model = BuildGridFillWords(letterPoints, out float totalTileAmount);

                string wordsPath = "Assets/App/Resources/FillWords/words_list.txt";
                model = FillTiles(model, letterPoints, wordsPath, (int)totalTileAmount);

                LevelSaver.AnyLevelWasLoaded = true;
                
                return model;
            }
            catch (ArgumentException e)
            {
                Debug.Log(e.Message);
                return LoadModel(index + 1);
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.Log(e.Message);
                return LoadModel(index + 1);
            }
            catch (NullReferenceException e)
            {
                Debug.Log(e.Message);
                index = 1;
                return LoadModel(index);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);

                // This code is for case, if something might be return anyway
                
                // GridFillWords emptyModel = new GridFillWords(new Vector2Int(1, 1));
                // nullModel.Set(0, 0, new CharGridModel('-'));
                // return nullModel;
            }

            return model;
        }

        private Dictionary<int, int[]> ParseCoordinates(int index, string path)
        {
            ReadLineByIndex(index, path, out string str);
            
            string[] splitBySpace = str.Split(" ");
            
            Dictionary<int, int[]> letterPoints = new Dictionary<int, int[]>();
            for (int i = 0; i < splitBySpace.Length; i += 2)
            {
                int stringNumber = Int32.Parse(splitBySpace[i]);
                
                string[] splitBySymbol = splitBySpace[i + 1].Split(";");
                int[] pointArray = new int[splitBySymbol.Length];
                
                for (int j = 0; j < splitBySymbol.Length; j++)
                {
                    pointArray[j] = Int32.Parse(splitBySymbol[j]);
                }
                
                letterPoints.Add(stringNumber, pointArray);
            }

            return letterPoints;
        }

        private void ReadLineByIndex(int index, string path, out string result)
        {
            result = "";
            if (index == 0)
            {
                result = File.ReadLines(path).Take(1).First();
                return;
            }
            
            using StreamReader reader = new StreamReader(path);
            for (int i = 0; i < index; i++)
            {
                result = reader.ReadLine();
            }
            reader.Close();

            if (result == null)
                throw new NullReferenceException("line wasn't read");
        }

        private GridFillWords BuildGridFillWords(Dictionary<int, int[]> letterPoints, out float totalTileAmount)
        {
            totalTileAmount = 0;
            foreach (var pair in letterPoints)
            {
                totalTileAmount += pair.Value.Length;
            }

            totalTileAmount = Mathf.Sqrt(totalTileAmount);
            if (GetDigitsCount(totalTileAmount) != 0)
                throw new ArgumentException("Can't make square tile");

            return new GridFillWords(new Vector2Int((int)totalTileAmount, (int)totalTileAmount));
        }
        
        private int GetDigitsCount(float number)
        {
            string[] str = number.ToString(new System.Globalization.NumberFormatInfo() { NumberDecimalSeparator = "." }).Split('.');
            return str.Length == 2 ? str[1].Length : 0;
        }

        private GridFillWords FillTiles(GridFillWords model, Dictionary<int, int[]> letterPoints, string path, int tilesInRow)
        {
            foreach (var pair in letterPoints)
            {
                ReadLineByIndex(pair.Key, path, out string str);

                if (str.Length != pair.Value.Length)
                    throw new IndexOutOfRangeException("different sizes");
                
                for (int i = 0; i < str.Length; i++)
                {
                    GetIndexes(pair.Value[i], tilesInRow, out int row, out int column);
                    CharGridModel symbol = new CharGridModel(str[i]);
                    if (model.Get(row, column) != null)
                        throw new ArgumentException("trying to rewrite tiles");
                    
                    model.Set(row, column, symbol);
                }
            }

            return model;
        }

        private void GetIndexes(int number, int tilesInRow, out int row, out int column)
        {
            row = number / tilesInRow;
            column = number % tilesInRow;
        }
    }
}