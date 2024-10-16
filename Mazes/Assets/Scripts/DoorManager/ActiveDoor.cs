using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveDoor : MonoBehaviour
{
    int maxItems;
    int itemCount;
    public void ItemCollect()
    {
        itemCount++;
        if (itemCount >= maxItems)
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Victoria");
        }
    }
}
