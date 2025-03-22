using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minZoom = 0.5f;
    [SerializeField] private float maxZoom = 5f;

    [Header("Movimiento")]
    [SerializeField] private float panSpeed = 20f;
    [SerializeField] private float panBorderThickness = 10f;

    // Límites fijos del escenario (puedes ajustarlos en el Inspector)
    [Header("Límites del Nivel")]
    [SerializeField] private float levelMinX = -20f;
    [SerializeField] private float levelMaxX = 20f;
    [SerializeField] private float levelMinZ = -20f;
    [SerializeField] private float levelMaxZ = 20f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        // Zoom con la rueda del ratón
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            cam.orthographicSize -= scrollInput * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }

        // Movimiento isométrico: usar los vectores right y forward de la cámara en el plano XZ
        Vector3 camRight = transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 camForward = transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 moveDir = Vector3.zero;

        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            moveDir += camRight;
        if (Input.mousePosition.x <= panBorderThickness)
            moveDir -= camRight;
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            moveDir += camForward;
        if (Input.mousePosition.y <= panBorderThickness)
            moveDir -= camForward;

        Vector3 pos = transform.position;
        pos += moveDir * panSpeed * Time.deltaTime;

        // Recalcula los límites de la cámara en función del tamaño ortográfico actual
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.orthographicSize * cam.aspect;

        pos.x = Mathf.Clamp(pos.x, levelMinX + halfWidth, levelMaxX - halfWidth);
        pos.z = Mathf.Clamp(pos.z, levelMinZ + halfHeight, levelMaxZ - halfHeight);

        transform.position = pos;
    }
}
