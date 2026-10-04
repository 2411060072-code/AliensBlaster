using System;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _jumpEndTime;
    [SerializeField] private float _jumpVelocity = 5;
    [SerializeField] private float _jumpDuration = 0.5f;
    private Vector2 _origin;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;
    [SerializeField] private bool _IsGrounded = false;
    [SerializeField] private float _horizontalVelocity = 3;
    private Sprite _defaultSpriteRenderer;
    private Animator _animator;
    [SerializeField] private Sprite _jumpingSpriteRenderer;
    private float _horizontal;
    private float _vertical;
    [SerializeField] private LayerMask layerMask;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _defaultSpriteRenderer = GetComponent<SpriteRenderer>().sprite;
        _animator = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_origin, _origin + Vector2.down * 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        _origin = new Vector2(transform.position.x, transform.position.y - _spriteRenderer.bounds.extents.y);
        RaycastHit2D hit = Physics2D.Raycast(_origin, Vector2.down, 0.1f, layerMask);
        if (hit.collider)
        {
            _IsGrounded = true;
        }
        else 
        {
            _IsGrounded = false;
        }
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = _rigidBody.linearVelocity.y;

        if (Input.GetKeyDown(KeyCode.Space) && _IsGrounded == true)
        {
            _jumpEndTime = Time.time + _jumpDuration;
        }

        if (Input.GetKey(KeyCode.Space) && _jumpEndTime > Time.time)
        {
            _vertical = _jumpVelocity;
        }

        _horizontal *= _horizontalVelocity;
        _rigidBody.linearVelocity = new Vector2(_horizontal, _vertical);

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        _animator.SetBool("IsGrounded", _IsGrounded);
        _animator.SetFloat("HorizontalSpeed", Math.Abs(_horizontal));

        if (_horizontal > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_horizontal < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
}
