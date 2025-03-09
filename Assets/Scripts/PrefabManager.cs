using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Button nextLevelButton;

    private int itemsRecolectados;
    private int totalItemsInicial;
    void Awake()
    {
        Instance = this;
        itemsRecolectados = 0; // Inicializamos en 0
        totalItemsInicial = GameObject.FindGameObjectsWithTag("Item").Length;

        ConfigureUI();
    }

    void ConfigureUI()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        // Configurar el bot�n de siguiente nivel
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.RemoveAllListeners();
            nextLevelButton.onClick.AddListener(LoadNextLevel);
        }
        else
        {
            Debug.LogWarning("Next Level Button no asignado en el inspector");
        }
    }

    public void ItemRecogido()
    {
        itemsRecolectados++;
        Debug.Log($"�Item recolectado! Total: {itemsRecolectados}");

        if (itemsRecolectados >= totalItemsInicial)
        {
            MostrarVictoria();
        }
    }

    private void MostrarVictoria()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void LoadNextLevel()
    {
        Time.timeScale = 1f; // Restaurar el tiempo
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Verificar si hay m�s escenas
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No hay m�s niveles - Volviendo al men� principal");
            SceneManager.LoadScene(0); // Cargar men� principal
        }
    }
}
