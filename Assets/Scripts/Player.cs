using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        var vertical = rigidBody.linearVelocity.y;
        if (Input.GetKeyDown(KeyCode.Space)) vertical = 5;
        rigidBody.linearVelocity = new Vector2(horizontal, vertical);
    }
}
