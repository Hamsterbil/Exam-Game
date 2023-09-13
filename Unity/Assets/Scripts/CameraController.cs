using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed; // Adjust the movement speed as needed

    private Vector3 dragStartPosition;
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Record the starting position of the mouse drag
            dragStartPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            // Calculate the drag distance
            Vector3 dragDelta = Input.mousePosition - dragStartPosition;

            // Calculate the camera's new position based on drag input
            Vector3 newPosition = transform.position - new Vector3(dragDelta.y, 0f, -dragDelta.x) * moveSpeed * Time.deltaTime;

            // Update the camera's position
            transform.position = newPosition;

            // Update the drag start position for the next frame
            dragStartPosition = Input.mousePosition;
        }
        // Zoom in and out with the mouse wheel
        float scroll = Mathf.Clamp(transform.position.y - Input.GetAxis("Mouse ScrollWheel") * 8 * 100f * Time.deltaTime, 10f, 60f);
        transform.position = new Vector3(transform.position.x, scroll, transform.position.z);
    }
}