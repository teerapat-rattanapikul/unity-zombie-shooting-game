using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickable : PickableObject
{
    public int healthIncrease = 10;

    public override void Picked(PlayerController player)
    {
        // เก็บไปแล้วเพิ่มเลือด player
        Debug.Log("Health increase");
        player.ChangeHealth(healthIncrease);

        base.Picked(player);
    }
}
