using UnityEngine;

public class Player : MonoBehaviour
{
    private float _jumpEndTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        float vertical = rigidBody.linearVelocity.y;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpEndTime = Time.time + 1;
        }

        if (Input.GetKey(KeyCode.Space) && _jumpEndTime > Time.time)
        {
            vertical = 5;
        }
        rigidBody.linearVelocity = new Vector2(horizontal, vertical);
    }
}
