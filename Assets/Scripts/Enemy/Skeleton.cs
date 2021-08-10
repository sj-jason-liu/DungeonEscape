using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        //assign health to enemy health
        Health = base.health;
    }

    public void Damage()
    {
        //subtract 1 from health
        Health--;
        Debug.Log("Current health: " + Health);

        //if health is less than 1
        //destroy the object
        if(Health < 1)
        {
            Destroy(gameObject);
        }
    }
}
