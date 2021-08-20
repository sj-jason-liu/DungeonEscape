using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDetection : MonoBehaviour
{
    [SerializeField] private bool _hasKey;

    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _boxCollider = gameObject.GetComponent<BoxCollider2D>();
        if(_boxCollider == null)
        {
            Debug.LogError("Box Collider is NULL!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(GameManager.Instance.HasCastleKey == false)
            {
                UIManager.Instance.NeedKeyText();
            }
            else
            {
                _boxCollider.enabled = false;
                _hasKey = true;
            }
        }
    }

    private void Update()
    {
        if(transform.position.y < -2.8f && _hasKey)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
    }

}
