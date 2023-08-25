using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour
{
    [SerializeField] Image countdownImage; // Arrastra tu objeto de imagen aquí desde el inspector.
    [SerializeField] float countdownDuration = 10f; // Duración del contador en segundos.

    private bool isCountingDown = false;
    private float currentTime;

    [SerializeField] private Moto moto;

    // panel sintiempo
    [SerializeField] private GameObject panelSinTiempo;

    [SerializeField] GameObject colliderFinal;

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
            countdownImage.fillAmount = currentTime / countdownDuration;
        }
    }

    public void StartCountdown()
    {
        // if moto.meta == true return
        if (moto.GetMeta() || moto.GetDead())
        {
            return;
        }

        if (!isCountingDown)
        {
            currentTime = countdownDuration;
            isCountingDown = true;
            StartCoroutine(CountdownCoroutine());
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }

        isCountingDown = false;
        currentTime = 0;
        OnCountdownFinished();
    }

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
