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

    public void Damage(int damageAmout)
    {
        if (isDead)
            return;

        Health -= damageAmout;
        if(Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject gem = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            gem.GetComponent<Diamond>().gem = gems;
        }
    }

    public override void Movement()
    {
        //stand still
    }

    public override void Update()
    {
        //prevent animator bool InCombat warning.
    }

    public void Attack()
    {
        Instantiate(_acidPrefab, transform.position, Quaternion.identity);
    }
}
