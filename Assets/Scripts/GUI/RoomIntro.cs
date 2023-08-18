using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomIntro : MonoBehaviour
{
    // audio source
    [SerializeField] AudioSource audioSource;
    // audio clip
    [SerializeField] AudioClip audioClip;

    // gameobject puerta
    [SerializeField] GameObject[] door;

    [SerializeField] AudioClip madreClip;

    // animator canvas fade
    [SerializeField] Animator animatorCanvasFade;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // door open
    public void DoorOpen()
    {
        // audio source play one shot
        audioSource.PlayOneShot(audioClip);
        // invoke activedoor
        Invoke("ActiveDoor", 1f);
        audioSource.PlayOneShot(madreClip);
    }

    void ActiveDoor()
    {
        // set active door
        door[0].SetActive(true);
        door[1].SetActive(true);
        // invoke change scene
        Invoke("ChangeScene", 4.7f);

        Invoke("FadeOut", 4.2f);

    }

    void ChangeScene()
    {
        // load scene
        SceneManager.LoadScene("GamePlayLevel1");
    }

    void FadeOut()
    {
        // animator canvas fade set trigger
        animatorCanvasFade.SetTrigger("Out");
    }
    

}
