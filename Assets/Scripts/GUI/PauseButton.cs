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
        // press p
        if (Input.GetKeyDown(KeyCode.P))
        {
            ActivePause();
        }

    }

    public void ActivePause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            textPause.SetActive(true);
            buttonPause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            textPause.SetActive(false);
            buttonPause.SetActive(false);
        }
    }
}
