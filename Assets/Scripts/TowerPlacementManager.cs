using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacementManager : MonoBehaviour
{
    [Header("Configuración del Preview")]
    [SerializeField] GameObject ghostPreviewPrefab; // Prefab 3D para la previsualización de la torre.
    private GameObject ghostPreviewInstance; // Instancia de la previsualización.

    [Header("Configuración del Grid")]
    [SerializeField] Grid grid;           // Componente Grid de la escena.
    [SerializeField] LayerMask gridLayer; // Capa asignada al plano del grid.
    [SerializeField] LayerMask cellsLayer; // Capa asignada a las celdas.
    [SerializeField] GameObject cells; // referencia al objeto que contiene las celdas

    private GameObject currentTowerPrefab; // Prefab real de la torre que se construirá.
    private bool isPlacing = false;

    // Input Actions
    private InputActions inputActions;
    private InputAction clickAction;
    private InputAction moveAction;

    void Awake()
    {
        cells.SetActive(false); // Ocultar celdas

        // Inicializar las acciones de entrada.
        inputActions = new InputActions();
        clickAction = inputActions.UI.Click;
        moveAction = inputActions.UI.Point;

        clickAction.performed += ctx => OnClick();// Suscribirse al evento Click.
        moveAction.performed += ctx => OnMove(ctx); // Suscribirse al evento Move.
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
    }

    // Actualiza la posición del preview en el centro de la celda.
    private void OnMove(InputAction.CallbackContext context)
    {
        if (!isPlacing) return;

        Vector2 mousePos = context.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gridLayer))
        {
            Vector3Int cellPos = grid.WorldToCell(hit.point);
            Vector3 cellCenter = grid.GetCellCenterWorld(cellPos);
            // Fijamos Y a 0.225f, como lo deseamos.
            Vector3 previewPos = new Vector3(cellCenter.x, 0.225f, cellCenter.z);
            if (ghostPreviewInstance != null)
            {
                ghostPreviewInstance.transform.position = previewPos;
            }
        }
    }

    // Coloca la torre real en la posición del preview al hacer clic.
    private void OnClick()
    {
        if (!isPlacing) return;
        Debug.Log("OnClick disparado");

        Vector2 mousePos = moveAction.ReadValue<Vector2>(); // Usamos la acción Point.
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cellsLayer))
        {
            // Obtener el componente Cell del objeto impactado.
            Cell cell = hit.collider.GetComponent<Cell>();         

            // Comprobar si el cell ya tiene una torre.
            if (cell.isOccupied)
            {
                Debug.Log("Este cell ya está ocupado.");
                return;
            }

            // Obtener la posición central del cell (por ejemplo, usando su collider o calculándola desde el Grid).
            Vector3Int cellPos = grid.WorldToCell(hit.point);
            Vector3 cellCenter = grid.GetCellCenterWorld(cellPos);
            // Ajustamos la posición en Y.
            Vector3 spawnPos = new Vector3(cellCenter.x, 0.225f, cellCenter.z);
            // Instanciar la torre real.
            GameObject newTower = Instantiate(currentTowerPrefab, spawnPos, Quaternion.identity);

            // Marcar el cell como ocupado y actualizar su material.
            cell.isOccupied = true;
            cell.UpdateCellMaterial();

            CancelPlacement();
        }
    }


    // Inicia el modo de colocación, instanciando el preview del objeto 3D.
    public void StartPlacement(GameObject towerPrefab)
    {
        currentTowerPrefab = towerPrefab;
        isPlacing = true;
        cells.SetActive(true); // Mostrar celdas

        // Instanciar prefab ghost.
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            ghostPreviewInstance = Instantiate(ghostPreviewPrefab, hit.point, Quaternion.identity);
        }

    }

    // Cancela la colocación y elimina la previsualización.
    public void CancelPlacement()
    {
        cells.SetActive(false);
        isPlacing = false;
        if (ghostPreviewInstance != null)
        {
            Destroy(ghostPreviewInstance);
        }
    }
}
