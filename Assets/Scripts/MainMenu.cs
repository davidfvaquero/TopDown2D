using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Configuraci�n de Botones")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        // Asignar funciones a los botones
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        // Cargar la primera escena del juego (ajusta el �ndice seg�n tu configuraci�n)
        SceneManager.LoadScene(1); // Normalmente el men� es escena 0 y el primer nivel 1

        // Opcional: Cargar por nombre en vez de �ndice
        // SceneManager.LoadScene("NombreDeTuPrimerNivel");
    }

    public void QuitGame()
    {
        // Para cerrar la aplicaci�n (funciona en build final, no en el editor)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        Debug.Log("Juego cerrado"); // Solo para prueba en editor
    }

    // Opcional: M�todo para cargar con pantalla de carga
    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            // Aqu� podr�as actualizar una barra de progreso
            yield return null;
        }
    }
}
