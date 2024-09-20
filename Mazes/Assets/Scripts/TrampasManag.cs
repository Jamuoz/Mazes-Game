using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum TrapsSelect { Pinchos, Corredor, EnemySpawn }
public class TrampasManag : MonoBehaviour
{
    #region variables
    public GameObject[] EnemysCount;
    public GameObject[] Traps;
    public GameObject[] Walls;
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

    

    public void ActiveEnemySpawnTrap()
    {
        Traps[2].transform.position = Walls[2].transform.position;
    }

    public void ActiveCorredorTrap(Transform star,Transform end,GameObject trap)
    {
        Traps[1].transform.position = Walls[1].transform.position;

    }

    public void ActivePinchosTrap()
    {
        Traps[0].transform.position = Walls[0].transform.position;
        Traps[0].transform.DOMoveY(Traps[0].transform.position.y -2,4);
    }
    #endregion
}
