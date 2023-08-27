using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasFinalMoto : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        // si se pulsa enter reiniciar
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Reiniciar();
        }

    }

    // volver a jugar nivel moto
    public void Reiniciar()
    {
        SceneManager.LoadScene("GamePlayLevel6");
    }

    // volver al menú principal
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
