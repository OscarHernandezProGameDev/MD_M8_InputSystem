using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmbeddedMovement3D : MonoBehaviour
{
    public InputAction move;
    [SerializeField] private float speed;
    private CharacterController characterController;
    private Vector3 direction;

    void OnEnable()
    {
        move.performed += Move;
        move.Enable();
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        ApplyMovement();
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector3>();
        direction = new Vector3(input.x, input.y, input.z);
    }

    private void ApplyMovement()
    {
        characterController.Move(direction * (Time.deltaTime * speed));
    }

    void OnDisable()
    {
        move.performed -= Move;
        move.Disable();
    }
}
