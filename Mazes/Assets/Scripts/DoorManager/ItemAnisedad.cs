using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ItemAnisedad : MonoBehaviour
{
    bool IsReward = false;
    bool firstTime = true;
    [SerializeField] private ActiveDoor ActiveDoor;
    [SerializeField] private GameObject canvas;  // Canvas global que se mover� al �tem
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField] private string frase;
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject ubicacionCanvas;
    [SerializeField] private Light linterna;
    public float velocidadEscritura;

    private void Awake()
    {
        if (ActiveDoor == null)
        {
            ActiveDoor = FindAnyObjectByType<ActiveDoor>();
        }
    }

    // M�todo para posicionar y rotar el canvas hacia el jugador
    private void PositionUpdate()
    {
        // Mueve el canvas ligeramente fuera del �tem
        canvas.transform.position = ubicacionCanvas.transform.position ;
        canvas.transform.rotation = ubicacionCanvas.transform.rotation;
    }


    private void OnTriggerStay(Collider other)
    {
        if (firstTime)
        { 
            sprite.SetActive(true);
            // Solo muestra la recompensa si a�n no se ha recogido y es el jugador
            if (!IsReward && other.CompareTag("Player"))
            {
                linterna.intensity = 1f;
                if (Input.GetKeyDown(KeyCode.F)) // Verifica que el jugador presione "F"
                {
                    sprite.SetActive(false);
                    ShowItemReward(other.gameObject);
                }
            }
        }
        else
        {
            PositionUpdate();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            linterna.intensity = 3f;
        }
    }
    private void ShowItemReward(GameObject other)
    {
        // Activa el canvas y lo posiciona
        canvas.SetActive(true);
        IsReward = true;

        // L�gica adicional al recoger el �tem
        PositionUpdate();  // Posiciona el canvas y lo rota hacia el jugador
        StartCoroutine(ChangeStringtoChar(frase));  // Muestra el texto
    }

    IEnumerator ChangeStringtoChar(string frase)
    {
        texto.text = "";  // Limpia el texto antes de empezar

        // Muestra el texto letra por letra
        foreach (char letra in frase)
        {
            texto.text += letra;
            yield return new WaitForSeconds(velocidadEscritura);  // Espera entre cada letra
        }
        ActiveEffects();
        ActiveDoor.ItemCollect();
        Instantiate(canvas);
    }

    private void ActiveEffects()
    {
        linterna.intensity = 3f;
        // Aqu� puedes agregar efectos de sonido, part�culas, etc.
        Destroy(gameObject, 1f);  // Destruye el �tem despu�s de 1 segundo
    }
}
