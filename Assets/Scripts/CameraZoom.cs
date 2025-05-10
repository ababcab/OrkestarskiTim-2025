using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    float minFov = 45f;
    float maxFov = 65f;
    float sens = 10f;

    private void Update()
    {
        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sens;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
