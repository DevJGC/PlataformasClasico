using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _spriteOn;

    [SerializeField] private bool _active;

    // referencia audio source
    [SerializeField] AudioSource audioSource;

    // referencia audio clip
    [SerializeField] AudioClip audioClip;


    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_active)
        {
            _spriteRenderer.sprite = _spriteOn;
            _active = true;
            // playsound
            audioSource.PlayOneShot(audioClip);

            // Informar al GameManager de la nueva posición de checkpoint
            GameManager.Instance.UpdateLastCheckPoint(transform.position);
        }
    }
}

