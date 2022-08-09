using UnityEngine;
using Unity.Mathematics;

namespace DreamValley.CellularAutomata
{
    public abstract class Cell : MonoBehaviour
    {
        public int2 position;
        public abstract int2 Step(Grid grid);
    }
}