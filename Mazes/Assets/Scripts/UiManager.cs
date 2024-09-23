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
    float VIdaEnUi;

    //void Start()
    //{
    //    // Llamamos a la función UpdateUI para que se ejecute continuamente
    //    UpdateUI();
    //}

    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        Vida.fillAmount = Player.currentHealth / Player.maxHealth;
        VIdaEnUi=Player.currentHealth;
        Estamina.fillAmount = Player.currentStamina / Player.maxStamina;
        EstadoMental.fillAmount = Player.currentmentalState / Player.maxMentalState;
        if (VIdaEnUi < Player.currentHealth)
        {
            //Invoke(FadeUpdate())
            FadeUpdate();
        }

        // Llama de nuevo a esta función en el siguiente frame
        //Invoke("UpdateUI", Time.deltaTime);
    }

    void FadeUpdate()
    {
        DelayVida.DOFillAmount(Vida.fillAmount, delayDuration);
    }

}
