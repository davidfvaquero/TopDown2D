using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance;

    private int itemsRecolectados; // Cambiamos el nombre de la variable
    private int totalItemsInicial; // Para saber el total si lo necesitas

    void Awake()
    {
        Instance = this;
        itemsRecolectados = 0; // Inicializamos en 0
        totalItemsInicial = GameObject.FindGameObjectsWithTag("Item").Length;
    }

    public void ItemRecogido()
    {
        itemsRecolectados++; // Incrementamos en lugar de decrementar
        Debug.Log($"¡Item recolectado! Total: {itemsRecolectados}");

        // Opcional: Mostrar progreso respecto al total inicial
        Debug.Log($"Progreso: {itemsRecolectados}/{totalItemsInicial}");
    }
}
