using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStatic : MonoBehaviour
{
    public float damage = 10f;
    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null) 
        {
            MakeDamage(playerController);
        }
    }

    void MakeDamage(PlayerController Player)
    {
        Player.GetComponent<PlayerController>().AdjustHealth(-damage);
    }
}
