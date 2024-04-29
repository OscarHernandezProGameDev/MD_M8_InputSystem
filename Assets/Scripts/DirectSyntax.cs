using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectSyntax : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            // Input.GetKeyDown(KeyCode.Space);
            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("La tecla Espacio fue presionada en este frame");
            }

            // Input.GetKeyUp(KeyCode.Space);
            if (keyboard.spaceKey.wasReleasedThisFrame)
            {
                Debug.Log("La tecla Espacio fue liberada en este frame");
            }

            // Input.GetKey(KeyCode.Space);
            if (keyboard.spaceKey.isPressed)
            {
                Debug.Log("La tecla Espacio esta presionada");
            }
        }

        Mouse mouse = Mouse.current;

        if (mouse != null)
        {
            // Input.GetMouseButtonDown(0);
            if (mouse.leftButton.wasPressedThisFrame)
            {
                Debug.Log("El boton izquierdo del ratón fue presionada en este frame");
            }

            // Input.GetMouseButton(1);
            if (mouse.rightButton.isPressed)
            {
                Debug.Log("El boton derecho del ratón esta siendo presionado");
            }

            // Input.GetMouseButtonUp(2);
            if (mouse.middleButton.wasReleasedThisFrame)
            {
                Debug.Log("El boton central del ratón fue liberado en este frame");
            }

            // Input.mouseScrollDelta.y>0;
            if (mouse.scroll.ReadValue().y > 0)
            {
                Debug.Log("El ratón se ha desplazado hacia arriba");
            }
            // Input.mouseScrollDelta.y<0;
            else if (mouse.scroll.ReadValue().y < 0)
            {
                Debug.Log("El ratón se ha desplazado hacia abajo");
            }

            // Comentamos para que no haga ruido
            // Input.mousePosition;            
            //Debug.Log($"la posicion del ratón es {mouse.position.ReadValue()}");
        }

        Gamepad gamepad = Gamepad.current;

        if (gamepad != null)
        {
            // Y
            if (gamepad.buttonNorth.wasPressedThisFrame)
            {
                Debug.Log("El boton Y del gamepad fue presionado en este frame");
            }

            // A
            if (gamepad.buttonSouth.wasReleasedThisFrame)
            {
                Debug.Log("El boton A del gamepad fue liberado en este frame");
            }

            // B
            if (gamepad.buttonEast.isPressed)
            {
                Debug.Log("El boton B del gamepad esta siendo presionado");
            }

            Debug.Log($"Valores de mi stick izquierdo: {gamepad.leftStick.ReadValue()}");
        }
    }
}