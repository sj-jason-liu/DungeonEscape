using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //create a variable as switch to damage
    private bool _canAttack = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if(hit != null)
        {
            if(_canAttack)
            {
                hit.Damage();
                _canAttack = false;
                Invoke("CanAttack", 0.5f);
            }
        }
    }

    void CanAttack()
    {
        _canAttack = true;
    }
}
