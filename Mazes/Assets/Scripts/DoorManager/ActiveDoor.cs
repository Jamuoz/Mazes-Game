using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveDoor : MonoBehaviour
{
    [SerializeField] int maxItems;
    int itemCount;
    [SerializeField] TextMeshProUGUI Keys;
    public void ItemCollect()
    {
        itemCount++;
        Keys.text= itemCount.ToString() + "/" + maxItems.ToString();
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
