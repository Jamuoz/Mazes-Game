using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    // Start is called before the first frame update
    TrapsSelect SelectTrap;
    TrampasManag refe;
    
    
    public void StarTrap()
    {
        switch (SelectTrap)
        {
            case TrapsSelect.Pinchos:
                refe.ActivePinchosTrap();
                break;
            case TrapsSelect.Corredor:
                //refe.ActiveCorredorTrap();
                break;
            case TrapsSelect.EnemySpawn:
                refe.ActiveEnemySpawnTrap();
                break;
            default:
                break;
        }
    }
}
