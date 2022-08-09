using UnityEngine;

namespace DreamValley.CellularAutomata
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GridManager gridManager;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _camera.transform.position = new Vector3(
                gridManager.BoardSize.x * .5f,
                gridManager.BoardSize.y * .5f,
                -10f
            );

            _camera.orthographicSize = (gridManager.BoardSize.x + gridManager.BoardSize.y) * .25f + 10;
        }
    }
}