using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextAutoHide : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textIntroLevel; // text intro level

    void Start()
    {
        Invoke("HideText", 3f); // hide text
    }

 
    void Update()
    {
        
    }

    // hide text
    void HideText()

    {
        // hide text dotweent and destroy on complete
        textIntroLevel.DOFade(0, 2f).OnComplete(() => Destroy(gameObject)); // fade y destruye
    } 


}
