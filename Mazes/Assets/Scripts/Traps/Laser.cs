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

    public Transform initialPosition;  // Guardar la posición inicial
    private void Start()
    {
        if (initialPosition == null)
        {
            initialPosition.position = transform.position;
        }
        // Guardar la posición inicial una sola vez
        
    }

    // Se llama cada vez que el objeto se activa (es encendido)
    private void OnEnable()
    {
        // Reiniciar la posición inicial
        transform.position = initialPosition.position;

        // Empezar el movimiento del láser
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

    // Corutina para mover el láser hacia adelante y hacia atrás
    private IEnumerator MoveLaser()
    {
        while (gameObject.activeSelf)
        {
            // Mover hacia adelante
            transform.DOMoveZ(initialPosition.position.z + metros, tiempoRecorrido);
            yield return new WaitForSeconds(tiempoEspera);

            // Mover hacia atrás
            transform.DOMoveZ(initialPosition.position.z, tiempoRecorrido);
            yield return new WaitForSeconds(tiempoEspera);
        }
    }
}
