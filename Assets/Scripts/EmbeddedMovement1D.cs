using System;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmbeddedMovement1D : MonoBehaviour
{
    public InputAction moveAction;
    public InputAction jumpAction;

    [SerializeField] private float speed, jumpForce;
    private Rigidbody2D rb;
    private float direction, jumpValue;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPos;

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = moveAction.ReadValue<float>();
        jumpValue = jumpAction.ReadValue<float>();
        Debug.Log($"El valor del salto es: {jumpValue}");

        Flip();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed, 0);
        Jump();
    }

    private void Flip()
    {
        if (direction < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (direction > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Jump()
    {
        if (IsGrounded() && jumpValue > 0.5f)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public bool IsGrounded() => Physics2D.Raycast(groundCheckPos.position, Vector2.down, 0.1f, groundLayer);

    // Es mejor ordenar estos eventos según el orden de ejecución de unity
    // Lo podemos al final porque segun el execution order de unity es de lo úitimo que se ejecuta

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }
}
