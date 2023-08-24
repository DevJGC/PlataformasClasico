using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] Image countdownImage; // Arrastra tu objeto de imagen aquí desde el inspector.
    [SerializeField] float countdownDuration = 10f; // Duración del contador en segundos.

    private bool isCountingDown = false;
    private float currentTime;

    [SerializeField] private Moto moto;

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
        Debug.Log("¡Contador finalizado!");
    }
}
