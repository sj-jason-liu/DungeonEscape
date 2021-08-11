﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }
    
    //use this for initialization
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

        if (Health < 1)
        {
            //Destroy(gameObject);
        }
    }
}
