using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacementManager : MonoBehaviour
{
    [Header("Configuración del Preview")]
    public GameObject ghostPreviewPrefab; // Prefab 3D para la previsualización de la torre.
    private GameObject ghostPreviewInstance; // Instancia de la previsualización.

    [Header("Configuración del Grid")]
    public Grid grid;           // Componente Grid de la escena.
    public LayerMask gridLayer; // Capa asignada a los tiles o al plano del grid.

    private GameObject currentTowerPrefab; // Prefab real de la torre que se construirá.
    private bool isPlacing = false;

    // Input Actions (asumiendo que usas un InputActions generado por el new input system)
    private InputActions inputActions;
    private InputAction clickAction;
    private InputAction moveAction;

    void Awake()
    {
        // Inicializamos las acciones de entrada.
        inputActions = new InputActions();
        clickAction = inputActions.UI.Click;
        moveAction = inputActions.UI.Point;

        clickAction.performed += ctx => OnClick();
        moveAction.performed += ctx => OnMove(ctx);
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
    }

    // Actualiza la posición del preview en el centro de la celda
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

        // Usamos la posición actual del preview para instanciar la torre real.
        if (ghostPreviewInstance != null)
        {
            Vector3 spawnPos = ghostPreviewInstance.transform.position;
            Instantiate(currentTowerPrefab, spawnPos, Quaternion.identity);
            CancelPlacement();
        }
    }

    // Inicia el modo de colocación, instanciando el preview del objeto 3D.
    public void StartPlacement(GameObject towerPrefab)
    {
        currentTowerPrefab = towerPrefab;
        isPlacing = true;

        // Instanciar la previsualización. Puedes usar el mismo prefab si quieres,
        // pero es recomendable tener un prefab ghost que tenga, por ejemplo, materiales transparentes.
        ghostPreviewInstance = Instantiate(ghostPreviewPrefab, Vector3.zero, Quaternion.identity);

        // Opcional: deshabilitar colisionadores o cambiar la capa para que no interfiera con el raycast.
        // Por ejemplo:
        // Collider col = ghostPreviewInstance.GetComponent<Collider>();
        // if(col != null) col.enabled = false;
    }

    // Cancela la colocación y elimina la previsualización.
    public void CancelPlacement()
    {
        isPlacing = false;
        if (ghostPreviewInstance != null)
        {
            Destroy(ghostPreviewInstance);
        }
    }
}
