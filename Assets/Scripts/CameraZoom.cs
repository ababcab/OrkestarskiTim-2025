using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    [Range(0,1f)]
    private float dolleyZoomCoef = 1f;

    float minFov = 45f;
    float maxFov = 63f;
    float sens = 10f;

    private Transform cam;
    private Vector3 defaultPosition;
    private void Start()
    {
        cam = Camera.main.transform;
        defaultPosition = cam.position;
    }

    private void Update()
    {
        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sens;
        fov = Mathf.Clamp(fov, minFov, maxFov);

        float fovDifference = maxFov - fov;
        Vector3 forwardOffset = (-1) * cam.transform.forward * fovDifference;

        Camera.main.fieldOfView = fov;
        Camera.main.transform.position = defaultPosition + forwardOffset * dolleyZoomCoef;
    }
}
