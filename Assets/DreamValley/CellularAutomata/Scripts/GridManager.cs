using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamValley.CellularAutomata
{
    using Unity.Mathematics;

    [RequireComponent(typeof(BoxCollider2D))]
    public class GridManager : MonoBehaviour
    {
        public int2 BoardSize => boardSize;

        [SerializeField] private int2 boardSize = new(256, 256);
        [SerializeField] private float iterationTime;

        private List<Cell> _activeCells;
        private WaitForSeconds _waitForSeconds;
        private readonly Dictionary<int2, Cell> _cellsToAdd = new();
        private Grid _grid;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(iterationTime);

            _activeCells = new List<Cell>(boardSize.x * boardSize.y);

            var col = GetComponent<BoxCollider2D>();
            col.offset = new Vector2(boardSize.x * .5f, boardSize.y * 0.5f);
            col.size = new Vector2(boardSize.x, boardSize.y);
            GenerateGrid();
        }

        private void Start()
        {
            StartCoroutine(Step());
        }

        private void GenerateGrid()
        {
            _grid = new Grid(boardSize.x, boardSize.y);
        }

        public void AddCellToQueue(int2 pos, Cell cell)
        {
            if (!_cellsToAdd.ContainsKey(pos))
            {
                _cellsToAdd.Add(pos, cell);
            }
        }

        private IEnumerator Step()
        {
            while (true)
            {
                var tempGrid = new Grid(_grid.Board);

                foreach (var activeCell in _activeCells)
                {
                    var oldPos = activeCell.position;
                    var step = activeCell.Step(tempGrid);
                    tempGrid.Board[oldPos.x, oldPos.y] = null;

                    activeCell.position = step;
                    tempGrid.Board[step.x, step.y] = activeCell;
                }

                for (int i = 0; i < tempGrid.Board.GetLength(0); i++)
                {
                    for (int j = 0; j < tempGrid.Board.GetLength(1); j++)
                    {
                        _grid.Board[i, j] = tempGrid.Board[i, j];
                        if (_grid.Board[i, j] != null)
                        {
                            _grid.Board[i, j].transform.position = new Vector3(i, j, 0);
                        }
                    }
                }

                foreach (var cell in _cellsToAdd)
                {
                    if (_grid.Board[cell.Key.x, cell.Key.y] != null)
                    {
                        continue;
                    }

                    var cellInstance = Instantiate(cell.Value, new Vector3(cell.Key.x, cell.Key.y, 0),
                        quaternion.identity, transform);

                    cellInstance.position = cell.Key;
                    _activeCells.Add(cellInstance);
                    _grid.Board[cellInstance.position.x, cellInstance.position.y] = cellInstance;
                }

                _cellsToAdd.Clear();

                yield return _waitForSeconds;
            }
        }
    }
}