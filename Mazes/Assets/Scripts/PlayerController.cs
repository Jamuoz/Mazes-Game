using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    //feedback
    //[SerializeField] GameObject panel;
    [SerializeField] AudioSource Pasos, damage;
    bool vActive, hActive;
    // Variables de estado

    public float maxHealth = 100f;
    public float currentHealth;
    public float maxStamina = 100f;
    public float currentStamina;
    public float maxMentalState = 100f;
    public float currentmentalState;

    // Variables de movimiento
    public float moveSpeed = 5f;
    private float RunSpeed;
    private float NormalSpeed;
    public float runSpeedMultiplier = 2f; 
    public float jumpForce = 10f;
    private bool isGrounded;

    private Rigidbody rb;
    private Collider playerCollider; 

    // Variables para la c�mara
    public Transform playerCamera;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>(); 
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentmentalState = maxMentalState;
        SpeedUpdate(); 
    }

    void Update()
    {
        //CheckIfGrounded(); 
        Move();
        //Jump(); 
        LookAround();
        HandleStaminaAndHealth();
        ReproAudio();
        ChangePanel();


    }

    void ChangePanel()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(!panel.activeSelf);

        }
        if (panel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }*/
    }
    void ReproAudio()
    {
        if (Pasos != null)
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                hActive = true;
                Pasos.Play();
            }
            if (Input.GetButtonDown("Vertical"))
            {
                vActive = true;
                Pasos.Play();
            }
            if (Input.GetButtonUp("Horizontal"))
            {
                hActive = false;
                if (!vActive)
                {
                    Pasos.Pause();
                }
            }
            if (Input.GetButtonUp("Vertical"))
            {
                vActive = false;
                if (!hActive)
                {
                    Pasos.Pause();
                }
            }

        }
        
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

        // Proyectar el movimiento para evitar que se pegue a las paredes
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("Paredes"))
            {
                moveDirection = Vector3.ProjectOnPlane(moveDirection, hit.normal); 
            }
        }

        if (moveDirection.magnitude >= 0.1f)
        {
            rb.velocity = moveDirection * moveSpeed + new Vector3(0, rb.velocity.y, 0);
            
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
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
        // Rotaci�n en el eje Y (girar el personaje)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        // Rotaci�n en el eje X (girar la c�mara)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar la rotaci�n en el eje X

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void SpeedUpdate()
    {
        RunSpeed = moveSpeed * runSpeedMultiplier;
        NormalSpeed = moveSpeed;
    }

    void HandleStaminaAndHealth()
    {
        
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

        // Si la estamina est� vac�a, empieza a reducir el estado mental
        if (currentStamina <= 0 && currentmentalState > 0)
        {
            currentmentalState -= Time.deltaTime * 4f;
            if (currentmentalState <= 0)
            {
                currentHealth -= Time.deltaTime * 10f;  // Reduce salud si el estado mental se vac�a
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
    void CheckIfGrounded()
    {
        // Define la posici�n desde la cual lanzar el rayo (desde el centro inferior del collider)
        Vector3 bottomCenter = new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y, playerCollider.bounds.center.z);
        float distanceToGround = 0.2f;
        isGrounded = Physics.Raycast(bottomCenter, Vector3.down, distanceToGround);
        
    }
    

    void Die()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Muerte");
    }

    // M�todos para modificar la salud y el estado mental
    public void AdjustHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0f, 100f); // Resto da�o a la vida sin dejar que baje de 0
        if (currentHealth <= 0)
        {
            Die();
        }
        if (damage != null )
        {
            if (value < 0)
            {
                damage.Play();
            }
        }
    }

    public void AdjustMentalState(float adjustment)
    {
        currentmentalState = Mathf.Clamp(currentmentalState + adjustment, 0f, 100f);
    }

}
