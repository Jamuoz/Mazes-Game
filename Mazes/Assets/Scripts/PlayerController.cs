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
    public float maxMentalState=100;// Estado mental, entre 0 y 100
    public float currentmentalState;

    // Variables de movimiento
    public float moveSpeed = 5f;
    private float RunSpeed;
    private float NormalSpeed;
    public float runSpeedMultiplier = 2f; // Multiplicador para velocidad al correr
    public float jumpForce = 10f;
    private bool isGrounded;

    private Rigidbody rb;

    // Variables para la cámara
    public Transform playerCamera;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentmentalState = maxMentalState;
        SpeedUpdate();

        // Ocultar y bloquear el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Jump();
        LookAround();
        HandleStaminaAndHealth();
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;

        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            moveSpeed = RunSpeed; 
        }
        else
        {
            moveSpeed = NormalSpeed;
            
        }
        //if (moveDirection.magnitude >= 0.1f) 
        //{
        //    rb.velocity = moveDirection * moveSpeed + new Vector3(0, rb.velocity.y, 0);  
        //}
        //else
        //{
        //    rb.velocity = new Vector3(0, rb.velocity.y, 0);
        //}
        rb.velocity = moveDirection * moveSpeed + new Vector3(0, rb.velocity.y, 0);
    }


    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (currentStamina >= 10f)
            {
                currentStamina -= 5f;
            }
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

    void SpeedUpdate()
    {
        RunSpeed = moveSpeed * runSpeedMultiplier;
        NormalSpeed = moveSpeed;
    }
    void HandleStaminaAndHealth()
    {
        // Verifica si el jugador está presionando Shift y tiene alguna entrada de movimiento
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            if (currentStamina > 0)
            {
                currentStamina -= Time.deltaTime * 10f;  
            }
        }
        else if (currentStamina < maxStamina)
        {
            currentStamina += Time.deltaTime * 5f;  
        }

        // Si la estamina está vacía, empieza a reducir el estado mental
        if (currentStamina <= 0 && currentmentalState > 0)
        {
            currentmentalState -= Time.deltaTime * 4f;
            if (currentmentalState <= 0)
            {
                currentHealth -= Time.deltaTime * 10f;  // Reduce salud si el estado mental se vacía
                if (currentHealth <= 0)
                {
                    Die();
                }
            }
        }
        else if (currentmentalState < maxMentalState)
        {
            currentmentalState += Time.deltaTime * 0.5f;  // Regenera estado mental lentamente
        }
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
        currentmentalState = Mathf.Clamp(currentmentalState + adjustment, 0f, 100f);
    }

    void Die()
    {
        // Lógica para manejar la muerte del personaje
        Debug.Log("Player has died.");
    }
}
