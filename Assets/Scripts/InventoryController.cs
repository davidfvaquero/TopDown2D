using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;
    public List<Slot> slots = new List<Slot>();

    void Awake()
    {
        // Evitar duplicados al cargar nuevas escenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
        }
        else
        {
            Destroy(gameObject); // Destruir copias
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeSlots();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void InitializeSlots()
    {
        // Destruir slots antiguos si existen
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
        slots.Clear();

        // Crear nuevos slots
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, inventoryPanel.transform);
            Slot slot = slotObj.GetComponent<Slot>();
            slots.Add(slot);
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        // Find the first empty slot and add the item to it
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }
        return false;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject newInventoryPanel = GameObject.Find("UI/Menu/Pages/InventoryPage");
        if (newInventoryPanel != null)
        {
            inventoryPanel = newInventoryPanel;
            InitializeSlots();
        }
        else
        {
            Debug.LogWarning("InventoryPage no encontrado en la nueva escena.");
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
