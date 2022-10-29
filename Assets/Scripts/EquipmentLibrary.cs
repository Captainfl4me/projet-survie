using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentLibrary : MonoBehaviour
{
    public List<EquipmentLibraryItem> equipmentLibrary = new List<EquipmentLibraryItem>();
}

[System.Serializable]
public class EquipmentLibraryItem
{
    public ItemData itemData;
    public GameObject itemPrefab;
    public GameObject[] elementsToDisable;
}
