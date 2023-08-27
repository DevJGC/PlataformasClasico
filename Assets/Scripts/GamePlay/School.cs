using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : MonoBehaviour
{
    [SerializeField] private GameObject canvasSchool; //    canvas final nivel

    // Título en el editor
    [Header("Numero Level Siguiente")]
    [SerializeField] private int i;  // número de nivel

    // Referencia al animator del player
    [SerializeField] private Animator animatorPlayer;

    // Referencia al CoinManager
    [SerializeField] private CoinManager coinManager;

    void Start()
    {
      
    }

    void Update()
    {

    }

    // trigger si player llega a la escuela
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().inSchool = true; //    player en escuela
            Invoke("CompletedLevel", 1f); //    invocar función
            // Set level i to 1
            PlayerPrefs.SetInt("Level" + i, 1); //  guardar nivel
        }
    }

    // función completar nivel
    void CompletedLevel()
    {
        animatorPlayer.SetTrigger("Final"); //  animación final
        canvasSchool.SetActive(true); //    activar canvas final

        // Sumar las monedas del nivel actual a las monedas totales
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); //    total monedas
        totalCoins += coinManager.GetCurrentCoinCount(); // suma las monedeas al manager    
        PlayerPrefs.SetInt("TotalCoins", totalCoins); //    guardar total monedas

        // Guardar el valor de las monedas mostradas antes de sumar las del nivel actual
        PlayerPrefs.SetInt("DisplayedCoins", totalCoins - coinManager.GetCurrentCoinCount());

        // Reiniciar el contador de monedas para el siguiente nivel
        coinManager.ResetCoinCount();
    }

}
