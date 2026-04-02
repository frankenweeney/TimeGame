using UnityEngine;
using UnityEngine.UIElements;

public class ClickAndDrag : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 offset;
    private float zCoord;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        

    }

    void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        rb.MovePosition(GetMouseWorldPosition() + offset);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord; // Maintain object's Z position
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


}