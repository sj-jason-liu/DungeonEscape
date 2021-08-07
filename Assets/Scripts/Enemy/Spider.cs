using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    private Vector3 _targetPosition;

    private Animator _anim;

    private SpriteRenderer _sprite;

    private bool _movingLeft;
    
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        if(_anim == null)
        {
            Debug.LogError("Animator is NULL!");
        }
        _sprite = GetComponentInChildren<SpriteRenderer>();
        if(_sprite == null)
        {
            Debug.LogError("SpriteRenderer is NULL!");
        }
    }

    public override void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return;
        Movement();
    }

    void Movement()
    {
        _sprite.flipX = _movingLeft;

        switch (_movingLeft)
        {
            case true:
                _targetPosition = _pointA.position;
                break;
            case false:
                _targetPosition = _pointB.position;
                break;
        }

        if (Vector3.Distance(transform.position, _targetPosition) <= 0)
        {
            _movingLeft = !_movingLeft;
            _anim.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
    }
}
