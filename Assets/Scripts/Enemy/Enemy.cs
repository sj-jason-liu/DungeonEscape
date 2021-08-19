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
    protected float detectRange = 2f;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform _pointA, _pointB;
    [SerializeField]
    protected GameObject diamondPrefab;

    protected Vector3 targetPosition, currentSpriteScale;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected Player player;
    [SerializeField] protected bool movingLeft;
    protected bool hasHit;
    protected bool isDead;

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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player == null)
        {
            Debug.LogError("Player is NULL!");
        }
        currentSpriteScale = sprite.transform.localScale;
        //Debug.Log("Scale of " + name + " sprite: " + currentSpriteScale);
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
            return;
        if(!isDead)
            Movement();
    }

    public virtual void Movement()
    {
        float facing = movingLeft ? -1 : 1;
        sprite.transform.localScale
            = new Vector3(currentSpriteScale.x * facing, currentSpriteScale.y, currentSpriteScale.z);
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

        if(!hasHit)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, player.transform.position) > detectRange || player.DeathState())
        {
            hasHit = false;
            anim.SetBool("InCombat", false);
        }

        Vector3 direction = player.transform.GetChild(0).transform.position - transform.position;
        if(anim.GetBool("InCombat"))
        {
            Debug.Log("Player position: " + player.transform.GetChild(0).transform.position + " and Enemy position: " + transform.position);
            Debug.Log("Direction.X: " + direction.x);
            if (direction.x > 0)
            {
                if(movingLeft)
                {
                    sprite.transform.localScale
                    = new Vector3(currentSpriteScale.x * -1, currentSpriteScale.y, currentSpriteScale.z);
                }
                else
                {
                    sprite.transform.localScale
                    = new Vector3(currentSpriteScale.x * 1, currentSpriteScale.y, currentSpriteScale.z);
                }
            }
            else if(direction.x < 0)
            {
                if(movingLeft)
                {
                    sprite.transform.localScale
                    = new Vector3(currentSpriteScale.x * 1, currentSpriteScale.y, currentSpriteScale.z);
                }
                else
                {
                    sprite.transform.localScale
                    = new Vector3(currentSpriteScale.x * -1, currentSpriteScale.y, currentSpriteScale.z);
                } 
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
    }
}
