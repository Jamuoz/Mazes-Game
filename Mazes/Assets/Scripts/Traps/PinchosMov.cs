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
            initialPosition = new GameObject("Initial Position").transform;
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

    // Corutina para mover los pinchos hacia adelante y hacia atrás
    private IEnumerator MovePinchos()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(tiempoEspera);
            yield return transform.DOMoveY(initialPosition.position.y - metros, tiempoRecorrido).WaitForCompletion();
            yield return new WaitForSeconds(tiempoEspera);
            yield return transform.DOMoveY(initialPosition.position.y, tiempoRecorrido).WaitForCompletion();
            yield return new WaitForSeconds(tiempoEspera);
        }
    }

    private void OnEnable()
    {
        transform.position = initialPosition.position;
        StartCoroutine(MovePinchos()); 
    }

    private void OnDisable()
    {
        transform.DOKill(); 
        StopCoroutine(MovePinchos()); 
    }

}
