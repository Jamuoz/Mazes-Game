using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UiManager : MonoBehaviour
{
    // Elementos UI
    public Image Vida;
    public Image DelayVida;
    public Image Estamina;
    public Image EstadoMental;

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
        Vida.fillAmount = Player.currentHealth / Player.maxHealth;
        Estamina.fillAmount = Player.currentStamina / Player.maxStamina;
        EstadoMental.fillAmount = Player.currentmentalState / Player.maxMentalState;
        if (DelayVida.fillAmount > Vida.fillAmount)
        {
            FadeUpdate();
        }
    }

    void FadeUpdate()
    {
        DelayVida.DOFillAmount(Vida.fillAmount, delayDuration);
    }

}
