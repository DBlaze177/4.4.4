using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("Cámara")]
    [SerializeField] private Transform cameraTransform;

    [Header("Ajustes de Movimiento")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 12f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float crouchMultiplier = 0.5f;

    private Vector3 normalScale;

    private void Awake()
    {
        normalScale = transform.localScale;

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (Keyboard.current == null) return;
        var keyboard = Keyboard.current;

        // Agacharse y Sprint
        bool isCrouching = keyboard.leftCtrlKey.isPressed;
        bool isSprinting = keyboard.leftShiftKey.isPressed;

        transform.localScale = isCrouching
            ? new Vector3(normalScale.x, normalScale.y * 0.5f, normalScale.z)
            : normalScale;

        float currentSpeed = speed;
        if (isCrouching) currentSpeed *= crouchMultiplier;
        else if (isSprinting) currentSpeed *= sprintMultiplier;

        // Teclas de dirección
        float horizontal = 0f;
        float vertical = 0f;

        if (keyboard.wKey.isPressed) vertical += 1f;
        if (keyboard.sKey.isPressed) vertical -= 1f;
        if (keyboard.aKey.isPressed) horizontal -= 1f;
        if (keyboard.dKey.isPressed) horizontal += 1f;

        Vector2 input = new Vector2(horizontal, vertical);

        if (input.sqrMagnitude > 0.001f && cameraTransform != null)
        {
            // Proyección horizontal relativa a la cámara
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = (camForward * input.y + camRight * input.x).normalized;

            // Traslación
            transform.position += moveDirection * (currentSpeed * Time.deltaTime);

            // Giro hacia la dirección del desplazamiento
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}