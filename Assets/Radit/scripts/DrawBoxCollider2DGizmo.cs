using UnityEngine;

[ExecuteInEditMode] // Allows the script to run in Edit Mode
public class DrawBoxCollider2DGizmo : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    void OnDrawGizmos()
    {
        // Get the BoxCollider2D component
        boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            // Set the Gizmo color to red
            Gizmos.color = Color.red;

            // Get the size and offset of the BoxCollider2D
            Vector2 size = boxCollider.size;
            Vector2 offset = boxCollider.offset;

            // Calculate the center of the BoxCollider2D in world space
            Vector2 center = (Vector2)transform.position + offset;

            // Draw a wireframe cube to represent the BoxCollider2D
            Gizmos.DrawWireCube(center, size);
        }
    }
}