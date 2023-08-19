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

    private void UpdateCoinCounterUI()
    {
        coinCounterText.text = coinCount.ToString(); // Solo muestra el número
    }

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
