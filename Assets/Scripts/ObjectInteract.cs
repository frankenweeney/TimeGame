using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class ObjectInteract : MonoBehaviour
{
    public Camera mainCamera;

    private void Update()
    {
    }

    private void OnMouseDown()
    {
       
    }

    private void OnMouseDrag()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;
        transform.position = mouseWorldPos;
    }

}
