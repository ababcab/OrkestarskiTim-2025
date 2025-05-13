using UnityEngine;

public class CameraBob : MonoBehaviour
{
    public float yawAngle = 2.5f;      // lijevo-desno
    public float pitchAngle = 1f;    // gore-dolje
    public float swaySpeed = 0.5f;

    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        float yaw = Mathf.Sin(Time.time * swaySpeed) * yawAngle;
        float pitch = Mathf.Cos(Time.time * swaySpeed * 0.8f) * pitchAngle;

        transform.rotation = originalRotation * Quaternion.Euler(pitch, yaw, 0f);
    }
}
