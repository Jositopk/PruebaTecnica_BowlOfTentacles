
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class FPSController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerCamera;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Camera")]
    [SerializeField] private float mouseSensitivity = 0.15f;
    [SerializeField] private float maxLookAngle = 80f;

    private CharacterController controller;
    private PlayerInput playerInput;

    private InputAction moveAction;
    private InputAction lookAction;

    private float verticalSpeed;
    private float cameraPitch;

    private void Awake()
    {
        // Obtenemos los componentes del jugador.
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        //buscamos acciones principales de movimiento del Playerinput
        moveAction = playerInput.actions.FindAction("Move", true);
        lookAction = playerInput.actions.FindAction("Look", true);
    }

    private void Start()
    {
        //bloquear cursor para que no se vea ni se mueva en pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
    }

    private void HandleMovement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        //Calculo de la direccion
        Vector3 direction =
            transform.right * input.x +
            transform.forward * input.y;

        direction = Vector3.ClampMagnitude(direction, 1f);

        //Calculo de velociada vertical para caidas
        if (controller.isGrounded && verticalSpeed < 0f)
            verticalSpeed = -2f;

        verticalSpeed += gravity * Time.deltaTime;

        //Calculo velociad en general para movimientos.
        Vector3 velocity =
            direction * moveSpeed +
            Vector3.up * verticalSpeed;

        //mover el personaje
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleLook()
    {
        //Lectura movimiento de raton
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>();

        // Rotacion horizontal: cuerpo del jugador
        transform.Rotate(
            Vector3.up * mouseDelta.x * mouseSensitivity
        );

        // Rotacion vertical: solo camara
        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(
            cameraPitch, -maxLookAngle, maxLookAngle
        );

        playerCamera.localRotation =
            Quaternion.Euler(cameraPitch, 0f, 0f);
    }
}
