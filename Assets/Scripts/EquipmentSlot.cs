using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private Image visualImage;
    [SerializeField] private GameObject desequipButton;
    public Sprite transparentImage;

    private ItemData currItem;

    public void Equip(ItemData item)
    {
        if (currItem != null)
        {
            Desequip();
        }
        
        currItem = item;
        visualImage.sprite = currItem.visual;
        desequipButton.SetActive(true);
    }

    public void Desequip()
    {
        if (Inventory.instance.IsFull())
        {
            Debug.Log("Inventory is full !!");
            return;
        }
        desequipButton.SetActive(false);
        visualImage.sprite = transparentImage;

        Inventory.instance.DesequipActionButton(currItem);
        Inventory.instance.AddItem(currItem);
        currItem = null;
    }
}
