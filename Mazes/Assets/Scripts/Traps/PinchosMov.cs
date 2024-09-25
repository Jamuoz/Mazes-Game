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
                player.TakeDamage(damage * Time.deltaTime);
            }
        }

    }

    // Corutina para mover el láser hacia adelante y hacia atrás
    private IEnumerator MoveLaser()
    {
        while (gameObject.activeSelf)
        {
            // Mover hacia adelante
            transform.DOMoveY(initialPosition.position.y - metros, tiempoRecorrido);
            yield return new WaitForSeconds(tiempoEspera);

            // Mover hacia atrás
            transform.DOMoveY(initialPosition.position.y, tiempoRecorrido);
            yield return new WaitForSeconds(tiempoEspera);
        }
    }
    
    private void OnEnable()
    {
        transform.position = initialPosition.position;
        StartCoroutine(MoveLaser());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
