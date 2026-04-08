using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class RadioScript : MonoBehaviour
{
    public float rotationSpeed = 5f; // Adjust sensitivity
    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void OnMouseDown()
    {
        // Start dragging when the object is clicked
        isDragging = true;
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        // Stop dragging when mouse is released
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // Get current mouse position in world space
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate direction vectors from object to mouse positions
            Vector2 lastDir = lastMousePosition - transform.position;
            Vector2 currentDir = currentMousePosition - transform.position;

            // Calculate signed angle difference
            float angle = Vector2.SignedAngle(lastDir, currentDir);

            // Apply rotation
            transform.Rotate(Vector3.forward, angle * rotationSpeed);

            // Update last mouse position
            lastMousePosition = currentMousePosition;
        }
    }
}