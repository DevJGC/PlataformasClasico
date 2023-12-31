using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    // Esta es una referencia al componente TextMeshProUGUI en tu canvas
    public TextMeshProUGUI coinCounterText;
    private int coinCount = 0;

    // M�todo para agregar monedas
    public void AddCoin()
    {
        coinCount++;
        UpdateCoinCounterUI();
    }

    // M�todo para actualizar el texto de la UI
    private void UpdateCoinCounterUI()
    {
        coinCounterText.text = coinCount.ToString(); // Solo muestra el n�mero
    }

    // Esta funci�n devuelve el n�mero actual de monedas
    public int GetCurrentCoinCount()
    {
        return coinCount;
    }

    // Esta funci�n reinicia el contador de monedas para el siguiente nivel
    public void ResetCoinCount()
    {
        coinCount = 0;
       // UpdateCoinCounterUI();
    }
}
