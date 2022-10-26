using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private Text _headerField;
    [SerializeField]
    private Text _contentField;

    [SerializeField] private LayoutElement _layoutElement;
    [SerializeField] private int _maxCharacter;

    [SerializeField] private RectTransform _rectTransform;

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;

        _rectTransform.pivot = new Vector2(pivotX, pivotY);
        
        transform.position = mousePosition;
    }

    public void SetText(string content, string header = "")
    {
        if (header == "")
        {
            _headerField.gameObject.SetActive(false);
        }
        else
        {
            _headerField.gameObject.SetActive(true);
            _headerField.text = header;
        }

        _contentField.text = content;
        
        
        int headerLength = _headerField.text.Length;
        int contentLength = _contentField.text.Length;

        _layoutElement.enabled = (headerLength > _maxCharacter || contentLength > _maxCharacter);
    }
}
