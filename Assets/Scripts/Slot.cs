using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ItemData _item;
    [SerializeField]
    private Image _visualSprite;

    public void setItem(ItemData item)
    {
        _item = item;
        _visualSprite.sprite = item.visual;
    }

    public ItemData getItem()
    {
        return _item;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_item)
            TooltipSystem.instance.Show(_item.description, _item.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
    }
}
