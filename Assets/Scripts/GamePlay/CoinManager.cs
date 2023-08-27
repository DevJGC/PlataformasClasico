using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    // Esta es una referencia al componente TextMeshProUGUI en tu canvas
    public TextMeshProUGUI coinCounterText;
    private int coinCount = 0;

    // Método para agregar monedas
    public void AddCoin()
    {
        coinCount++;
        UpdateCoinCounterUI();
    }

    // Método para actualizar el texto de la UI
    private void UpdateCoinCounterUI()
    {
        coinCounterText.text = coinCount.ToString(); // Solo muestra el número
    }

    // Esta función devuelve el número actual de monedas
    public int GetCurrentCoinCount()
    {
        return coinCount;
    }

    // Esta función reinicia el contador de monedas para el siguiente nivel
    public void ResetCoinCount()
    {
        coinCount = 0;
       // UpdateCoinCounterUI();
    }
}
