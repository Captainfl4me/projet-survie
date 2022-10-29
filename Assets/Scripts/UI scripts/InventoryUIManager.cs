using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(MenuMouseHandler))]
public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]
    private Vector2Int _UISize = new Vector2Int(4, 6);
    [SerializeField]
    private Vector2 _slotSize = new Vector2(100, 100);
    [SerializeField] 
    private int _spacing = 6;
    [SerializeField]
    private Vector2 _uiPadding = new Vector2(4, 6);

    [SerializeField] 
    private GameObject _UIContent;

    private RectTransform _inventorySize;
    private GridLayoutGroup _inventoryGridLayoutGroup;

    public GameObject slotPrefab;

    private void Start()
    {
        UpdateUI();
    }
    
    private void Awake()
    {
        _inventorySize = transform.GetComponent<RectTransform>();
        _inventoryGridLayoutGroup = _UIContent.GetComponent<GridLayoutGroup>();
    }
    public void UpdateUI()
    {
        if (!_inventoryGridLayoutGroup || !_inventorySize)
            Awake();

        int index = 0;
        GameObject[] allChildren = new GameObject[_UIContent.transform.childCount];
        foreach (Transform child in _UIContent.transform)
        {
            allChildren[index++] = child.gameObject;
        }
        foreach (GameObject child in allChildren) {
            if(Application.isEditor)
                DestroyImmediate(child);
            else
                Destroy(child);
        }
        
        _inventoryGridLayoutGroup.cellSize = _slotSize;
        _inventoryGridLayoutGroup.spacing = new Vector2(_spacing, _spacing);

        float gridWidth = 2 * (_UISize.x - 1) * _spacing + _slotSize.x * _UISize.x + 2*_uiPadding.x;
        float gridHeight = 2 * (_UISize.y - 1) * _spacing + _slotSize.y * _UISize.y+ 2*_uiPadding.y;

        _inventorySize.sizeDelta = new Vector2(gridWidth, gridHeight);

        for (int i = 0; i < _UISize.x * _UISize.y; i++)
        {
            Instantiate(slotPrefab, _UIContent.transform);
        }
    }

    public Slot[] GetInventorySlots()
    {
        int index = 0;
        Slot[] allChildren = new Slot[_UIContent.transform.childCount];
        foreach (Transform child in _UIContent.transform)
        {
            allChildren[index++] = child.GetComponent<Slot>();
        }

        return allChildren;
    }

    public int GetInventorySize()
    {
        return _UISize.x * _UISize.y;
    }
}
