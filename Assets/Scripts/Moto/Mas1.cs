using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mas1 : MonoBehaviour
{
    // referencia al spriterenderer
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        // starcourrutine
        StartCoroutine(FadeOut(1.0f));
        StartCoroutine(MoveUp(1.0f));
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // fade to transparent

    public IEnumerator FadeOut(float time)
    {
        float elapsedTime = 0.0f;
        Color c = spriteRenderer.material.color;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / time);
            spriteRenderer.material.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }
    }

    // move up
    public IEnumerator MoveUp(float time)
    {
        float elapsedTime = 0.0f;
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + Vector3.up * 2.0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startingPos, finalPos, elapsedTime / time);
            yield return null;
        }
    }

}
