using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Laser : MonoBehaviour
{
    public int metros = 8;
    public float tiempoRecorrido = 2f;
    public float tiempoEspera = 5f;
    public float damage = 5f;
    public bool Hori;

    public Transform initialPosition;  
    private void Start()
    {
        if (initialPosition == null)
        {
            initialPosition.position = transform.position;
        }
        
    }
    private void OnEnable()
    {
        transform.position = initialPosition.position;
        StartLaserMovement(Hori);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.AdjustHealth(-damage);
        }
    }

    // Corutina para mover el láser hacia adelante y hacia atrás
    private void StartLaserMovement(bool dire)
    {
        if (!dire)
        {
            // Mover 
            transform.DOMoveZ(initialPosition.position.z + metros, tiempoRecorrido)
                .SetLoops(-1, LoopType.Yoyo) //invierte el movimiento
                .SetDelay(tiempoEspera); // Hace una pausa antes de invertir el movimiento
        }
        else
        {
            
            transform.DOMoveX(initialPosition.position.x + metros, tiempoRecorrido)
                .SetLoops(-1, LoopType.Yoyo)
                .SetDelay(tiempoEspera); 
        }
    }
}
