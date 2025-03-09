using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject currentItem;
    public Image icon;

    void Start()
    {
        if (currentItem != null)
        {
            icon.sprite = currentItem.GetComponent<Item>().icon;
            icon.enabled = true;
        }
        else
        {
            icon.enabled = false;
        }
    }

}
