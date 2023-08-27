using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        // Si se pulsa Escape, sale del juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
        
    }
    
    // exit button
    public void Exit()
    {
        Application.Quit();
    }
}
