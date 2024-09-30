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

    public Transform initialPosition;  // Guardar la posici�n inicial
    private void Start()
    {
        if (initialPosition == null)
        {
            initialPosition.position = transform.position;
        }
        // Guardar la posici�n inicial una sola vez
        
    }

    // Se llama cada vez que el objeto se activa (es encendido)
    private void OnEnable()
    {
        // Reiniciar la posici�n inicial
        transform.position = initialPosition.position;

        // Empezar el movimiento del l�ser
        StartCoroutine(MoveLaser());
    }

    private void OnDisable()
    {
        // Detener la corutina cuando el objeto se desactiva
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.AdjustHealth(-damage);
        }
    }

    // Corutina para mover el l�ser hacia adelante y hacia atr�s
    private IEnumerator MoveLaser()
    {
        while (gameObject.activeSelf)
        {
            // Mover hacia adelante
            transform.DOMoveZ(initialPosition.position.z + metros, tiempoRecorrido);
            yield return new WaitForSeconds(tiempoEspera);

            // Mover hacia atr�s
            transform.DOMoveZ(initialPosition.position.z, tiempoRecorrido);
            yield return new WaitForSeconds(tiempoEspera);
        }
    }
}
