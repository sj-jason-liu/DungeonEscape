using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = health;
    }

    public void Damage()
    {
        Debug.Log(name + " Damage()");
        Health--;
        anim.SetTrigger("Hit");
        hasHit = true;
        anim.SetBool("InCombat", true);

        if(Health < 1)
        {
            anim.SetTrigger("Death");
        }
    }
}
