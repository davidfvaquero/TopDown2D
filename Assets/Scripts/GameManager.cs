using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector2 spawnPosition;


    private void Start()
    {
        if (GameManager.Instance != null)
        {
            transform.position = GameManager.Instance.GetSpawnPosition();
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSpawnPosition(Vector2 position)
    {
        spawnPosition = position;
    }

    public Vector2 GetSpawnPosition()
    {
        return spawnPosition;
    }
}