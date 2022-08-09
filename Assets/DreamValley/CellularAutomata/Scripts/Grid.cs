using Unity.Mathematics;

namespace DreamValley.CellularAutomata
{
    public class Grid
    {
        public readonly Cell[,] Board;

        public Grid(int width, int height)
        {
            Board = new Cell[width, height];
        }

        public Grid(Cell[,] board)
        {
            Board = board.Clone() as Cell[,];
        }

        private bool IsIndexValid(int2 pos)
        {
            return pos.x >= 0 && pos.x < Board.GetLength(0) && pos.y >= 0 && pos.y < Board.GetLength(1);
        }

        public bool IsEmpty(int2 pos)
        {
            if (IsIndexValid(pos))
            {
                return Board[pos.x, pos.y] == null;
            }

            return false;
        }
    }
}