using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class IntroText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI; //    referencia textmeshpro
    [SerializeField] string dialogText; //  dialog text
    [SerializeField] float timeToType; //   time to type

    // audio source
    [SerializeField] AudioSource audioSource;
    // audio clip
    [SerializeField] AudioClip audioClip;

    // referencia audio source music
    [SerializeField] AudioSource audioSourceMusic;

    void Start()
    {
        // textmeshpro text = ""
        textMeshProUGUI.text = ""; 
        // startcoroutine type
        StartCoroutine(Type());

    }

    void Update()
    {
        
    }

    // typewritter
    IEnumerator Type()
    {
        // foreach string in sentences
        foreach (char letter in dialogText.ToCharArray())
        {
            // textmeshpro text += letter
            textMeshProUGUI.text += letter;
            // yield return new waitforseconds
            yield return new WaitForSeconds(timeToType);
            audioSource.PlayOneShot(audioClip);
        }
        // startcoroutine erase
        StartCoroutine(Erase());
    }

    // erase letter to letter
    IEnumerator Erase()
    {
        // foreach string in sentences
        foreach (char letter in dialogText.ToCharArray())
        {
            // textmeshpro text = substring
            textMeshProUGUI.text = textMeshProUGUI.text.Substring(0, textMeshProUGUI.text.Length - 1);
            // yield return new waitforseconds
            yield return new WaitForSeconds(timeToType);
            audioSource.PlayOneShot(audioClip);
        }
        // play audio source music
        audioSourceMusic.Play();

    }
}
