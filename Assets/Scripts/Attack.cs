using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canAttack = true;
    [SerializeField][Range(1,3)]
    private int _damageAmout = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if(hit != null)
        {
            if(_canAttack)
            {
                hit.Damage(_damageAmout);
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
