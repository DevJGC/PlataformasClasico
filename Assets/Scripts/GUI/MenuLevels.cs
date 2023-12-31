using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLevels : MonoBehaviour
{
    [SerializeField] GameObject[] levelsButtons;  // Array de botones de niveles.
    [SerializeField] GameObject[] levelsLockImages;  // Array de im�genes de candado.

    // reference audio source
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;

    // reference audio play
    [SerializeField] private AudioClip audioPlay;

    // reference animator
    [SerializeField] private Animator animator;

    private const string MUSIC_STATE_PREF = "MusicState"; //    key para guardar el estado de la m�sica.

    void Start()
    {
        LoadMusicState();

        // Comprobar si es la primera ejecuci�n del juego.
        CheckFirstRun();

        // Verificar qu� niveles est�n desbloqueados.
        CheckLevel();
    }

    //  Gestionar las variables PlayerPrefs de niveles desbloqueados.
    public void CheckFirstRun()
    {
        // Si no existe la key "FirstRun", entonces es la primera vez que se ejecuta el juego.
        if (!PlayerPrefs.HasKey("FirstRun"))
        {
            // Establecer que solo el primer nivel est� desbloqueado.
            PlayerPrefs.SetInt("Level1", 1);

            // Establecer todos los dem�s niveles como bloqueados.
            for (int i = 2; i <= levelsLockImages.Length; i++)
            {
                PlayerPrefs.SetInt("Level" + i, 0);
            }

            // Establecer la key "FirstRun" para que no volvamos a entrar en este condicional en futuras ejecuciones.
            PlayerPrefs.SetInt("FirstRun", 1);
        }
    }

    //  chequea los niveles desbloqueados.
    public void CheckLevel()
    {
        for (int i = 0; i < levelsLockImages.Length; i++)
        {
            if (PlayerPrefs.GetInt("Level" + (i + 1)) == 1)
            {
                // Desactivamos la imagen del candado para el nivel desbloqueado.
                levelsLockImages[i].SetActive(false);

                // Tambi�n aseguramos que el bot�n est� interactuable.
                levelsButtons[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                // Si el nivel est� bloqueado, aseguramos que el bot�n no sea interactuable.
                levelsButtons[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    //  carga el nivel seleccionado.
    public void GotoLevel(string levelName)
    {
        // Reproduce el audio
        audioSource.PlayOneShot(audioPlay);
        // settrigger exit animator
        animator.SetTrigger("Exit");

        // Comienza una Coroutine que espera 1 segundo antes de cargar la escena
        StartCoroutine(LoadLevelAfterDelay(levelName, 1f));
    }

    //  carga el nivel seleccionado con retardo
    IEnumerator LoadLevelAfterDelay(string levelName, float delay)
    {
       
        // Espera por el tiempo definido en "delay" (en este caso, 1 segundo)
        yield return new WaitForSeconds(delay);

        // Carga la escena
        SceneManager.LoadScene(levelName);

        if (GameManager.Instance != null) // Si el GameManager existe, entonces estamos en un nivel
        {
            GameManager.Instance.ResetLastCheckPointPosition(); //  Resetea la posici�n del �ltimo checkpoint
        }
    }

    // music
    private void LoadMusicState()
    {
        int musicState = PlayerPrefs.GetInt(MUSIC_STATE_PREF, 1); // Por defecto, la m�sica est� activa
        if (musicState == 0)
        {
            musicSource.volume = 0;
        }
        else
        {
            musicSource.volume = 1;
        }
    }

    // Si se pulsa R se borran los valores PlayerPrefs para Testear
    private void Update()
    {
        // key Reset
        if (Input.GetKey(KeyCode.R))
        {
            // Resetea prefabs niveles
            for (int i = 2; i <= 6; i++)
            {
                PlayerPrefs.SetInt("Level" + i, 0);
            }

            // Resetea las monedas
            PlayerPrefs.SetInt("DisplayedCoins", 0);
            PlayerPrefs.SetInt("TotalCoins", 0);

            Debug.Log("Reset");
        }
    }

}
