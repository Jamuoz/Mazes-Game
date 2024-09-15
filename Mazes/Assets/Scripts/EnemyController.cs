using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Variables de estado
    public float maxHealth = 50f;
    public float currentHealth;
    public float maxStamina = 50f;
    public float currentStamina;

    // Variables de combate
    public float attackRange = 2f;
    public float detectionRange = 10f;
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    // Referencia al jugador
    private GameObject player;

    // Layer para las paredes
    public LayerMask obstacleMask;

    // Referencia al NavMeshAgent
    private NavMeshAgent agent;

    // Puntos de patrullaje
    public GameObject[] paradas;
    public GameObject ParadasParent;
    public int RandomLOcation;
    public int numParada;
    public float TimeToPatru;

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        RandomLOcation = 1;

        // Inicializa y llena la matriz paradas
        int childCount = ParadasParent.transform.childCount;
        paradas = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            paradas[i] = ParadasParent.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange && CanSeePlayer())
        {
            ChasePlayer();

            if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            TimeToPatru += Time.deltaTime;
            if (TimeToPatru >= 10)
            {
                TimeToPatru = 0;
                agent.SetDestination(paradas[numParada].transform.position); // Mueve al enemigo a la siguiente parada

                // Actualiza numParada
                if (numParada >= paradas.Length - 1)
                {
                    numParada = 0;
                }
                else
                {
                    numParada++;
                }
            }
        }
    }

    void ChasePlayer()
    {
        // Usar NavMeshAgent para moverse hacia el jugador
        agent.SetDestination(player.transform.position);
    }

    void AttackPlayer()
    {
        // Lógica para atacar al jugador
        player.GetComponent<PlayerController>().TakeDamage(attackDamage);
    }

    // Método para verificar si el enemigo puede ver al jugador
    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, directionToPlayer);
        RaycastHit hit;

        // Realizar el raycast
        if (Physics.Raycast(ray, out hit, detectionRange, obstacleMask))
        {
            // Si el raycast golpea al jugador y no a una pared
            if (hit.transform.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Lógica para manejar la muerte del enemigo
        Destroy(gameObject);
    }
}
