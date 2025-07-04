using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SignalChange : MonoBehaviour
{
    Material mati;
    Color coltrans;
    Color colNor;
    public float durationChange = 0.2f;
    public float durationWait = 5f;

    // Start is called before the first frame update
void Start()
{
    mati = GetComponent<Renderer>().material;
    colNor = mati.color;
    coltrans = new Color(mati.color.r, mati.color.g, mati.color.b, 1f);
    StartCoroutine(ChangeColor());
}

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(durationWait);
        for (int i = 0; i < 5; i++) 
        {
        yield return mati.DOColor(coltrans, durationChange).WaitForCompletion();
        yield return mati.DOColor(colNor, durationChange).WaitForCompletion();
        }
        StartCoroutine(ChangeColor());
    }
}
