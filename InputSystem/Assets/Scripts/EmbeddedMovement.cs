using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmbeddedMovement : MonoBehaviour
{
    public InputAction jumpAction;
    public InputAction moveAction;
    public InputAction mouseRotationAction;
    public InputAction gamepadRotationAction;

    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector3 aimPosition;
    private float zRotation;

    private bool exampleBool;

    private void OnEnable()
    {
        jumpAction.performed += Jump;
        jumpAction.canceled += StopJump;
        jumpAction.Enable();

        moveAction.performed += Move;
        moveAction.canceled += StopMove;
        moveAction.Enable();

        mouseRotationAction.performed += MouseRotate;
        mouseRotationAction.Enable();

        gamepadRotationAction.performed += GamepadRotate;
        gamepadRotationAction.Enable();
    }

    private void OnDisable()
    {
        jumpAction.performed -= Jump;
        jumpAction.canceled -= StopJump;
        jumpAction.Disable();

        moveAction.performed -= Move;
        moveAction.canceled -= StopMove;
        moveAction.Disable();

        mouseRotationAction.performed -= MouseRotate;
        mouseRotationAction.Disable();

        gamepadRotationAction.performed -= GamepadRotate;
        gamepadRotationAction.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     //direction = moveAction.ReadValue<Vector2>().normalized;
    // }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //exampleBool = true;
        exampleBool = context.ReadValueAsButton();
        Debug.Log($"Salto accionado, la booleana es: {exampleBool}");
    }

    private void StopJump(InputAction.CallbackContext context)
    {
        //exampleBool = false;
        exampleBool = context.ReadValueAsButton();
        Debug.Log($"Salto cancelado, la booleana es: {exampleBool}");
    }

    private void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>().normalized;
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>().normalized;
    }

    // Solo haremos la función perform porque nos interesa que siga leyendo la rotación mientras se mueve
    private void MouseRotate(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = context.ReadValue<Vector2>();
        aimPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));

        Vector3 direction = aimPosition - transform.position;
        ApplyRotation(direction);
    }

    private void GamepadRotate(InputAction.CallbackContext context)
    {
        Vector2 directionStick = context.ReadValue<Vector2>();
        if (directionStick != Vector2.zero)
            ApplyRotation(directionStick);
    }

    private void ApplyRotation(Vector2 rotationDir)
    {
        zRotation = Mathf.Atan2(rotationDir.y, rotationDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }
}
