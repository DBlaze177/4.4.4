using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Objetivo")]
    [SerializeField] private Transform target;

    [Header("Distancia y Altura")]
    [SerializeField] private float distance = 5f;
    [SerializeField] private float height = 2f;

    [Header("Sensibilidad del Ratón")]
    [SerializeField] private float mouseSensitivity = 0.15f;
    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 70f;

    private float yaw;   // Rotación horizontal (eje Y)
    private float pitch; // Rotación vertical (eje X)

    private void Start()
    {
        // Bloquear y ocultar el cursor para jugar cómodamente
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Leer movimiento del ratón con el nuevo Input System
        if (Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            yaw += mouseDelta.x * mouseSensitivity;
            pitch -= mouseDelta.y * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        // Calcular posición orbital alrededor del target
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetFocusPosition = target.position + Vector3.up * height;
        Vector3 desiredPosition = targetFocusPosition - (rotation * Vector3.forward * distance);

        transform.rotation = rotation;
        transform.position = desiredPosition;
    }
}