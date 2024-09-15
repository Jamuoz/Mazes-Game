using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables de estado
    public float maxHealth = 100f;
    public float currentHealth;
    public float maxStamina = 100f;
    public float currentStamina;
    public float mentalState = 100f; // Estado mental, entre 0 y 100

    // Variables de movimiento
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    private bool isRunning;

    private Rigidbody rb;

    // Variables para la cámara
    public Transform playerCamera;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    void Start()
    {
        isRunning = false;
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;

        // Ocultar y bloquear el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        //Jump();
        LookAround();

        // Ejemplo de desgaste de resistencia y salud con el tiempo
        if (isRunning)
        {
            currentStamina -= Time.deltaTime * 1f;
            if (currentStamina <= 0)
            {
                mentalState -= Time.deltaTime * 4f;
                if (mentalState <= 0)
                {
                    currentHealth -= Time.deltaTime * 2f;
                }
            }
        }
        else
        {
            moveSpeed = 5;

            if (currentStamina<maxStamina)
            {
                currentStamina += Time.deltaTime * 1f;
                
            }
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(transform.position + move);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Run();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            moveSpeed = 5;
        }
       
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void LookAround()
    {
        // Rotación en el eje Y (girar el personaje)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        // Rotación en el eje X (girar la cámara)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotación en el eje X

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // Métodos para modificar la salud y el estado mental
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void AdjustMentalState(float adjustment)
    {
        mentalState += adjustment;
        mentalState = Mathf.Clamp(mentalState, 0f, 100f);
    }

    public void Run()
    {
        isRunning = true;
        if (isGrounded)
        {
            isRunning = true;
            moveSpeed += 5;
        }
        
    }
    void Die()
    {
        // Lógica para manejar la muerte del personaje
        Debug.Log("Player has died.");
    }
}
