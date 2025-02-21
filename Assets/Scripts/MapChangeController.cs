using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "ArenaMap"; // Nombre de la escena destino
    [SerializeField] private GameObject dialogPanel; // Referencia al panel de diálogo UI

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Muestra el diálogo
            dialogPanel.SetActive(true);
            Time.timeScale = 0f; // Pausa el juego (opcional)
            Confirm();
        }
    }

    // Llamado desde el botón "Sí" del diálogo
    public void Confirm()
    {
        Time.timeScale = 1f; // Reanuda el juego si estaba pausado
        SceneManager.LoadScene(nextSceneName);
        dialogPanel.SetActive(false);
    }

    // Llamado desde el botón "No" del diálogo
    public void Cancel()
    {
        Time.timeScale = 1f;
        dialogPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}