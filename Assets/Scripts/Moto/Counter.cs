using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    [SerializeField] Image countdownImage; // imagen tiempo restante.
    [SerializeField] float countdownDuration = 10f; // Duración del contador en segundos.

    private bool isCountingDown = false; // Indica si el contador está activo.
    private float currentTime; //   Tiempo actual del contador.

    [SerializeField] private Moto moto; //  referencia script moto

    // panel sintiempo
    [SerializeField] private GameObject panelSinTiempo;

    [SerializeField] GameObject colliderFinal; //   referencia collider final (meta)

    // sound
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip audioClip;

    
    void Start()
    {
        StartCountdown(); // Inicia el contador regresivo al comenzar el juego.
    }

    void Update()
    {
        // Actualiza el fillAmount si el contador está activo.
        if (isCountingDown)
        {
            countdownImage.fillAmount = currentTime / countdownDuration; // Actualiza el fillAmount de la imagen.
        }
    }

    //  Restablece el contador a su duración inicial.
    public void StartCountdown()
    {
        // if moto.meta == true return
        if (moto.GetMeta() || moto.GetDead()) //    Si la moto ha llegado a la meta o está muerta, no inicia el contador.
        {
            return;
        }

        if (!isCountingDown) //  Si el contador no está activo, lo inicia.
        {
            currentTime = countdownDuration; //  Establece el tiempo actual al valor de la duración del contador.
            isCountingDown = true; //    Activa el contador.
            StartCoroutine(CountdownCoroutine());
        }
    }

    //  Restablece el contador a su duración inicial.
    private IEnumerator CountdownCoroutine()
    {
        // Espera hasta que el tiempo actual sea 0.
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }

        isCountingDown = false;
        currentTime = 0;
        OnCountdownFinished();
    }

    //  final de contador   
    private void OnCountdownFinished()
    {
        // Aquí puedes agregar cualquier lógica que desees que ocurra cuando el contador termine.
        // activar sintiempo
        panelSinTiempo.SetActive(true);
        // invoke restarlevel 2 seconds
        StartCoroutine(RestartLevelWithDelay(2f));
        //Time.timeScale = 0.5f;
        // deactive colliderFinal
        colliderFinal.SetActive(false);
        // play sound
        audioSource.PlayOneShot(audioClip);

        Debug.Log("¡Contador finalizado!");
    }

    //  restart level with delay
    IEnumerator RestartLevelWithDelay(float delay)
    {
        float elapsedTime = 0f;
        while (elapsedTime < delay)
        {
            elapsedTime += Time.unscaledDeltaTime; // Usa unscaledDeltaTime para contar el tiempo sin tener en cuenta la escala del tiempo
            yield return null;
        }
        RestartLevel();
    }

    // restart level
    private void RestartLevel()
    {
        // reload level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }


}
