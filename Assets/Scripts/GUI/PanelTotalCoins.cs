using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelTotalCoins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTotalCoins; // Texto que muestra el total de monedas
    [SerializeField] private AudioSource audioSource; //    audio source
    [SerializeField] private AudioClip audioClip; //    audio clip
    // byesound
    [SerializeField] private AudioClip byeSound; //    audio clip

    private const string SUPER_JUMP_BOUGHT = "SuperJumpBought"; //  super jump bought
    private const string SUPER_SPEED_BOUGHT = "SuperSpeedBought"; //    

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
        displayedCoins = PlayerPrefs.GetInt("DisplayedCoins", 0); // Obtener el valor de las monedas mostradas
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); // Obtener el valor de las monedas totales

        UpdateButtonInteractivity(totalCoins); // Actualizar la interactividad de los botones basándose en totalCoins
        UpdateButtonText(); // Actualizar el texto de los botones en el inicio

        //  Si el total de monedas es mayor que las monedas mostradas, incrementar las monedas mostradas
        if (displayedCoins < totalCoins) 
        {
            StartCoroutine(IncrementCoins());
        }
    }

    //  Pulsa R para resetear valores de monedas, para testear
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPurchaseValues();
        }
    }

    //  reset purchase values
    private void ResetPurchaseValues()
    {
        PlayerPrefs.DeleteKey(SUPER_JUMP_BOUGHT);
        PlayerPrefs.DeleteKey(SUPER_SPEED_BOUGHT);

        // Luego de borrar los valores, actualiza el texto de los botones y su interactividad
        UpdateButtonText();
        UpdateButtonInteractivity(PlayerPrefs.GetInt("TotalCoins", 0)); // Actualizar la interactividad de los botones basándose en totalCoins
    }

    // Actualiza botones según las monedas
    private void UpdateButtonInteractivity(int totalCoins)
    {
        bool hasEnoughCoins = totalCoins >= 100; // Si tienes suficientes monedas para comprar una mejora
        bool superJumpBought = PlayerPrefs.GetInt(SUPER_JUMP_BOUGHT, 0) == 1; //    super jump bought
        bool superSpeedBought = PlayerPrefs.GetInt(SUPER_SPEED_BOUGHT, 0) == 1; //  super speed bought

        buySuperJump.interactable = hasEnoughCoins && !superJumpBought; // Habilitar o deshabilitar el botón de compra de super salto
        buySuperSpeed.interactable = hasEnoughCoins && !superSpeedBought; // Habilitar o deshabilitar el botón de compra de super velocidad

        // Habilitar o deshabilitar el componente Animator en función de si la mejora ha sido comprada y si tienes suficientes monedas
        animatorButtonJump.enabled = hasEnoughCoins && !superJumpBought; // animator button jump
        animatorButtonSpeed.enabled = hasEnoughCoins && !superSpeedBought; //   animator button speed
    }


    //  actualiza el texto de los botones según las monedas
    private void UpdateButtonText()
    {
        bool superJumpBought = PlayerPrefs.GetInt(SUPER_JUMP_BOUGHT, 0) == 1; //    super jump bought
        bool superSpeedBought = PlayerPrefs.GetInt(SUPER_SPEED_BOUGHT, 0) == 1; //  super speed bought

        //  Si la mejora ha sido comprada, cambiar el texto del botón a "Mejora comprada"
        if (superJumpBought)
        {
            buySuperJump.GetComponentInChildren<TextMeshProUGUI>().text = "Mejora de Salto Comprada";
            buySuperJump.interactable = false;
                
        }
        else // Si la mejora no ha sido comprada, cambiar el texto del botón a "Super Salto Precio: $100"
        {
            buySuperJump.GetComponentInChildren<TextMeshProUGUI>().text = "Super Salto Precio: $100";
        }

        //  Si la mejora ha sido comprada, cambiar el texto del botón a "Mejora comprada"
        if (superSpeedBought)
        {
            buySuperSpeed.GetComponentInChildren<TextMeshProUGUI>().text = "Mejora de Velocidad Comprada";
            buySuperSpeed.interactable = false;
        }
        else // Si la mejora no ha sido comprada, cambiar el texto del botón a "Super Velocidad Precio: $100"
        {
            buySuperSpeed.GetComponentInChildren<TextMeshProUGUI>().text = "Super Velocidad Precio: $100";
        }
    }

    //  Si se compra la mejora, actualiza el texto de los botones y su interactividad
    public void OnBuySuperJumpClicked()
    {
        // playsound
        audioSource.PlayOneShot(byeSound);

        UpdateTotalCoins(-100); //  resta 100 monedas
        PlayerPrefs.SetInt(SUPER_JUMP_BOUGHT, 1); //    super jump bought
        UpdateButtonText(); //  actualiza el texto de los botones
    }

    //  Si se compra la mejora, actualiza el texto de los botones y su interactividad
    public void OnBuySuperSpeedClicked()
    {
        // playsound
        audioSource.PlayOneShot(byeSound);

        UpdateTotalCoins(-100); //  resta 100 monedas
        PlayerPrefs.SetInt(SUPER_SPEED_BOUGHT, 1); //   super speed bought
        UpdateButtonText(); //  actualiza el texto de los botones
    }

    //  Actualiza el total de monedas
    private void UpdateTotalCoins(int amount)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); //    total coins
        totalCoins += amount; // suma monedas
        PlayerPrefs.SetInt("TotalCoins", totalCoins); //  actualiza playerPrefs

        textTotalCoins.text = "$" + totalCoins.ToString(); //   actualiza el texto de las monedas

        UpdateButtonInteractivity(totalCoins); //   actualiza la interactividad de los botones
    }

    //  Incrementa las monedas
    private IEnumerator IncrementCoins()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); //    actualiza el total de monedas en el playerPrefs

        //  Si el total de monedas es menor que las monedas que se muestran, incrementa las monedas que se muestran
        while (displayedCoins < totalCoins)
        {
            displayedCoins++; //    incrementa las monedas que se muestran
            textTotalCoins.text = "$" + displayedCoins.ToString(); //   actualiza el texto de las monedas

            //  Si hay un audioClip y un audioSource, reproduce el audioClip
            if (audioClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(audioClip);
            }

            yield return new WaitForSeconds(timeToWait); // espera un tiempo
        }
    }
}
