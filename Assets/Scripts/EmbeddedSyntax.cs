using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;

public class EmbeddedSyntax : MonoBehaviour
{
    public InputAction jumpAction;
    public InputAction moveAction;
    [SerializeField] private float speed;

    private bool exampleBool;

    private Vector2 direction;

    private Transform myTransform;

    void Awake()
    {
        myTransform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        jumpAction.performed += Jump;
        jumpAction.canceled += StopJump;
        jumpAction.Enable();

        moveAction.performed += Move;
        moveAction.canceled += StopMove;
        moveAction.Enable();
    }

    private void OnDisable()
    {
        jumpAction.performed -= Jump;
        jumpAction.canceled -= StopJump;
        jumpAction.Disable();

        moveAction.performed -= Move;
        moveAction.canceled -= StopMove;
        moveAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(moveAction.ReadValue<Vector2>().normalized);

        myTransform.Translate(direction * (speed * Time.deltaTime));
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
        //Debug.Log(context.ReadValue<Vector2>().normalized);
        direction = context.ReadValue<Vector2>().normalized;
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector2>().normalized);
        direction = context.ReadValue<Vector2>().normalized;
    }
}
