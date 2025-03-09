using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip finalSceneMusic;
    private int finalSceneIndex;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Hace que persista entre escenas
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = menuMusic;
            audioSource.loop = true;
            audioSource.Play();

            // Establece el índice de la última escena
            finalSceneIndex = SceneManager.sceneCountInBuildSettings - 1;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // Evita duplicados al cambiar de escena
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == finalSceneIndex)
        {
            ChangeMusic(finalSceneMusic);
        }
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (audioSource.clip != newClip)
        {
            audioSource.Stop();
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }
}
