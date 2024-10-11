using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UiManager : MonoBehaviour
{
    // Elementos UI
    public Image Vida;
    public Image DelayVida;
    public Image Estamina;
    public Image EstadoMental;
    //private bool completed=true;

    // Referencias
    public PlayerController Player;

    // variables
    public float delayDuration;
    

    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        //actualizacion de las barras de vida
        Vida.fillAmount = Player.currentHealth / Player.maxHealth;
        Estamina.fillAmount = Player.currentStamina / Player.maxStamina;
        EstadoMental.fillAmount = Player.currentmentalState / Player.maxMentalState;

    }

}
