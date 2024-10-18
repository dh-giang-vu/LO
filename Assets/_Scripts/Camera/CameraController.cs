using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Camera offset and sensitivity
    public Vector3 offset = new Vector3(0, 3, -6);
    public float yawSensitivity = 100f;
    public float pitchSensitivity = 80f;
    public float zoomSensitivity = 2f;

    // Vertical rotation limits (rotation about the x-axis)
    public float minPitch = -40f;
    public float maxPitch = 75f;

    // Zoom limits
    public float minZoom = 2f;
    public float maxZoom = 10f;

    // Current camera states
    private float yaw = 0f;
    private float pitch = 0f;
    private float currentZoom;

    private void Start()
    {
        currentZoom = offset.magnitude;
    }

    private void LateUpdate()
    {
        HandleRotation();
        HandleZoom();
        UpdateCameraPosition();
    }

    void HandleRotation()
    {
        // Only handle rotation when player is holding Right Mouse Button down
        if (Input.GetMouseButton(1))
        {
            // Adjust yaw (horizontal rotation about the y-axis) based on mouse X input
            yaw += Input.GetAxis("Mouse X") * yawSensitivity * Time.deltaTime;

            // Adjust pitch (vertical rotation about the x-axis) based on mouse Y input
            pitch -= Input.GetAxis("Mouse Y") * pitchSensitivity * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
    }

    void HandleZoom()
    {
        // Adjust zoom based on mouse scroll wheel input
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    void UpdateCameraPosition()
    {
        // Calculate the new position and rotation of the camera based on yaw, pitch, and zoom
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0); 
        Vector3 newPosition = player.position + rotation * offset.normalized * currentZoom;

        // Set camera position + points towards player
        transform.position = newPosition;
        transform.LookAt(player.position + Vector3.up * 1.5f);  // look slightly abover player's "feet"
    }
}
