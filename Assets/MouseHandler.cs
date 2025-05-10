using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MouseHandler : MonoBehaviour, IPointerDownHandler
{
    Vector3 axis_x = Vector3.right;
    Vector3 axis_y = Vector3.up;

    [SerializeField]
    private GameObject prefab;

    public void OnPointerDown(PointerEventData eventData)
    {
        float x = Vector3.Dot(eventData.pointerPressRaycast.worldPosition, axis_x.normalized);
        float y = Vector3.Dot(eventData.pointerPressRaycast.worldPosition, axis_y.normalized);
        //Vector3 position = ((int)x / 10) * axis_x * 10 + ((int)y / 10) * axis_y * 10;
        // Vector3 position = x * axis_x + y * axis_y;
        Vector3 position = eventData.pointerCurrentRaycast.worldPosition;
        //position.z = eventData.pointerCurrentRaycast.gameObject.transform.position.z;
       // position.z=eventData.pointerPressRaycast.worldPosition.z;
        Instantiate(prefab, position, Quaternion.identity);
        //eventData.pointerCurrentRaycast.worldPosition;
        // Debug.Log($"cursor world pos: {eventData.pointerPressRaycast.worldPosition} {x} {y} {position}");
        Debug.Log($"cursor world pos:  {x} {y} {position}");
    }

    private void Update()
    {
        //Debug.Log($" {Mouse.current.position.value}");
    }
}
