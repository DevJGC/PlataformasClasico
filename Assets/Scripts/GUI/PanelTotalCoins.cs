using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelTotalCoins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTotalCoins;

    // sounds
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private int displayedCoins; // Las monedas que se están mostrando actualmente

    [SerializeField] private float timeToWait = 0.1f; // Tiempo que se espera entre incrementos de monedas

    [SerializeField] private Button buySuperJump;
    [SerializeField] private Button buySuperSpeed;

    void Start()
    {
        displayedCoins = PlayerPrefs.GetInt("DisplayedCoins", 0);
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        UpdateButtonInteractivity(totalCoins); // Actualizar la interactividad de los botones basándose en totalCoins

        if (displayedCoins < totalCoins)
        {
            StartCoroutine(IncrementCoins());
        }
    }

    private void UpdateButtonInteractivity(int totalCoins)
    {
        bool isInteractable = totalCoins >= 100;
        buySuperJump.interactable = isInteractable;
        buySuperSpeed.interactable = isInteractable;
    }

    public void OnBuySuperJumpClicked()
    {
        UpdateTotalCoins(-100); // Restar 100 monedas
    }

    public void OnBuySuperSpeedClicked()
    {
        UpdateTotalCoins(-100); // Restar 100 monedas
    }

    private void UpdateTotalCoins(int amount)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += amount;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);

       // textTotalCoins.text = totalCoins.ToString(); // Actualizar el texto de monedas en UI
        textTotalCoins.text = "$" + totalCoins.ToString();

        UpdateButtonInteractivity(totalCoins); // Actualizar la interactividad de los botones basándose en el nuevo totalCoins
    }

    private IEnumerator IncrementCoins()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        while (displayedCoins < totalCoins)
        {
            displayedCoins++;
            //textTotalCoins.text = displayedCoins.ToString();
            textTotalCoins.text = "$" + displayedCoins.ToString();

            // Si tienes un sonido para cuando se incrementa el coin, puedes reproducirlo aquí
            if (audioClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(audioClip);
            }

            yield return new WaitForSeconds(timeToWait);
        }
    }
}
