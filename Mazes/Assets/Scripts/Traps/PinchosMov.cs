using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PinchosMov : MonoBehaviour
{
    public float damage = 10f;
    public float tiempoRecorrido = 2f;
    public float tiempoEspera = 5f;
    public int metros = 5;
    public Transform initialPosition;

    private void Start()
    {
        if (initialPosition == null)
        {
            initialPosition.position = transform.position;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AdjustHealth(-damage * Time.deltaTime);
            }
        }

    }

    // Corutina para mover el láser hacia adelante y hacia atrás
    private void MovePinchos()
    {
        while (gameObject.activeSelf)
        {
            
            transform.DOMoveY(initialPosition.position.y + metros, tiempoRecorrido)
                .SetLoops(-1, LoopType.Yoyo) //invierte el movimiento
                .SetDelay(tiempoEspera);

        }
    }
    
    private void OnEnable()
    {
        transform.position = initialPosition.position;
        MovePinchos();
    }

    private void OnDisable()
    {
        transform.DOKill() ;
    }
}
