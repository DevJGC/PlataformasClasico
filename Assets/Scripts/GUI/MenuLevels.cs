using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLevels : MonoBehaviour
{
    [SerializeField] GameObject[] levelsButtons;  // Array de botones de niveles.
    [SerializeField] GameObject[] levelsLockImages;  // Array de imágenes de candado.

    // reference audio source
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;

    // reference audio play
    [SerializeField] private AudioClip audioPlay;

    // reference animator
    [SerializeField] private Animator animator;

    private const string MUSIC_STATE_PREF = "MusicState";

    void Start()
    {
        LoadMusicState();

        // Comprobar si es la primera ejecución del juego.
        CheckFirstRun();

        // Verificar qué niveles están desbloqueados.
        CheckLevel();
    }

    public void CheckFirstRun()
    {
        // Si no existe la key "FirstRun", entonces es la primera vez que se ejecuta el juego.
        if (!PlayerPrefs.HasKey("FirstRun"))
        {
            // Establecer que solo el primer nivel está desbloqueado.
            PlayerPrefs.SetInt("Level1", 1);

            // Establecer todos los demás niveles como bloqueados.
            for (int i = 2; i <= levelsLockImages.Length; i++)
            {
                PlayerPrefs.SetInt("Level" + i, 0);
            }

            // Establecer la key "FirstRun" para que no volvamos a entrar en este condicional en futuras ejecuciones.
            PlayerPrefs.SetInt("FirstRun", 1);
        }
    }

    public void CheckLevel()
    {
        for (int i = 0; i < levelsLockImages.Length; i++)
        {
            if (PlayerPrefs.GetInt("Level" + (i + 1)) == 1)
            {
                // Desactivamos la imagen del candado para el nivel desbloqueado.
                levelsLockImages[i].SetActive(false);

                // También aseguramos que el botón esté interactuable.
                levelsButtons[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                // Si el nivel está bloqueado, aseguramos que el botón no sea interactuable.
                levelsButtons[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void GotoLevel(string levelName)
    {
        // Reproduce el audio
        audioSource.PlayOneShot(audioPlay);
        // settrigger exit animator
        animator.SetTrigger("Exit");

        // Comienza una Coroutine que espera 1 segundo antes de cargar la escena
        StartCoroutine(LoadLevelAfterDelay(levelName, 1f));
    }

    IEnumerator LoadLevelAfterDelay(string levelName, float delay)
    {
       
        // Espera por el tiempo definido en "delay" (en este caso, 1 segundo)
        yield return new WaitForSeconds(delay);

        // Carga la escena
        SceneManager.LoadScene(levelName);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetLastCheckPointPosition();
        }
    }

    // music
    private void LoadMusicState()
    {
        int musicState = PlayerPrefs.GetInt(MUSIC_STATE_PREF, 1); // Por defecto, la música está activa
        if (musicState == 0)
        {
            musicSource.volume = 0;
        }
        else
        {
            musicSource.volume = 1;
        }
    }

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
