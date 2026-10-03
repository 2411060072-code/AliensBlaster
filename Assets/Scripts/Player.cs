using System.Xml.Serialization;
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

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_origin, _origin + Vector2.down * 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        _origin = new Vector2(transform.position.x, transform.position.y - _spriteRenderer.bounds.extents.y - 0.1f);
        RaycastHit2D hit = Physics2D.Raycast(_origin, Vector2.down, 0.1f);
        if (hit.collider)
        {
            _IsGrounded = true;
        }
        else _IsGrounded = false;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = _rigidBody.linearVelocity.y;

        if (Input.GetKeyDown(KeyCode.Space) && _IsGrounded == true)
        {
            _jumpEndTime = Time.time + _jumpDuration;
        }

        if (Input.GetKey(KeyCode.Space) && _jumpEndTime > Time.time)
        {
            vertical = _jumpVelocity;
        }

        horizontal *= _horizontalVelocity;
        _rigidBody.linearVelocity = new Vector2(horizontal, vertical);
    }
}
