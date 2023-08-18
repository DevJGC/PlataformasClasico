using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextAutoHide : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textIntroLevel;
    
    void Start()
    {
        Invoke("HideText", 3f);



    }

 
    void Update()
    {
        
    }

    void HideText()

    {
        // hide text dotweent and destroy on complete
        textIntroLevel.DOFade(0, 2f).OnComplete(() => Destroy(gameObject));
    }


}
