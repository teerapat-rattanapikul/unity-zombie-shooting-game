using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickable : PickableObject
{
    
    public int ammoAmount;

    public override void Picked(PlayerController player)
    {
        Debug.Log("Get Ammo");
        player.AddAmmo(ammoAmount);
        base.Picked(player);
    }
}
