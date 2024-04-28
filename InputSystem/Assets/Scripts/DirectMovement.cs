using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private float valueX, valueY;
    private Vector2 direction, directionGamepad, rotationGamepad;
    private Vector3 mousePosition, aimPosition;
    private float zRotation;

    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;
        Gamepad gamepad = Gamepad.current;

        if (keyboard != null)
        {
            // Reset values
            valueX = valueY = 0;

            if (keyboard.aKey.isPressed)
            {
                valueX = -1;
            }
            if (keyboard.dKey.isPressed)
            {
                valueX = 1;
            }

            // Para evitar que cuando se pulsa a la vez la tecla derecha y la izquierda se mueva a la derecha o a la izquierda
            if (keyboard.dKey.isPressed && keyboard.aKey.isPressed)
                valueX = 0;

            if (keyboard.wKey.isPressed)
            {
                valueY = 1;
            }
            if (keyboard.sKey.isPressed)
            {
                valueY = -1;
            }

            // Para evitar que cuando se pulsa a la vez la tecla arriba y la abajo se mueva hacia arriba o hacia abajo
            if (keyboard.wKey.isPressed && keyboard.sKey.isPressed)
                valueY = 0;
        }

        if (mouse != null)
        {
            mousePosition = mouse.position.ReadValue();
            Rotate();

            if (mouse.leftButton.wasPressedThisFrame)
            {
                //Comentamos para que no interfiera con el disparo de proyectiles del gamepad
                //ShootProjectile();
            }
        }

        if (gamepad != null)
        {
            directionGamepad = gamepad.leftStick.ReadValue();
            valueX = directionGamepad.x;
            valueY = directionGamepad.y;
            rotationGamepad = gamepad.rightStick.ReadValue();
            GamepadRotation();

            if (gamepad.rightTrigger.wasPressedThisFrame)
                GamepadShootProjectile();
        }

        direction = new Vector2(valueX, valueY).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    private void Rotate()
    {
        aimPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));

        Vector3 direction = aimPosition - transform.position;
        zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }


    private void ShootProjectile()
    {
        Vector3 shootDirection = (aimPosition - shootPosition.position).normalized;
        GameObject tempProjectile = Instantiate(projectilePrefab, shootPosition.position, Quaternion.identity);

        tempProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * 10;
    }

    private void GamepadRotation()
    {
        if (rotationGamepad != Vector2.zero)
            zRotation = Mathf.Atan2(rotationGamepad.y, rotationGamepad.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }

    private void GamepadShootProjectile()
    {
        Vector3 shootDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * zRotation), Mathf.Sin(Mathf.Deg2Rad * zRotation), 0);
        GameObject tempProjectile = Instantiate(projectilePrefab, shootPosition.position, Quaternion.identity);

        tempProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * 10;
    }
}
