using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<ItemData> _content = new List<ItemData>();
    
    [SerializeField] 
    private InventoryUIManager _inventoryPanel;

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
    }

    public bool isFull()
    { 
        return _content.Count >= _inventoryPanel.GetInventorySize();
    }
    
    private void RefreshContent()
    {
        Transform[] allSlot = _inventoryPanel.GetInventorySlotsVisual();
        for (int i = 0; i < _content.Count; i++)
        {
            allSlot[i].GetComponent<Image>().sprite = _content[i].visual;
        }
    }
}
