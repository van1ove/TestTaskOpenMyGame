using System.Collections.Generic;
using App.Scripts.Scenes.SceneChess.Features.ChessField.GridMatrix;
using UnityEngine;

namespace App.Scripts.Scenes.SceneChess.Features.GridNavigation.Navigator
{
    public class UnitNavigator
    {
        public static List<Vector2Int> PonNavigate(int[][] cellDistances, bool[][] visited, Vector2Int[][] previous, 
            ChessGrid grid, Vector2Int from, Vector2Int to)
        {
            List<Vector2Int> points = new List<Vector2Int>();

            Vector2Int ponMove = new Vector2Int(0, 1);

            // start of bfs
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(from);

            while (queue.Count > 0)
            {
                Vector2Int currentCell = queue.Dequeue();
                Vector2Int pos = new Vector2Int(currentCell.x + ponMove.x, currentCell.y + ponMove.y);
                
                if (IsValid(pos, grid.Size) && !visited[pos.x][pos.y])
                {
                    if (grid.Get(pos) != null) 
                        continue;
                    
                    queue.Enqueue(pos);
                    visited[pos.x][pos.y] = true;
                    cellDistances[pos.x][pos.y] = cellDistances[currentCell.x][currentCell.y] + 1;
                    previous[pos.x][pos.y] = currentCell;
                }

            }
            
            if (cellDistances[to.x][to.y] == -1)
                return null;
            
            // path recovery
            points.Add(to);
            Vector2Int current = to;
            while (previous[current.x][current.y] != previous[from.x][from.y])
            {
                current = previous[current.x][current.y];
                if (current == from) 
                    continue;
                
                points.Add(current);
            }

            points.Reverse();
            return points;
        }
        
        public static List<Vector2Int> KingNavigate(int[][] cellDistances, bool[][] visited, Vector2Int[][] previous, 
            ChessGrid grid, Vector2Int from, Vector2Int to)
        {
            List<Vector2Int> points = new List<Vector2Int>();
            
            int[] row = { 1, 1, 1, -1, -1, -1, 0, 0};
            int[] col = { -1, 0, 1, -1, 0, 1, 1, -1};

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
                        visited[pos.x][pos.y] = true;
                        cellDistances[pos.x][pos.y] = cellDistances[currentCell.x][currentCell.y] + 1;
                        previous[pos.x][pos.y] = currentCell;
                    }
                }
                
            }
            
            if (cellDistances[to.x][to.y] == -1)
                return null;
            
            // path recovery
            points.Add(to);
            Vector2Int current = to;
            while (previous[current.x][current.y] != previous[from.x][from.y])
            {
                current = previous[current.x][current.y];
                if (current == from) 
                    continue;
                
                points.Add(current);
            }
            
            points.Reverse();
            return points;
        }
        
        public static List<Vector2Int> QueenNavigate(int[][] cellDistances, bool[][] visited, Vector2Int[][] previous, 
            ChessGrid grid, Vector2Int from, Vector2Int to)
        {
            List<Vector2Int> points = new List<Vector2Int>();
            
            int[] row = { 1, 1, 1, -1, -1, -1, 0, 0};
            int[] col = { -1, 0, 1, -1, 0, 1, 1, -1};

            // start of bfs
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(from);

            while (queue.Count > 0)
            {
                Vector2Int currentCell = queue.Dequeue();
                for (int i = 0; i < row.Length; i++)
                {
                    for (int shift = 1; shift < grid.Size.x; shift++)
                    {
                        Vector2Int shiftedPos = ShiftPosition(row[i], col[i], shift);
                        Vector2Int pos = new Vector2Int(currentCell.x + shiftedPos.x, currentCell.y + shiftedPos.y);

                        if (IsValid(pos, grid.Size) && !visited[pos.x][pos.y])
                        {
                            if (grid.Get(pos) != null) 
                                break;
                        
                            queue.Enqueue(pos);
                            visited[pos.x][pos.y] = true;
                            cellDistances[pos.x][pos.y] = cellDistances[currentCell.x][currentCell.y] + 1;
                            previous[pos.x][pos.y] = currentCell;
                        }
                    }
                }
                
            }
            
            if (cellDistances[to.x][to.y] == -1)
                return null;
            
            // path recovery
            points.Add(to);
            Vector2Int current = to;
            while (previous[current.x][current.y] != previous[from.x][from.y])
            {
                current = previous[current.x][current.y];
                if (current == from) 
                    continue;
                
                points.Add(current);
            }

            points.Reverse();
            return points;
        }
        
        public static List<Vector2Int> RookNavigate(int[][] cellDistances, bool[][] visited, Vector2Int[][] previous, 
            ChessGrid grid, Vector2Int from, Vector2Int to)
        {
            List<Vector2Int> points = new List<Vector2Int>();
            
            int[] row = { 1, 0, -1, 0 };
            int[] col = { 0, 1, 0, -1 };

            // start of bfs
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(from);

            while (queue.Count > 0)
            {
                Vector2Int currentCell = queue.Dequeue();
                for (int i = 0; i < row.Length; i++)
                {
                    for (int shift = 1; shift < grid.Size.x; shift++)
                    {
                        Vector2Int shiftedPos = ShiftPosition(row[i], col[i], shift);
                        Vector2Int pos = new Vector2Int(currentCell.x + shiftedPos.x, currentCell.y + shiftedPos.y);

                        if (IsValid(pos, grid.Size) && !visited[pos.x][pos.y])
                        {
                            if (grid.Get(pos) != null) 
                                break;
                        
                            queue.Enqueue(pos);
                            visited[pos.x][pos.y] = true;
                            cellDistances[pos.x][pos.y] = cellDistances[currentCell.x][currentCell.y] + 1;
                            previous[pos.x][pos.y] = currentCell;
                        }
                    }
                }
                
            }
            
            if (cellDistances[to.x][to.y] == -1)
                return null;
            
            // path recovery
            points.Add(to);
            Vector2Int current = to;
            while (previous[current.x][current.y] != previous[from.x][from.y])
            {
                current = previous[current.x][current.y];
                if (current == from) 
                    continue;
                
                points.Add(current);
            }

            points.Reverse();
            return points;
        }
        
        public static List<Vector2Int> KnightNavigate(int[][] cellDistances, bool[][] visited, Vector2Int[][] previous, 
            ChessGrid grid, Vector2Int from, Vector2Int to)
        {
            List<Vector2Int> points = new List<Vector2Int>();
            
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
                        visited[pos.x][pos.y] = true;
                        cellDistances[pos.x][pos.y] = cellDistances[currentCell.x][currentCell.y] + 1;
                        previous[pos.x][pos.y] = currentCell;
                    }
                }
                
            }
            
            if (cellDistances[to.x][to.y] == -1)
                return null;
            
            // path recovery
            points.Add(to);
            Vector2Int current = to;
            while (previous[current.x][current.y] != previous[from.x][from.y])
            {
                current = previous[current.x][current.y];
                if (current == from) 
                    continue;
                
                points.Add(current);
            }

            points.Reverse();
            return points;
        }

        public static List<Vector2Int> BishopNavigate(int[][] cellDistances, bool[][] visited, Vector2Int[][] previous, 
            ChessGrid grid, Vector2Int from, Vector2Int to)
        {
            List<Vector2Int> points = new List<Vector2Int>();
            
            int[] row = { 1, 1, -1, -1 };
            int[] col = { 1, -1, 1, -1 };

            // start of bfs
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(from);

            while (queue.Count > 0)
            {
                Vector2Int currentCell = queue.Dequeue();
                for (int i = 0; i < row.Length; i++)
                {
                    for (int shift = 1; shift < grid.Size.x; shift++)
                    {
                        Vector2Int shiftedPos = ShiftPosition(row[i], col[i], shift);
                        Vector2Int pos = new Vector2Int(currentCell.x + shiftedPos.x, currentCell.y + shiftedPos.y);

                        if (IsValid(pos, grid.Size) && !visited[pos.x][pos.y])
                        {
                            if (grid.Get(pos) != null) 
                                break;
                        
                            queue.Enqueue(pos);
                            visited[pos.x][pos.y] = true;
                            cellDistances[pos.x][pos.y] = cellDistances[currentCell.x][currentCell.y] + 1;
                            previous[pos.x][pos.y] = currentCell;
                        }
                    }
                }
                
            }
            
            if (cellDistances[to.x][to.y] == -1)
                return null;
            
            // path recovery
            points.Add(to);
            Vector2Int current = to;
            while (previous[current.x][current.y] != previous[from.x][from.y])
            {
                current = previous[current.x][current.y];
                if (current == from) 
                    continue;
                
                points.Add(current);
            }

            points.Reverse();
            return points;
        }
        
        private static Vector2Int ShiftPosition(int x, int y, int shift)
        {
            return new Vector2Int(x * shift, y * shift);
        }
        
        private static bool IsValid(Vector2Int position, Vector2Int gridSize)
        {
            return position.x >= 0 && position.x < gridSize.x && position.y >= 0 && position.y < gridSize.y;
        }
    }
}