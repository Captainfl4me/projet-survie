using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    [Header("Inventory Panel References")] 
    [SerializeField] private InventoryUIManager inventoryPanel;
    [SerializeField] private Sprite emptyVisual;

    [SerializeField] private Transform dropPoint;
    
    [SerializeField] private List<ItemData> content = new List<ItemData>();
    
    [Header("Action Panel References")] 
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private GameObject useItemButton;
    [SerializeField] private GameObject equipItemButton;
    [SerializeField] private GameObject dropItemButton;
    [SerializeField] private GameObject destroyItemButton;

    private ItemData currItem;

    [Header("Equipment Panel References")] 
    [SerializeField] private EquipmentLibrary equipmentLibrary;
    [SerializeField] private GameObject equipmentPanel;

    [SerializeField] private EquipmentSlot headSlotImage;
    [SerializeField] private EquipmentSlot chestSlotImage;
    [SerializeField] private EquipmentSlot handsSlotImage;
    [SerializeField] private EquipmentSlot legsSlotImage;
    [SerializeField] private EquipmentSlot feetSlotImage;
    
    public static Inventory instance;

    private bool isOpen = false;
    
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        RefreshContent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;

            if (isOpen)
                OpenInventory();
            else
                CloseInventory();
        }
    }
    
    public void AddItem(ItemData item)
    {
        content.Add(item);
        RefreshContent();
    }

    private void OpenInventory()
    {
        inventoryPanel.transform.gameObject.SetActive(true);
        equipmentPanel.SetActive(true);
    }
    
    public void CloseInventory()
    {
        inventoryPanel.transform.gameObject.SetActive(false);
        equipmentPanel.SetActive(false);
        CloseActionPanel();
        TooltipSystem.instance.Hide();
    }

    public void CloseActionPanel()
    {
        actionPanel.SetActive(false);
        currItem = null;
    }

    public bool IsFull()
    { 
        return content.Count >= inventoryPanel.GetInventorySize();
    }
    
    private void RefreshContent()
    {
        Slot[] allSlot = inventoryPanel.GetInventorySlots();
        
        for (int i = 0; i < allSlot.Length; i++)
        {
            if(i < content.Count)
                allSlot[i].setItem(content[i]);
            else
                allSlot[i].setItem(null, emptyVisual);
        }
    }

    public void OpenActionPanel(ItemData item, Vector2 position)
    {
        currItem = item;
        if (item == null)
        {
            actionPanel.SetActive(false);
            return;
        }

        switch (item.itemType)
        {
            case ItemType.Ressource:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(false);
                break;
            case ItemType.Equipement:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(true);
                break;
            case ItemType.Consumable:
                useItemButton.SetActive(true);
                equipItemButton.SetActive(false);
                break;
        }
        
        actionPanel.SetActive(true);
        actionPanel.transform.position = position;
    }
    
    public void UseActionButton()
    {
        RefreshContent();
        CloseActionPanel();
    }
    public void EquipActionButton()
    {
        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.equipmentLibrary.Single(e => e.itemData == currItem);

        if (equipmentLibraryItem != null)
        {
            content.Remove(currItem);
            RefreshContent();
            
            switch (currItem.equipmentType)
            {
                case EquipmentType.Head:
                    headSlotImage.Equip(currItem);
                    break;
                case EquipmentType.Chest:
                    chestSlotImage.Equip(currItem);
                    break;
                case EquipmentType.Hands:
                    handsSlotImage.Equip(currItem);
                    break;
                case EquipmentType.Legs:
                    legsSlotImage.Equip(currItem);
                    break;
                case EquipmentType.Feet:
                    feetSlotImage.Equip(currItem);
                    break;
            }

            foreach (var equipmentToDisable in equipmentLibraryItem.elementsToDisable)
            {
                equipmentToDisable.SetActive(false);
            }

            equipmentLibraryItem.itemPrefab.SetActive(true);
            
            CloseActionPanel();
        }
        else
            Debug.LogError("Item not found in list!!");
    }
    
    public void DesequipActionButton(ItemData equipItem)
    {
        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.equipmentLibrary.Single(e => e.itemData == equipItem);

        if (equipmentLibraryItem != null)
        {
            foreach (var equipmentToDisable in equipmentLibraryItem.elementsToDisable)
            {
                equipmentToDisable.SetActive(true);
            }

            equipmentLibraryItem.itemPrefab.SetActive(false);
        }
        else
            Debug.LogError("Item not found in list!!");
    }
    
    public void DropActionButton()
    {
        Instantiate(currItem.prefab, dropPoint.position, Quaternion.identity);
        DestroyActionButton();
    }
    public void DestroyActionButton()
    {
        content.Remove(currItem);
        RefreshContent();
        CloseActionPanel();
    }
}
