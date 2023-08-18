using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBoxes : MonoBehaviour
{
    [SerializeField] Animator animPlayer;

    [SerializeField] Rigidbody2D rbPlayer;

    bool oneTime;

    // audio source
    [SerializeField] AudioSource audioSource;
    // audio play
    [SerializeField] AudioClip audioClip;

    void Start()
    {

    }


    void Update()
    {

    }

    // ontriggerenter2d tag player timescale to 0
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !oneTime)
        {
            animPlayer.SetTrigger("isCelebrate");
            oneTime = true;
            // playsound
            audioSource.PlayOneShot(audioClip);
            // add mass rigidbody player
            rbPlayer.mass = 1000;
        }
    }

    // ontriggerexit2d tag player timescale to 1
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // restart mass rigidbody player
            rbPlayer.mass = 1;
            animPlayer.SetTrigger("isCelebrate");
            // destroy this
            Destroy(gameObject.GetComponent<MegaBoxes>());
        }
    }
}