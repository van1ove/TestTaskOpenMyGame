using System;
using System.IO;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;
using UnityEngine;

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
                TextAsset asset = Resources.Load<TextAsset>("WordSearch/Levels/" + levelIndex);
                levelInfo = JsonUtility.FromJson<LevelInfo>(asset.text);
                
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