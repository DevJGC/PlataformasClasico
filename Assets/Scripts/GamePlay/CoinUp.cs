using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinUp : MonoBehaviour
{
    // Duración de la animación de movimiento de la moneda
    [SerializeField] float moveDuration = 1f;

    // Referencia al CoinManager
    private CoinManager coinManager;

    // referencia audio source
    private AudioSource audioSource;

    // referencia audio clip
    [SerializeField] private AudioClip audioClip;

    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        // reference CoinManager
        coinManager = FindObjectOfType<CoinManager>();
        MoveCoinToTopRightAndDestroy();
    }

    void Update()
    {

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        // playsound
    //        audioSource.PlayOneShot(audioClip);
    //        coinManager.AddCoin();  // Utiliza la referencia pública directamente
    //        MoveCoinToTopRightAndDestroy();
    //    }
    //}

    private void MoveCoinToTopRightAndDestroy()
    {
        audioSource.PlayOneShot(audioClip);
        coinManager.AddCoin(); 
        // Puedes ajustar este punto para que se adapte a tu UI o configuración de cámara
        Vector2 topRightScreenPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        transform.DOLocalMove(topRightScreenPosition, moveDuration).OnComplete(() => Destroy(gameObject));
    }
}