using System;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpVelocity = 5;
    [SerializeField] private float _jumpDuration = 0.2f;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private float _horizontalVelocity = 3;
    [SerializeField] private Sprite _jumpingSpriteRenderer;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _footOfSet = 0.3f;
    [SerializeField] private int _numberOfJumps = 2;

    private float _horizontal;
    private float _vertical;
    private Animator _animator;
    private AudioSource _audioSource;
    private Vector2 _origin;
    private RaycastHit2D _hit;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;
    private float _jumpEndTime;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDrawGizmos()
    {
        if (_spriteRenderer == null) return;

        Gizmos.color = Color.red;

        //middle
        Vector2 originGizmos = new Vector2(transform.position.x, transform.position.y - _spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(originGizmos, originGizmos + Vector2.down * 0.1f);

        //left
        originGizmos = new Vector2(transform.position.x - _footOfSet, transform.position.y - _spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(originGizmos, originGizmos + Vector2.down * 0.1f);

        //right
        originGizmos = new Vector2(transform.position.x + _footOfSet, transform.position.y - _spriteRenderer.bounds.extents.y);
        Gizmos.DrawLine(originGizmos, originGizmos + Vector2.down * 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGrounding();
        UpdateMovement();
        UpdateSprite();
    }

    private void UpdateMovement()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = _rigidBody.linearVelocity.y;

        if (Input.GetKeyDown(KeyCode.Space) && _numberOfJumps > 0) 
        {
            if (_numberOfJumps > 1)
                _audioSource.pitch = 1;
            else
                _audioSource.pitch = 0.8f;
            _jumpEndTime = Time.time + _jumpDuration;
            _numberOfJumps--;
            _audioSource.Play();
        }

        if (Input.GetKey(KeyCode.Space) && _jumpEndTime > Time.time)
            _vertical = _jumpVelocity;

        _horizontal *= _horizontalVelocity;
        _rigidBody.linearVelocity = new Vector2(_horizontal, _vertical);
    }

    private void UpdateGrounding()
    {
        _isGrounded = false;

        _origin = new Vector2(transform.position.x, transform.position.y - _spriteRenderer.bounds.extents.y);
        _hit = Physics2D.Raycast(_origin, Vector2.down, 0.1f, _layerMask);
        if (_hit.collider)
            _isGrounded = true;

        _origin = new Vector2(transform.position.x - _footOfSet, transform.position.y - _spriteRenderer.bounds.extents.y);
        _hit = Physics2D.Raycast(_origin, Vector2.down, 0.1f, _layerMask);
        if (_hit.collider)
            _isGrounded = true;

        _origin = new Vector2(transform.position.x + _footOfSet, transform.position.y - _spriteRenderer.bounds.extents.y);
        _hit = Physics2D.Raycast(_origin, Vector2.down, 0.1f, _layerMask);
        if (_hit.collider)
            _isGrounded = true;

        if (_isGrounded && GetComponent<Rigidbody2D>().linearVelocityY <= 0)
        {
            _numberOfJumps = 2;
        }
    }

    private void UpdateSprite()
    {
        _animator.SetBool("IsGrounded", _isGrounded);
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