using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    [Tooltip("How sensitive the mouse is")]
    public float mouseSensitivity = 300f;

    [Header("References")]
    [Tooltip("Drag your Player (body) transform here for yaw")]
    public Transform playerBody;

    [Header("Recoil Settings")]
    [Tooltip("How quickly recoil irons itself out")]
    public float recoilRecoverSpeed = 8f;

    // Internal state
    private float xRotation = 0f;           // current pitch
    private Vector2 recoil = Vector2.zero;  // x = vertical kick, y = horizontal kick

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // EARLY-OUT: if cursor unlocked (menu open), do NOTHING
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        // 1) Read raw mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 2) Smoothly recover any recoil back toward zero
        recoil = Vector2.Lerp(recoil, Vector2.zero, recoilRecoverSpeed * Time.deltaTime);

        // 3) Apply pitch (invert Y), including vertical recoil
        xRotation -= mouseY;
        xRotation -= recoil.x;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 4) Apply yaw (left/right), including horizontal recoil
        float totalYaw = mouseX + recoil.y;
        if (playerBody != null)
            playerBody.Rotate(Vector3.up * totalYaw);
        else
            Debug.LogWarning("PlayerBody not assigned in MouseLook!");
    }

    /// <summary>
    /// Call from your shooting script to add kick.
    /// </summary>
    public void AddRecoil(float verticalAngle, float horizontalAngle)
    {
        recoil.x += verticalAngle;
        recoil.y += horizontalAngle;
    }

    /// <summary>
    /// Immediately clear any recoil and reset the view to straight ahead.
    /// Call this when you open your level-up menu.
    /// </summary>
    public void ResetView()
    {
        recoil = Vector2.zero;
        xRotation = 0f;
        transform.localRotation = Quaternion.identity;
        if (playerBody != null)
            playerBody.localRotation = Quaternion.Euler(0f, playerBody.eulerAngles.y, 0f);
    }
}
