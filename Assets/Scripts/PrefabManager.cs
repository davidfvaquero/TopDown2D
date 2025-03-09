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

    void Start()
    {
        Debug.Log("PrefabManager Start");

        if (victoryPanel == null)
        {
            Debug.Log("Buscando VictoryPanel dentro de UI...");

            GameObject ui = GameObject.Find("UI"); // Buscar el objeto UI
            if (ui != null)
            {
                Debug.Log("UI encontrado correctamente.");
                victoryPanel = ui.transform.Find("VictoryPanel")?.gameObject;
            }
        }

        if (victoryPanel != null)
        {
            Debug.Log("VictoryPanel encontrado correctamente.");
            victoryPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("VictoryPanel sigue sin encontrarse.");
        }
    }
    void Awake()
    {
        Debug.Log("PrefabManager Awake");

        Instance = this;
        itemsRecolectados = 0; // Inicializamos en 0

        // Buscar VictoryPanel directamente por su nombre en la jerarquía
        if (victoryPanel == null)
        {
            victoryPanel = GameObject.Find("VictoryPanel");
        }

        if (victoryPanel == null)
        {
            Debug.LogError(" VictoryPanel no encontrado en la escena.");
        }
        else
        {
            victoryPanel.SetActive(false);

            // Buscar el botón dentro de VictoryPanel
            nextLevelButton = victoryPanel.GetComponentInChildren<Button>();
        }

        if (nextLevelButton == null)
        {
            Debug.LogError(" Next Level Button no encontrado en VictoryPanel.");
        }
        else
        {
            Debug.Log("Next Level Button encontrado.");
        }

        totalItemsInicial = GameObject.FindGameObjectsWithTag("Item").Length;
        Debug.Log($"Total de items en la escena: {totalItemsInicial}");

        ConfigureUI();
    }

    void ConfigureUI()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        // Configurar el botón de siguiente nivel
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
        Debug.Log($"¡Item recolectado! Total: {itemsRecolectados}");

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
            Debug.Log("Pantalla de victoria mostrada.");
        }
        else
        {
            Debug.LogError("VictoryPanel no está asignado.");
        }
    }

    private void LoadNextLevel()
    {
        Time.timeScale = 1f; // Restaurar el tiempo
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Verificar si hay más escenas
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No hay más niveles - Volviendo al menú principal");
            SceneManager.LoadScene(0); // Cargar menú principal
        }
    }
}
