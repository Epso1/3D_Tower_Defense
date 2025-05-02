using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minZoom = 0.5f;
    [SerializeField] private float maxZoom = 5f;
    [SerializeField] private float zoomSmoothFactor = 5f;  // Factor de suavizado para la interpolación

    [Header("Movimiento")]
    [SerializeField] private float panSpeed = 20f;
    [SerializeField] private float panBorderThickness = 10f;

    private Camera cam;
    private float targetZoom;  // Zoom objetivo

    // Asset de InputActions
    private InputActions inputActions;
    private InputAction scrollAction;
    private InputAction pointAction;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
            cam = Camera.main;

        // Inicializar targetZoom con el valor inicial de la cámara
        targetZoom = cam.orthographicSize;

        // Inicializar InputActions y las acciones
        inputActions = new InputActions();
        scrollAction = inputActions.UI.ScrollWheel;
        pointAction = inputActions.UI.Point;
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
    }


    void Update()
    {

        /// ZOOM CÁMARA
        // Leer scroll input y normalizar
        Vector2 scrollInputNormalized = scrollAction.ReadValue<Vector2>().normalized;
        // Leer scroll input y normalizar dividiendo por 120 (ticks)
        //float scrollInput = scrollAction.ReadValue<Vector2>().y / 120f;
        
        float scrollInput = scrollInputNormalized.y * 10f;

        if (Mathf.Abs(scrollInput) > 0)
        {
            // Actualizar targetZoom basándonos en scrollInput
            targetZoom -= scrollInput * zoomSpeed * Time.deltaTime;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }

        // Interpolar suavemente hacia el targetZoom
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSmoothFactor * Time.deltaTime);


        /// MOVIMIENTO CÁMARA        
        // Movimiento isométrico basado en la posición del puntero
        Vector2 pointerPosition = pointAction.ReadValue<Vector2>();

        Vector3 camRight = transform.right;
        Debug.Log("camRight = " + camRight);
        camRight.y = 0;
        Debug.Log("camRight.y = " + camRight.y);
        camRight.Normalize();
        Debug.Log("camRight Normalized = " + camRight);

        Vector3 camForward = transform.forward;
        Debug.Log("camForward = " + camForward);
        camForward.y = 0;
        Debug.Log("camForward.y = " + camForward.y);
        camForward.Normalize();
        Debug.Log("camForward Normalized = " + camForward);

        Vector3 moveDir = Vector3.zero;

        if (pointerPosition.x >= Screen.width - panBorderThickness)
            moveDir += camRight;
        if (pointerPosition.x <= panBorderThickness)
            moveDir -= camRight;
        if (pointerPosition.y >= Screen.height - panBorderThickness)
            moveDir += camForward;
        if (pointerPosition.y <= panBorderThickness)
            moveDir -= camForward;

        Vector3 pos = transform.position;
        pos += moveDir * panSpeed * Time.deltaTime;
        
        transform.position = pos;
    }
}
