using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer; // referencia al sprite renderer
    [SerializeField] private Sprite _spriteOn; // referencia al sprite on

    [SerializeField] private bool _active; // referencia al bool active (una vez)

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

    // Si el jugador entra en el trigger, el checkpoint se activa
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_active)
        {
            // Cambiar el sprite
            _spriteRenderer.sprite = _spriteOn;
            _active = true;
            // playsound
            audioSource.PlayOneShot(audioClip);

            // Informar al GameManager de la nueva posición de checkpoint
            GameManager.Instance.UpdateLastCheckPoint(transform.position);
        }
    }
}

