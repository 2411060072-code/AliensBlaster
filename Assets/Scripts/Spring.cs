using System;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    private Sprite _defaultSprite;
    [SerializeField] private Sprite _sprungSprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _defaultSprite = _spriteRenderer.sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) 
        {
            _spriteRenderer.sprite = _sprungSprite;
            _audioSource.Play();
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
