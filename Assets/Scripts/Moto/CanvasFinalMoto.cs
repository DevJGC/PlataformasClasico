using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasFinalMoto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("GamePlayLevel6");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
