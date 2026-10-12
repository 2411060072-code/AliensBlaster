using UnityEngine;

public class Spring : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Sprite _defaultSprite;
    [SerializeField] private Sprite _sprungSprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) 
        {
            _spriteRenderer.sprite = _sprungSprite;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _spriteRenderer.sprite = _defaultSprite;
        }
    }
}
