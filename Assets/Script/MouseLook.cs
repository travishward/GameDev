using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Increased sensitivity value
    public float mouseSensitivity = 300f;

    // Reference to the player body for horizontal rotation.
    // Make sure to assign this in the Inspector by dragging your Player GameObject here.
    public Transform playerBody;

    // Tracks the vertical rotation so we can clamp it.
    private float xRotation = 0f;

    void Start()
    {
        // Lock the cursor to the center of the screen for better control.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculate vertical rotation and clamp it.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate the camera for vertical movement.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Ensure horizontal rotation is applied to the player body.
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            Debug.LogWarning("PlayerBody is not assigned in MouseLook script!");
        }
    }
}
