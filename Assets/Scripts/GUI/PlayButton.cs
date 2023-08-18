using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // referencia del animator
    [SerializeField] private Animator animator;

    //audio source
    [SerializeField] private AudioSource audioSource;
    //audio play
    [SerializeField] private AudioClip audioPlay;

    void Start()
    {
        
    }

    
    void Update()
    {
        // key enter play
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Play();
        }

    }

    public void Play()
    {
        // play audio
        audioSource.PlayOneShot(audioPlay);
        animator.SetTrigger("Final");
        Invoke("PlayButtonAnimation", 1f);
    }

    public void PlayButtonAnimation()
    {
        
        SceneManager.LoadScene("MenuLevel");
    }


}
