using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Patron singleton

    private Vector3 lastCheckPointPosition; //  Ultimo checkpoint

    //  Awake se ejecuta antes que Start
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Esto asegura que el GameManager persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //  Actualiza la posicion del ultimo checkpoint
    public void UpdateLastCheckPoint(Vector3 newCheckPointPosition)
    {
        lastCheckPointPosition = newCheckPointPosition;
    }

    //  Devuelve la posicion del ultimo checkpoint
    public Vector3 GetLastCheckPointPosition()
    {
        return lastCheckPointPosition;
    }

    //  Resetea la posicion del ultimo checkpoint
    public void ResetLastCheckPointPosition()
    {
        lastCheckPointPosition = Vector3.zero;
    }

}
