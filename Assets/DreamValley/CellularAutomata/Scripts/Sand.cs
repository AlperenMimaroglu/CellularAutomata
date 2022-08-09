namespace DreamValley.CellularAutomata
{
    using Unity.Mathematics;

    public class Sand : Cell
    {
        public override int2 Step(Grid grid)
        {
            if (grid.IsEmpty(position + new int2(0, -1)))
            {
                position += new int2(0, -1);
                return position;
            }

            if (grid.IsEmpty(position + new int2(-1, -1)))
            {
                position += new int2(-1, -1);
                return position;
            }

            if (grid.IsEmpty(position + new int2(1, -1)))
            {
                position += new int2(1, -1);
                return position;
            }

            return position;
        }
    }
}