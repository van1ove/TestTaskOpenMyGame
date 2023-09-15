using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using App.Scripts.Scenes.SceneChess.Features.ChessField.Types;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    public class ChessGridNavigator : IChessGridNavigator
    {
        public List<Vector2Int> FindPath(ChessUnitType unit, Vector2Int from, Vector2Int to, ChessGrid grid)
        {
            //напиши реализацию не меняя сигнатуру функции
            
            // structures initialization
            int[][] cellDistances = new int[grid.Size.x][];
            for (int i = 0; i < cellDistances.Length; i++)
            {
                cellDistances[i] = new int[grid.Size.y];
                for (int j = 0; j < cellDistances[i].Length; j++)
                {
                    cellDistances[i][j] = -1;
                }
            }
            cellDistances[from.x][from.y] = 0;

            bool[][] visited = new bool[grid.Size.x][];
            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = new bool[grid.Size.y];
                for (int j = 0; j < visited[i].Length; j++)
                {
                    visited[i][j] = false;
                }
            }
            visited[from.x][from.y] = true;
            
            Vector2Int[][] previous = new Vector2Int[grid.Size.x][];
            for (int i = 0; i < previous.Length; i++)
            {
                previous[i] = new Vector2Int[grid.Size.y];
                for (int j = 0; j < previous[i].Length; j++)
                {
                    previous[i][j] = new Vector2Int(-10, -10);
                }
            }
            previous[from.x][from.y] = new Vector2Int(-1, -1);

            return unit switch
            {
                ChessUnitType.Pon => UnitNavigator.PonNavigate(cellDistances, visited, previous, grid, from, to),
                ChessUnitType.King => UnitNavigator.KingNavigate(cellDistances, visited, previous, grid, from, to),
                ChessUnitType.Queen => UnitNavigator.QueenNavigate(cellDistances, visited, previous, grid, from, to),
                ChessUnitType.Rook => UnitNavigator.RookNavigate(cellDistances, visited, previous, grid, from, to),
                ChessUnitType.Knight => UnitNavigator.KnightNavigate(cellDistances, visited, previous, grid, from, to),
                ChessUnitType.Bishop => UnitNavigator.BishopNavigate(cellDistances, visited, previous, grid, from, to),
                _ => new List<Vector2Int> { from }
            };
        }
    }
}