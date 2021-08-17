using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Animator _swordAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        if(_anim == null)
        {
            Debug.LogError("Animator is missing!");
        }
        _swordAnim = transform.GetChild(1).GetComponent<Animator>();
        if(_swordAnim == null)
        {
            Debug.LogError("Sword Animation is NULL!");
        }
    }

    public void Move(float moveValue)
    {
        _anim.SetFloat("Move", Mathf.Abs(moveValue));
    }

    public void Jumping(bool hasJump)
    {
        _anim.SetBool("Jumping", hasJump);
    }    

    public void Attack()
    {
        _anim.SetTrigger("Attacking");
        _swordAnim.SetTrigger("SwordAnim");
    }

    public void FlameAttack()
    {
        _anim.SetTrigger("FlameAttacking");
        _swordAnim.SetTrigger("SwordAnim");
    }

    public void Death()
    {
        _anim.SetTrigger("Death");
    }
}
