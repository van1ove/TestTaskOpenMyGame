using System;
using System.Collections.Generic;
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
            try
            {
                if (!LevelSaver.TryInit(index))
                {
                    if (LevelSaver.IsStartLevel(index) && !LevelSaver.AnyLevelWasLoaded)
                        throw new Exception("Can't load any level");
                }

                TextAsset asset = Resources.Load<TextAsset>("Fillwords/pack_0");
                List<string> coordinates = asset.text.Split('\r').ToList();
                
                // for removing \n in the beginning of the word
                for (int i = 1; i < coordinates.Count; i++)
                {
                    coordinates[i] = coordinates[i].Substring(1);
                }
                
                Dictionary<int, int[]> letterPoints = GridModelBuilder.ParseCoordinates(index, coordinates);

                GridFillWords model = GridModelBuilder.BuildGridFillWords(letterPoints, out float totalTileAmount);
                
                TextAsset wordsAsset = Resources.Load<TextAsset>("Fillwords/words_list");
                List<string> words = wordsAsset.text.Split('\r').ToList();
                
                // for removing \n in the beginning of the word
                for (int i = 1; i < words.Count; i++)
                {
                    words[i] = words[i].Substring(1);
                }
                
                model = GridModelBuilder.FillTiles(model, letterPoints, words, (int)totalTileAmount);

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

                // This code is for case, if something might be return anyway for game keep working
                GridFillWords emptyModel = new GridFillWords(new Vector2Int(1, 1));
                emptyModel.Set(0, 0, new CharGridModel('-'));
                return emptyModel;
            }
        }
    }
}