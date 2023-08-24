using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] Image countdownImage; // Arrastra tu objeto de imagen aqu� desde el inspector.
    [SerializeField] float countdownDuration = 10f; // Duraci�n del contador en segundos.

    private bool isCountingDown = false;
    private float currentTime;

    void Start()
    {
        StartCountdown(); // Inicia el contador regresivo al comenzar el juego.
    }

    void Update()
    {
        // Actualiza el fillAmount si el contador est� activo.
        if (isCountingDown)
        {
            countdownImage.fillAmount = currentTime / countdownDuration;
        }
    }

    public void StartCountdown()
    {
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
        // Aqu� puedes agregar cualquier l�gica que desees que ocurra cuando el contador termine.
        Debug.Log("�Contador finalizado!");
    }
}
