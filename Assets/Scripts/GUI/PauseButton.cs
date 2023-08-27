using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    // referencia button
    [SerializeField] GameObject buttonPause;

    // referencia text pause
    [SerializeField] GameObject textPause;

    void Start()
    {
        
    }

    
    void Update()
    {
        // press p para activar Pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            ActivePause();
        }

    }

    //  active pause
    public void ActivePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0; //  pausa el tiempo
            textPause.SetActive(true); //   activa el texto
            buttonPause.SetActive(true); // activa el boton
        }
        else
        {
            Time.timeScale = 1; //  activa el tiempo
            textPause.SetActive(false); //  desactiva el texto
            buttonPause.SetActive(false); //    desactiva el boton
        }
    }
}
