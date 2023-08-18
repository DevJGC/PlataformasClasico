using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    // referencia al rigidbody
    [SerializeField] Rigidbody2D rb;

    [SerializeField] GameObject prefabCoin;

    [SerializeField] GameObject prefabExplosion;

    bool onlyOne;

    // Variable para almacenar el n�mero del layer que deseas chequear.
    [SerializeField] LayerMask groundLayer;

    // referencia 4 prefabs de trozos
    [SerializeField] GameObject[] prefabChunk;



    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !onlyOne || collision.gameObject.tag == "Barrel" && !onlyOne)
        {
            rb.gravityScale = 1;
            rb.isKinematic = false;
            onlyOne = true;
            CoinWin();
            Explosion();
        }

        // Chequeo del Layer en lugar del tag
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            Destroy(gameObject, 0.1f);
        }
    }

    private void CoinWin()
    {
        Instantiate(prefabCoin, transform.position, Quaternion.identity);

    }

    private void Explosion()
    {
        // Instanciar el prefab de la explosi�n
        Instantiate(prefabExplosion, transform.position, Quaternion.identity);

        // Para cada trozo en el array, inst�ncialo y dale un empuje en una direcci�n.
        foreach (GameObject chunk in prefabChunk)
        {
            GameObject instancedChunk = Instantiate(chunk, transform.position, Quaternion.identity);
            Rigidbody2D chunkRb = instancedChunk.GetComponent<Rigidbody2D>();

            if (chunkRb) // Asegur�ndonos de que el trozo tenga un Rigidbody2D
            {
                // Dar un empuje al trozo. Puedes ajustar la fuerza y direcci�n seg�n necesites.
                float randomX = Random.Range(-1f, 1f);
                float randomY = Random.Range(-1f, 1f);
                Vector2 randomDirection = new Vector2(randomX, randomY).normalized;
                float forceMagnitude = 1.5f; // Ajusta este valor seg�n la fuerza que quieras

                chunkRb.AddForce(randomDirection * forceMagnitude, ForceMode2D.Impulse);
            }
        }
    }


}
