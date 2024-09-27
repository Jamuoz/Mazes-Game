using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum TrapType { Pinchos, Lanzallamas, Lasers, EnemySpawn }
public class TrampasManag : MonoBehaviour
{
    #region variables
    public GameObject[] EnemysCount;
    public GameObject[] Traps;
    public GameObject enemytospawn;
    public bool Active,Finish;
    //public float RangeActivation;
    public float TimeActivation;
    //public string Type;
    public float Damage;
    public float Speed;
    #endregion
    // Start is called before the first frame update
    
    
    void Start()
    {
        DOTween.Init();
        EnemysCount = GameObject.FindGameObjectsWithTag("Enemy");
        Active = false;
        Finish = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region metodos
    

    public void EndTrap()
    {
        if (Finish) 
        {
            gameObject.SetActive(false);
        }
    }

    

    public void ActiveEnemySpawnTrap(GameObject Trap,Transform posi)
    {
        
    }

    public void ActiveCorredorTrap(GameObject Trap, Transform posi)
    {
        
    }

    public void ActivePinchosTrap(GameObject Trap, Transform posi)
    {
        
    }

    public void ActiveFireTrap(GameObject Trap, Transform posi)
    {

    }
    #endregion
}
