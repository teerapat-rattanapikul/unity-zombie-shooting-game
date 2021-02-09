using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHurtBox : MonoBehaviour
{
    ZombieController parent;

    public float hurtRate = 1f;

    private void Start()
    {
        parent = GetComponentInParent<ZombieController>();
    }

    public void TakeDamage(int damage)
    {
        parent.TakeDamage((int)(damage * hurtRate), hurtRate > 1);
    }
}
