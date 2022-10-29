using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> _content = new List<ItemData>();
    
    [SerializeField] 
    private InventoryUIManager _inventoryPanel;
    [SerializeField] private Sprite _emptyVisual;

    public static Inventory instance;
    [Header("Action Panel References")] 
    [SerializeField] private GameObject _actionPanel;
    [SerializeField] private GameObject _useItemButton;
    [SerializeField] private GameObject _equipItemButton;
    [SerializeField] private GameObject _dropItemButton;
    [SerializeField] private GameObject _destroyItemButton;

    private ItemData _currItem;

    [SerializeField]
    private Transform _dropPoint;

    [SerializeField] private EquipmentLibrary _equipmentLibrary;
    
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
            _inventoryPanel.transform.gameObject.SetActive(!_inventoryPanel.transform.gameObject.activeSelf);
        }
    }
    
    public void AddItem(ItemData item)
    {
        _content.Add(item);
        RefreshContent();
    }

    public void CloseInventory()
    {
        _inventoryPanel.transform.gameObject.SetActive(false);
        CloseActoinPanel();
    }

    public void CloseActoinPanel()
    {
        _actionPanel.SetActive(false);
        _currItem = null;
    }

    public bool isFull()
    { 
        return _content.Count >= _inventoryPanel.GetInventorySize();
    }
    
    private void RefreshContent()
    {
        Slot[] allSlot = _inventoryPanel.GetInventorySlots();
        
        for (int i = 0; i < allSlot.Length; i++)
        {
            if(i < _content.Count)
                allSlot[i].setItem(_content[i]);
            else
                allSlot[i].setItem(null, _emptyVisual);
        }
    }

    public void OpenActionPanel(ItemData item, Vector2 position)
    {
        _currItem = item;
        if (item == null)
        {
            _actionPanel.SetActive(false);
            return;
        }

        switch (item.itemType)
        {
            case ItemType.Ressource:
                _useItemButton.SetActive(false);
                _equipItemButton.SetActive(false);
                break;
            case ItemType.Equipement:
                _useItemButton.SetActive(false);
                _equipItemButton.SetActive(true);
                break;
            case ItemType.Consumable:
                _useItemButton.SetActive(true);
                _equipItemButton.SetActive(false);
                break;
        }
        
        _actionPanel.SetActive(true);
        _actionPanel.transform.position = position;
    }
    
    public void UseActionButton()
    {
        RefreshContent();
        CloseActoinPanel();
    }
    public void EquipActionButton()
    {
        EquipmentLibraryItem equipmentLibraryItem = _equipmentLibrary.equipmentLibrary.Single(e => e.itemData == _currItem);

        if (equipmentLibraryItem != null)
        {
            foreach (var equipmentToDisable in equipmentLibraryItem.elementsToDisable)
            {
                equipmentToDisable.SetActive(false);
            }

            equipmentLibraryItem.itemPrefab.SetActive(true);
        }
        else
            Debug.LogError("Item not found in list!!");

        RefreshContent();
        CloseActoinPanel();
    }
    public void DropActionButton()
    {
        Instantiate(_currItem.prefab, _dropPoint.position, Quaternion.identity);
        DestroyActionButton();
    }
    public void DestroyActionButton()
    {
        _content.Remove(_currItem);
        RefreshContent();
        CloseActoinPanel();
    }
}
