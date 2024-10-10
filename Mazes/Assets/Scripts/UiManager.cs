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
        Vida.fillAmount = Player.currentHealth / Player.maxHealth;
        Estamina.fillAmount = Player.currentStamina / Player.maxStamina;
        EstadoMental.fillAmount = Player.currentmentalState / Player.maxMentalState;
        DelayVida.fillAmount= Player.currentHealth / Player.maxHealth;
        //if (DelayVida.fillAmount > Vida.fillAmount && completed && Player.currentHealth>0)
        //{

        //    completed = false;
        //    StartCoroutine(FadeUpdate());
        //}
    }

    
    
    //IEnumerator FadeUpdate()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    DelayVida.DOFillAmount(Vida.fillAmount, delayDuration).WaitForCompletion();
    //    completed = true;

        
    //}
}
