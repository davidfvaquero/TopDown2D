using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    private string saveLoc;
    private GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        saveLoc = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadGame();
    }

    public void saveGame()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        SaveData saveData = new SaveData
        {
            playerPosition = player.transform.position,
            sceneName = SceneManager.GetActiveScene().name
        };

        File.WriteAllText(saveLoc, JsonUtility.ToJson(saveData));
    }

    public void loadGame()
    {
        if (File.Exists(saveLoc))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLoc));

            if (SceneManager.GetActiveScene().name != saveData.sceneName)
            {
                SceneManager.LoadScene(saveData.sceneName);
            }
            else
            {

            }
        }
        else
        {
            saveGame();
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(SetPositionAfterLoad());
    }

    IEnumerator SetPositionAfterLoad()
    {
        yield return null;

        if (File.Exists(saveLoc))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLoc));
            SetPlayerPosition(saveData.playerPosition);
        }
    }

    private void SetPlayerPosition(Vector3 position)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = position;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}