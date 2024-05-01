using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmbeddedTest : MonoBehaviour
{
    public InputAction buttonAction;
    public InputAction axis1DAction;

    void OnEnable()
    {
        buttonAction.Enable();
        buttonAction.started += context => Debug.Log($"Button action started: {context.ReadValue<float>()}");
        buttonAction.performed += context => Debug.Log($"Button action performed: {context.ReadValue<float>()}");
        buttonAction.canceled += context => Debug.Log($"Button action canceled: {context.ReadValue<float>()}");

        axis1DAction.Enable();
        axis1DAction.started += context => Debug.Log($"Axis 1D action started: {context.ReadValue<Vector2>()}");
        axis1DAction.performed += context => Debug.Log($"Axis 1D action performed: {context.ReadValue<Vector2>()}");
        axis1DAction.canceled += context => Debug.Log($"Axis 1D action canceled: {context.ReadValue<Vector2>()}");
    }

    void OnDisable()
    {
        buttonAction.Disable();
        axis1DAction.Disable();
    }
}
