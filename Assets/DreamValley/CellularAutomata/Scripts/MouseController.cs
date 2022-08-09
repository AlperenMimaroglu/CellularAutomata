using System;
using Unity.Mathematics;
using UnityEngine;

namespace DreamValley.CellularAutomata
{
    public class MouseController : MonoBehaviour
    {
        [SerializeField] private Cell cellToSpawn;
        [SerializeField] private GridManager gridManager;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                AddCell();
            }
        }

        private void AddCell()
        {
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                gridManager.AddCellToQueue(new int2((int) hit.point.x, (int) hit.point.y), cellToSpawn);
            }
        }
    }
}