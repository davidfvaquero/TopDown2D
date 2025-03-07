using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID;
    public string itemName;

    public virtual void Pickup()
    {
        Sprite itemIcon = GetComponent<Image>().sprite;

        if (ItemPickUpUIController.Instance != null)
        {
            ItemPickUpUIController.Instance.ShowItemPickup(itemName, itemIcon);
        }
    }
}
