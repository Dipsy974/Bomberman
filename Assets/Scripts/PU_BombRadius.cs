using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_BombRadius : PowerUps
{
    public override void GetCollected(PlayerController player)
    {
        player.IncreaseBombLevel(); 
    }
}
