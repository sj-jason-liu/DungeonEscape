using System.Collections;
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
        if (isDead)
            return;

        Debug.Log(name + " Damage()");
        Health--;
        anim.SetTrigger("Hit");
        hasHit = true;
        anim.SetBool("InCombat", true);

        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject gem = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            gem.GetComponent<Diamond>().gem = gems;
        }
    }
}
