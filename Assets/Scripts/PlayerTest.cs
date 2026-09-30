using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private bool _isGrounded = false;
    private float _speed = 1;
    private Rigidbody2D _rigidBody;
    private float _horizontal;
    private float _vertical;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "grassMid_0")
        {
            Debug.Log("player is on the ground");
            _isGrounded = true;
        }
    }

    private void PlayerSprint() 
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 5;
            _rigidBody.linearVelocity = new Vector2(_horizontal * _speed, _vertical);
        }
    }

    void Update()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = _rigidBody.linearVelocity.y;

        if (Input.GetKey(KeyCode.Space) && _isGrounded == true)
        {
            _vertical = 5;
            _isGrounded = false;
        }

        _rigidBody.linearVelocity = new Vector2(_horizontal, _vertical);
        PlayerSprint();
    }
}
