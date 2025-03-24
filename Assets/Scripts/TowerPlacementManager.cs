using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TowerPlacementManager : MonoBehaviour
{
    [Header("Configuración del Drag")]
    public RectTransform dragImagePrefab; // Prefab que contiene la imagen de la torre para el drag.
    public Canvas canvas;                 // Canvas de la UI

    [Header("Configuración del Grid")]
    public Grid grid;                     // Componente Grid de la escena
    public LayerMask gridLayer;           // Capa asignada a los tiles o al plano del grid

    private GameObject currentTowerPrefab;
    private bool isPlacing = false;
    private RectTransform dragImageInstance; // Instancia de la imagen creada para el drag

    // Acciones de entrada para el mouse usando el new input system.
    private InputAction clickAction;
    private InputAction moveAction;

    void Awake()
    {
        // Configuramos las acciones para el clic y el movimiento del mouse.
        clickAction = new InputAction("Click", binding: "<Mouse>/leftButton");
        moveAction = new InputAction("Move", binding: "<Mouse>/position");

        clickAction.performed += ctx => OnClick();
        moveAction.performed += ctx => OnMove(ctx);
    }

    void OnEnable()
    {
        clickAction.Enable();
        moveAction.Enable();
    }

    void OnDisable()
    {
        clickAction.Disable();
        moveAction.Disable();
    }

    // Actualiza la posición de la imagen de drag, posicionándola en el centro de la celda.
    private void OnMove(InputAction.CallbackContext context)
    {
        if (!isPlacing) return;

        Vector2 mousePos = context.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gridLayer))
        {
            Vector3Int cellPos = grid.WorldToCell(hit.point);
            Vector3 cellCenter = grid.GetCellCenterWorld(cellPos);
            if (dragImageInstance != null)
            {
                // Convertir la posición del centro de celda a posición en pantalla y actualizar la instancia.
                dragImageInstance.position = Camera.main.WorldToScreenPoint(cellCenter);
            }
        }
    }

    // Maneja el clic para colocar la torre en la celda seleccionada.
    private void OnClick()
    {
        if (!isPlacing) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gridLayer))
        {
            Vector3Int cellPos = grid.WorldToCell(hit.point);
            Vector3 cellCenter = grid.GetCellCenterWorld(cellPos);
            // Fijamos Y = 0.225f al instanciar la torre.
            Vector3 spawnPos = new Vector3(cellCenter.x, 0.225f, cellCenter.z);
            Instantiate(currentTowerPrefab, spawnPos, Quaternion.identity);
            CancelPlacement();
        }
    }

    // Se llama al pulsar "comprar" en la UI.
    // Este método crea una instancia de la imagen de drag a partir del prefab.
    public void StartPlacement(GameObject towerPrefab)
    {
        currentTowerPrefab = towerPrefab;
        isPlacing = true;
        // Instanciar una copia de la imagen para el drag en el mismo padre que el prefab original.
        dragImageInstance = Instantiate(dragImagePrefab, dragImagePrefab.parent);
        dragImageInstance.gameObject.SetActive(true);
    }

    // Cancela el modo de colocación y destruye la imagen creada.
    public void CancelPlacement()
    {
        isPlacing = false;
        if (dragImageInstance != null)
        {
            Destroy(dragImageInstance.gameObject);
        }
    }
}
