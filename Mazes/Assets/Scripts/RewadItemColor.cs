using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RewadItemColor : MonoBehaviour
{
    Material myCol;
    Color ColorNew;
    Color ColorOld;
    

    // Start is called before the first frame update
    void Start()
    {
        myCol = GetComponent<Renderer>().material;
        
        StartCoroutine(ChangeColor());
    }
    IEnumerator ChangeColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            ColorNew = Random.ColorHSV(0f, 1f, 0.2f, 0.6f, 0.7f, 1f);
            ColorOld= new Color(ColorNew.r, ColorNew.g, ColorNew.b, myCol.color.a);
            yield return myCol.DOColor(ColorOld, 0.3f).WaitForCompletion();
        }
    }
   
}
