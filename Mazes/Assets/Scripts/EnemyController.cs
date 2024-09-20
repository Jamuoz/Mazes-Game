using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] patrolPoints;  // Puntos de patrulla
    public float patrolSpeed = 3.5f;
    public float chaseSpeed = 5f;
    public float detectionRange = 10f;
    public float loseRange = 15f;
    public float waitTime = 2f;  // Tiempo que espera antes de continuar patrullando
    public Transform player;

    private int currentPointIndex = 0;
    private NavMeshAgent agent;
    private float waitTimer;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        // Inicia el patrullaje hacia el primer punto
        agent.destination = patrolPoints[currentPointIndex].position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Perseguir al jugador
            isChasing = true;
            agent.speed = chaseSpeed;
            agent.destination = player.position;
        }
        else if (distanceToPlayer > loseRange)
        {
            // Dejar de perseguir y volver a patrullar
            isChasing = false;
            agent.speed = patrolSpeed;
            Patrol();
        }
        else if (isChasing)
        {
            // Seguir persiguiendo si el jugador está dentro del rango de detección pero no ha salido del rango de perder
            agent.destination = player.position;
        }

        // Comportamiento de patrulla si no está persiguiendo
        if (!isChasing)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Si ha llegado al punto de patrullaje actual y no está persiguiendo al jugador
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                // Selecciona un punto de patrullaje aleatorio
                currentPointIndex = Random.Range(0, patrolPoints.Length);
                agent.destination = patrolPoints[currentPointIndex].position;
                waitTimer = 0f;  // Reinicia el temporizador de espera
            }
        }
    }
}
