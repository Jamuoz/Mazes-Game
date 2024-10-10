using System;
using System.Collections;
using UnityEngine;

public class PausePoison : MonoBehaviour
{
    public GameObject poison;
    public GameObject otherPlatform;
    private Color myColor;
    private bool isActive = true;
    public float timeToOn = 3f;
    public float timeWait = 5f;
    private Renderer rend;
    private bool First = true;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        myColor = GetComponent<Renderer>().material.color;
        StartCoroutine(DarPista());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (First)
            {
                StopCoroutine(DarPista());
                
            }
            else 
            {
                First = false;
            }

            if (isActive)
            {
                isActive = false;
                ColorChange(Color.red);
                poison.SetActive(false);
                StartCoroutine(ChangeState());
            }
            
        }
    }

    

    IEnumerator DarPista()
    {
        yield return new WaitForSeconds(120f);
        ColorChange(Color.blue);
        yield return new WaitForSeconds(1f);
        ColorChange(myColor);
        yield return new WaitForSeconds(1f);
        ColorChange(Color.blue);
        yield return new WaitForSeconds(1f);
        ColorChange(myColor);
        yield return new WaitForSeconds(1f);
        ColorChange(Color.blue);
        yield return new WaitForSeconds(1f);
        ColorChange(myColor);
        yield return new WaitForSeconds(1f);
        ColorChange(Color.blue);
        yield return new WaitForSeconds(1f);
        ColorChange(myColor);
        
    }
    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(timeWait);
        poison.SetActive(true);
        isActive = true;
        ColorChange(myColor);
        
    }
    private void ColorAndActiveChange(Color col, bool act)
    {
        rend.material.color = col;
        isActive=act;

    }
    private void ColorChange(Color col)
    {
        rend.material.color = col;
        otherPlatform.GetComponent<PausePoison>().ColorAndActiveChange(col, isActive);
    }

}
