using System;
using System.Collections.Generic;
using App.Scripts.Scenes.SceneFillwords.Features.FillwordModels;
using UnityEngine;

namespace App.Scripts.Scenes.SceneFillwords.Features.ProviderLevel
{
    public class GridModelBuilder
    {
        public static Dictionary<int, int[]> ParseCoordinates(int index, List<string> lines)
        {
            string str = GetLineByIndex(index, lines);
            
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
        public static GridFillWords BuildGridFillWords(Dictionary<int, int[]> letterPoints, out float totalTileAmount)
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
        public static GridFillWords FillTiles(GridFillWords model, Dictionary<int, int[]> letterPoints, List<string> words, int tilesInRow)
        {
            foreach (var pair in letterPoints)
            {
                string str = GetLineByIndex(pair.Key, words);
                //ReadLineByIndex(pair.Key, path, out string str);
                if (str.Length != pair.Value.Length)
                {
                    //Debug.Log(str + " .... " + str.Length + "    :    " + pair.Value.Length);
                    throw new IndexOutOfRangeException("different sizes");
                }
                    
                
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
        private static int GetDigitsCount(float number)
        {
            string[] str = number.ToString(new System.Globalization.NumberFormatInfo() { NumberDecimalSeparator = "." }).Split('.');
            return str.Length == 2 ? str[1].Length : 0;
        }
        private static string GetLineByIndex(int index, List<string> lines)
        {
            if (index > lines.Count)
                throw new NullReferenceException("line by this index doesn't exists");
            return index == 0 ? lines[index] : lines[index - 1];
        }
        private static void GetIndexes(int number, int tilesInRow, out int row, out int column)
        {
            row = number / tilesInRow;
            column = number % tilesInRow;
        }
    }
}