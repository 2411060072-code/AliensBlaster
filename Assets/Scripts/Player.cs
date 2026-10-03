using UnityEngine;

public class Player : MonoBehaviour
{
    private float _jumpEndTime;
    [SerializeField] private float _jumpVelocity = 5;
    [SerializeField] private float _jumpDuration = 0.5f;

    private void OnDrawGizmos()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float halfY = spriteRenderer.bounds.extents.y;
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - halfY);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + Vector2.down * 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        float vertical = rigidBody.linearVelocity.y;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpEndTime = Time.time + _jumpDuration;
        }

        if (Input.GetKey(KeyCode.Space) && _jumpEndTime > Time.time)
        {
            vertical = _jumpVelocity;
        }
        rigidBody.linearVelocity = new Vector2(horizontal, vertical);
    }
}
