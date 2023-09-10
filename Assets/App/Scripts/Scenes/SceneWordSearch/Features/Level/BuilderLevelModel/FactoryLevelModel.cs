using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Libs.Factory;
using App.Scripts.Scenes.SceneWordSearch.Features.Level.Models.Level;

namespace App.Scripts.Scenes.SceneWordSearch.Features.Level.BuilderLevelModel
{
    public class FactoryLevelModel : IFactory<LevelModel, LevelInfo, int>
    {
        public LevelModel Create(LevelInfo value, int levelNumber)
        {
            var model = new LevelModel();

            model.LevelNumber = levelNumber;

            model.Words = value.words;
            model.InputChars = BuildListChars(value.words);

            return model;
        }

        private List<char> BuildListChars(List<string> words)
        {
            //напиши реализацию не меняя сигнатуру функции
            Dictionary<char, int> lettersDictionary = new Dictionary<char, int>();
            //HashSet<char> tableOfLetter = new HashSet<char>();
            foreach (string word in words)
            {
                TakeWordApart(lettersDictionary, word);
                //TakeWordApart(tableOfLetter, word);
            }

            return ConvertToList(lettersDictionary);
            //return tableOfLetter.ToList();
        }

        // private void TakeWordApart(HashSet<char> table, string word)
        // {
        //     foreach (char letter in word)
        //     {
        //         
        //         if (table.Contains(letter))
        //             continue;
        //
        //         table.Add(letter);
        //     }
        // }
        
        private void TakeWordApart(Dictionary<char, int> table, string word)
        {
            foreach (char letter in word)
            {
                int frequency = word.Count(f => f == letter);

                if (table.TryGetValue(letter, out int value))
                {
                    if (value >= frequency) continue;

                    table[letter] = frequency;
                }
                else
                {
                    table.Add(letter, 1);
                }
            }
        }

        private List<char> ConvertToList(Dictionary<char, int> dictionary)
        {
            List<char> list = new List<char>();
            foreach (var pair in dictionary)
            {
                for (int count = 0; count < pair.Value; count++)
                {
                    list.Add(pair.Key);
                }
            }

            return list;
        }
    }
}