using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class IntroText : MonoBehaviour
{
    // referencia text tmpro
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    // referencia string
    [SerializeField] string dialogText;
    [SerializeField] float timeToType;

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

    // Update is called once per frame
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
