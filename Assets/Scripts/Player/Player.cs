using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private int _health = 10;
    [SerializeField]
    private int _diamond;

    private bool _grounded, _resetJump, _hasDoubleJumped, _isDead;
    [SerializeField]
    private bool _hasFlameSword, _hasBoots, _hasKey;
    
    private Rigidbody2D _rb;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _sprite;
    private GameObject _swordArcObject;

    public int Health { get; set; }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        if(_sprite == null)
        {
            Debug.LogError("Spriterenderer is null!");
        }
        _swordArcObject = transform.GetChild(1).gameObject;
        if(_swordArcObject == null)
        {
            Debug.LogError("Sword Arc is NULL!");
        }
        Health = _health;
    }

    void Update()
    {
        if (_isDead)
            return;

        GetItemCheck();

        Movement();

        if (CrossPlatformInputManager.GetButtonDown("Button_B") && IsGrounded() == true)
        {
            if(_hasFlameSword)
            {
                _playerAnim.FlameAttack();
            }
            else
            {
                _playerAnim.Attack();
            }
        }
    }

    void Movement()
    {
        float horiInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float move = horiInput * _speed;
        _playerAnim.Move(horiInput);
        _grounded = IsGrounded();

        Flip(horiInput);

        if (IsGrounded() == true)
        {
            if(CrossPlatformInputManager.GetButtonDown("Button_A") || Input.GetKeyDown(KeyCode.Space))
            {
                _playerAnim.Jumping(true);
                StartCoroutine(ResetJumpRoutine());
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            }            
        }
        else if(_hasBoots)
        {
            if(CrossPlatformInputManager.GetButtonDown("Button_A") || Input.GetKeyDown(KeyCode.Space))
            {
                if(!_hasDoubleJumped)
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce * 1.5f);
                    _hasDoubleJumped = true;
                }
            }
        }
        _rb.velocity = new Vector2(move, _rb.velocity.y);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 8);
        if (hit.collider != null)
        {
            _hasDoubleJumped = false;
            if (_resetJump == false)
            {
                _playerAnim.Jumping(false);
                return true;
            }
        }
        return false;
    }

    void Flip(float facing)
    {
        Vector3 newPos = _swordArcObject.transform.localPosition;
        if (facing < 0)
        {
            _sprite.transform.localScale = new Vector3(-1, 1, 1);
            newPos.x = -1.01f;
            _swordArcObject.transform.localPosition = newPos;
            _swordArcObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        else if (facing > 0)
        {
            _sprite.transform.localScale = new Vector3(1, 1, 1);
            newPos.x = 1.01f;
            _swordArcObject.transform.localPosition = newPos;
            _swordArcObject.GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void AddDiamond(int diamondCollected)
    {
        _diamond += diamondCollected;
        UIManager.Instance.UpdateGemCount(_diamond);
        UIManager.Instance.OpenShop(_diamond);
    }

    public int CurrentDiamondCount()
    {
        return _diamond;
    }

    public void PurchasedDiamondCount(int cost)
    {
        _diamond -= cost;
        UIManager.Instance.UpdateGemCount(_diamond);
        UIManager.Instance.OpenShop(_diamond);
    }

    void GetItemCheck()
    {
        _hasFlameSword = GameManager.Instance.HasFlameSword;
        _hasBoots = GameManager.Instance.HasFlightBoots;
        _hasKey = GameManager.Instance.HasCastleKey;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        Debug.Log("Current health: " + Health);
        UIManager.Instance.UpdateLives(Health);
        if (Health < 1)
        {
            _playerAnim.Death();
            _isDead = true;
        }
    }

    public bool DeathState()
    {
        return _isDead;
    }
}
