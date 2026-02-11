using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController2D : MonoBehaviour
{

    //Camera Settings
    public float moveSpeed = 15f;     // Camera speed in units per second
    public float edgeSize = 50f;      // Distance from screen edge to start moving

    //Background
    public SpriteRenderer background; // Assign your background sprite here

    private Camera cam;

    // Internal bounds
    private float minX, maxX, minY, maxY;

    void Awake()
    {
        cam = Camera.main;
        if (background == null)
        {
            Debug.LogError("Assign the background SpriteRenderer in the inspector!");
            enabled = false;
            return;
        }

        UpdateBounds();

        // Clamp starting position inside bounds
        Vector3 pos = transform.position;
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        pos.x = Mathf.Clamp(pos.x, minX + camWidth, maxX - camWidth);
        pos.y = Mathf.Clamp(pos.y, minY + camHeight, maxY - camHeight);
        transform.position = pos;
    }

    void UpdateBounds()
    {
        Vector3 bgPos = background.transform.position;
        Vector2 bgSize = background.size;

        minX = bgPos.x - bgSize.x / 2f;
        maxX = bgPos.x + bgSize.x / 2f;
        minY = bgPos.y - bgSize.y / 2f;
        maxY = bgPos.y + bgSize.y / 2f;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 moveDelta = Vector3.zero;
        if (mousePos.x >= Screen.width - edgeSize) moveDelta.x += moveSpeed;
        if (mousePos.x <= edgeSize) moveDelta.x -= moveSpeed;
        if (mousePos.y >= Screen.height - edgeSize) moveDelta.y += moveSpeed;
        if (mousePos.y <= edgeSize) moveDelta.y -= moveSpeed;

        moveDelta *= Time.deltaTime;
        transform.position += moveDelta;

        // Then clamp
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX + camWidth, maxX - camWidth);
        pos.y = Mathf.Clamp(pos.y, minY + camHeight, maxY - camHeight);
        transform.position = pos;

    }
}





