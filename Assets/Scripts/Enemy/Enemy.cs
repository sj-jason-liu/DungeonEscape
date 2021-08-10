using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform _pointA, _pointB;

    protected Vector3 targetPosition;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected bool movingLeft;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator is NULL!");
        }
        sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.LogError("SpriteRenderer is NULL");
        }
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return;
        Movement();
    }

    public virtual void Movement()
    {
        sprite.flipX = movingLeft;
        switch (movingLeft)
        {
            case true:
                targetPosition = _pointA.position;
                break;
            case false:
                targetPosition = _pointB.position;
                break;
        }

        if (Vector3.Distance(transform.position, targetPosition) <= 0)
        {
            movingLeft = !movingLeft;
            anim.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
