using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePoison : MonoBehaviour
{
    public GameObject poison;
    public GameObject otherPlatform;
    private bool isActive = true;
    public float timeToOn = 3f;
    public float timeWait = 5f;
    private Renderer rend;
    private void Start()
    {
        rend = GetComponent<Renderer>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isActive)
            {
                isActive = false;
                ColorChange(Color.red);
                poison.SetActive(false);
                StartCoroutine(ChangeState());
            }
            
        }
    }
    

    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(timeWait);
        poison.SetActive(true);
        isActive = true;
        ColorChange(Color.green);
        
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
