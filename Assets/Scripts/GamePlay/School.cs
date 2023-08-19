using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : MonoBehaviour
{
    [SerializeField] private GameObject canvasSchool;

    // Título en el editor
    [Header("Numero Level Siguiente")]
    [SerializeField] private int i;  // número de nivel

    // Referencia al animator del player
    [SerializeField] private Animator animatorPlayer;

    // Referencia al CoinManager
    [SerializeField] private CoinManager coinManager;

    void Start()
    {
        // Asumiendo que no necesitas nada aquí por ahora
    }

    void Update()
    {
        // Asumiendo que no necesitas nada aquí por ahora
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().inSchool = true;
            Invoke("CompletedLevel", 1f);
            // Set level i to 1
            PlayerPrefs.SetInt("Level" + i, 1);
        }
    }

    void CompletedLevel()
    {
        // set animator player
        animatorPlayer.SetTrigger("Final");
        canvasSchool.SetActive(true);

        // Sumar las monedas del nivel actual a las monedas totales
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += coinManager.GetCurrentCoinCount();
        PlayerPrefs.SetInt("TotalCoins", totalCoins);

        // Guardar el valor de las monedas mostradas antes de sumar las del nivel actual
        PlayerPrefs.SetInt("DisplayedCoins", totalCoins - coinManager.GetCurrentCoinCount());

        // Reiniciar el contador de monedas para el siguiente nivel
        coinManager.ResetCoinCount();
    }

}
