using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton pattern

    private Vector3 lastCheckPointPosition;

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

    public void UpdateLastCheckPoint(Vector3 newCheckPointPosition)
    {
        lastCheckPointPosition = newCheckPointPosition;
    }

    public Vector3 GetLastCheckPointPosition()
    {
        return lastCheckPointPosition;
    }

    public void ResetLastCheckPointPosition()
    {
        lastCheckPointPosition = Vector3.zero;
    }

}
