using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelTotalCoins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTotalCoins;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    // byesound
    [SerializeField] private AudioClip byeSound;

    private const string SUPER_JUMP_BOUGHT = "SuperJumpBought";
    private const string SUPER_SPEED_BOUGHT = "SuperSpeedBought";

    private int displayedCoins; // Las monedas que se están mostrando actualmente
    [SerializeField] private float timeToWait = 0.1f; // Tiempo que se espera entre incrementos de monedas

    [SerializeField] private Button buySuperJump; // boton compra SuperJump
    [SerializeField] private Button buySuperSpeed; // boton compra SuperSpeed

    // animator buttonJump
    [SerializeField] private Animator animatorButtonJump;
    // animator buttonSpeed
    [SerializeField] private Animator animatorButtonSpeed;

    void Start()
    {
        displayedCoins = PlayerPrefs.GetInt("DisplayedCoins", 0);
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        UpdateButtonInteractivity(totalCoins); // Actualizar la interactividad de los botones basándose en totalCoins
        UpdateButtonText(); // Actualizar el texto de los botones en el inicio

        if (displayedCoins < totalCoins)
        {
            StartCoroutine(IncrementCoins());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPurchaseValues();
        }
    }

    private void ResetPurchaseValues()
    {
        PlayerPrefs.DeleteKey(SUPER_JUMP_BOUGHT);
        PlayerPrefs.DeleteKey(SUPER_SPEED_BOUGHT);

        // Luego de borrar los valores, actualiza el texto de los botones y su interactividad
        UpdateButtonText();
        UpdateButtonInteractivity(PlayerPrefs.GetInt("TotalCoins", 0));
    }


    private void UpdateButtonInteractivity(int totalCoins)
    {
        bool hasEnoughCoins = totalCoins >= 100;
        bool superJumpBought = PlayerPrefs.GetInt(SUPER_JUMP_BOUGHT, 0) == 1;
        bool superSpeedBought = PlayerPrefs.GetInt(SUPER_SPEED_BOUGHT, 0) == 1;

        buySuperJump.interactable = hasEnoughCoins && !superJumpBought;
        buySuperSpeed.interactable = hasEnoughCoins && !superSpeedBought;

        // Habilitar o deshabilitar el componente Animator en función de si la mejora ha sido comprada y si tienes suficientes monedas
        animatorButtonJump.enabled = hasEnoughCoins && !superJumpBought;
        animatorButtonSpeed.enabled = hasEnoughCoins && !superSpeedBought;
    }



    private void UpdateButtonText()
    {
        bool superJumpBought = PlayerPrefs.GetInt(SUPER_JUMP_BOUGHT, 0) == 1;
        bool superSpeedBought = PlayerPrefs.GetInt(SUPER_SPEED_BOUGHT, 0) == 1;

        if (superJumpBought)
        {
            buySuperJump.GetComponentInChildren<TextMeshProUGUI>().text = "Mejora de Salto Comprada";
            buySuperJump.interactable = false;
                
        }
        else
        {
            buySuperJump.GetComponentInChildren<TextMeshProUGUI>().text = "Super Salto Precio: $100";
        }

        if (superSpeedBought)
        {
            buySuperSpeed.GetComponentInChildren<TextMeshProUGUI>().text = "Mejora de Velocidad Comprada";
            buySuperSpeed.interactable = false;
        }
        else
        {
            buySuperSpeed.GetComponentInChildren<TextMeshProUGUI>().text = "Super Velocidad Precio: $100";
        }
    }

    public void OnBuySuperJumpClicked()
    {
        // playsound
        audioSource.PlayOneShot(byeSound);
        
        UpdateTotalCoins(-100);
        PlayerPrefs.SetInt(SUPER_JUMP_BOUGHT, 1);
        UpdateButtonText();
    }

    public void OnBuySuperSpeedClicked()
    {
        // playsound
        audioSource.PlayOneShot(byeSound);
        
        UpdateTotalCoins(-100);
        PlayerPrefs.SetInt(SUPER_SPEED_BOUGHT, 1);
        UpdateButtonText();
    }

    private void UpdateTotalCoins(int amount)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += amount;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);

        textTotalCoins.text = "$" + totalCoins.ToString();

        UpdateButtonInteractivity(totalCoins);
    }

    private IEnumerator IncrementCoins()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        while (displayedCoins < totalCoins)
        {
            displayedCoins++;
            textTotalCoins.text = "$" + displayedCoins.ToString();

            if (audioClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(audioClip);
            }

            yield return new WaitForSeconds(timeToWait);
        }
    }
}
