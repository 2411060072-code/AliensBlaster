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
    private Animator _animator;
    [SerializeField] private Sprite _jumpingSpriteRenderer;
    private float _horizontal;
    private float _vertical;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float _footOfSet = 0.3f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        if (_spriteRenderer == null) return;

        Gizmos.color = Color.red;

        //middle
        Vector2 _origin = new Vector2(transform.position.x, transform.position.y - _spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(_origin, _origin + Vector2.down * 0.1f);

        //left
        _origin = new Vector2(transform.position.x - _footOfSet, transform.position.y - _spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(_origin, _origin + Vector2.down * 0.1f);

        //right
        _origin = new Vector2(transform.position.x + _footOfSet, transform.position.y - _spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(_origin, _origin + Vector2.down * 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 _originMidle = new Vector2(transform.position.x, transform.position.y - _spriteRenderer.bounds.extents.y);
        Vector2 _originLeft = new Vector2(transform.position.x - _footOfSet, transform.position.y - _spriteRenderer.bounds.extents.y);
        Vector2 _originRight = new Vector2(transform.position.x + _footOfSet, transform.position.y - _spriteRenderer.bounds.extents.y);

        RaycastHit2D hitMidle = Physics2D.Raycast(_originMidle, Vector2.down, 0.1f, layerMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(_originLeft, Vector2.down, 0.1f, layerMask);
        RaycastHit2D hitRight = Physics2D.Raycast(_originRight, Vector2.down, 0.1f, layerMask);

        if (hitMidle.collider || hitLeft.collider || hitRight.collider)
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
