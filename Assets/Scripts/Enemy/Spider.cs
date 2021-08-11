using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health {get; set; }

    [SerializeField]
    private GameObject _acidPrefab;
    
    //use this for initialization
    public override void Init()
    {
        base.Init();
        Health = health;
    }

    public void Damage()
    {
        Health--;
        if(Health < 1)
        {
            anim.SetTrigger("Death");
        }
    }

    public override void Movement()
    {
        //stand still
    }

    public void Attack()
    {
        Instantiate(_acidPrefab, transform.position, Quaternion.identity);
    }
}
