using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School : MonoBehaviour
{
    [SerializeField] GameObject canvasSchool;

    // title editor
    [Header("Numero Level Siguiente")]
    [SerializeField] int i;// numero de nivel

    // referencia animator player
    [SerializeField] Animator animatorPlayer;

    void Start()
    {

    }


    void Update()
    {

    }

    // ontriggerenter2d tag player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().inSchool = true;
            Invoke("CompletedLevel", 1f);
            // set level i to 1
            PlayerPrefs.SetInt("Level"+i, 1);
        }
    }

    void CompletedLevel()
    {
        // set animator player
        // set trigger Final
        animatorPlayer.SetTrigger("Final");
        canvasSchool.SetActive(true);
        
    }

}    
