using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_Rewind : PowerUps
{
    public override void GetCollected(PlayerController player)
    {
        player.SetCanRewind(true); 
    }
}
