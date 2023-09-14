using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Vector2Int> points = new List<Vector2Int>();

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

            // knight moveset 
            int[] row = { 2, 2, -2, -2, 1, 1, -1, -1 };
            int[] col = { -1, 1, 1, -1, 2, -2, 2, -2 };

            // start of bfs
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(from);

            while (queue.Count > 0)
            {
                Vector2Int currentCell = queue.Dequeue();
                for (int i = 0; i < row.Length; i++)
                {
                    Vector2Int pos = new Vector2Int(currentCell.x + row[i], currentCell.y + col[i]);
                    if (IsValid(pos, grid.Size) && !visited[pos.x][pos.y])
                    {
                        if (grid.Get(pos) != null) 
                            continue;
                        
                        queue.Enqueue(pos);
                        visited[currentCell.x][currentCell.y] = true;
                        cellDistances[pos.x][pos.y] = cellDistances[currentCell.x][currentCell.y] + 1;
                        previous[pos.x][pos.y] = currentCell;
                    }
                }
                
            }

            Debug.Log(cellDistances[to.x][to.y]);
            
            if (cellDistances[to.x][to.y] == -1)
                return null;
            
            // path recovery
            points.Add(to);
            Vector2Int current = to;
            while (previous[current.x][current.y] != previous[from.x][from.y])
            {
                current = previous[current.x][current.y];
                points.Add(current);
                Debug.Log(points.Count);
            }

            points.Reverse();

            return points;
        }

        private bool IsValid(Vector2Int position, Vector2Int gridSize)
        {
            return position.x >= 0 && position.x < gridSize.x && position.y >= 0 && position.y < gridSize.y;
        }
    }
}