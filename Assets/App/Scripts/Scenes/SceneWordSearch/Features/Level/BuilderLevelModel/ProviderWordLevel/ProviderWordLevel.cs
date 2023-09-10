using System;
using System.Collections.Generic;
using System.IO;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using UnityEngine;
using UnityEngine.WSA;
using Application = UnityEngine.Application;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel.ProviderWordLevel
{
    public class ProviderWordLevel : IProviderWordLevel
    {
        public LevelInfo LoadLevelData(int levelIndex)
        {
            //напиши реализацию не меняя сигнатуру функции
            LevelInfo levelInfo = new LevelInfo();
            try
            {
                string name = levelIndex + ".json";
                using StreamReader reader = new StreamReader("Assets/App/Resources/WordSearch/Levels/" + name);
                string json = reader.ReadToEnd();
                
                levelInfo = JsonUtility.FromJson<LevelInfo>(json);

                if (levelInfo.words.Count == 0)
                    throw new ArgumentException("Level Info is empty");
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            return levelInfo;
        }
    }
}