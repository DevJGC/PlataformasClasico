using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mas1 : MonoBehaviour
{
    // referencia al spriterenderer
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        // starcourrutines y destruye el prefab
        StartCoroutine(FadeOut(1.0f));
        StartCoroutine(MoveUp(1.0f));
        Destroy(gameObject, 1.0f);
    }
    
    void Update()
    {
        
    }

    // hace transparente poco a poco
    public IEnumerator FadeOut(float time)
    {
        float elapsedTime = 0.0f; 
        Color c = spriteRenderer.material.color;

        //  mientras no termine el tiempo, lo hace transparente
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsedTime / time);
            spriteRenderer.material.color = new Color(c.r, c.g, c.b, alpha); // hace transparente poco a poco
            yield return null;
        }
    }

    // mueve hacia arriba poco a poco
    public IEnumerator MoveUp(float time)
    {
        float elapsedTime = 0.0f;
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + Vector3.up * 2.0f;
        
        // mientras no termine el tiempo, sube
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startingPos, finalPos, elapsedTime / time); // mueve hacia arriba poco a poco
            yield return null;
        }
    }

}
