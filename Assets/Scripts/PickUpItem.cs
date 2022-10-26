using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField]
    private float _pickUpLength = 2.6f;
    
    public PickUpBehaviour playerPickUpBehaviour;
    [SerializeField]
    private GameObject _pickUpText;

    [SerializeField] 
    private LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, _pickUpLength, layerMask))
        {
            if (hit.transform.CompareTag("Item"))
            {
                _pickUpText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Item item = hit.transform.GetComponent<Item>();
                    playerPickUpBehaviour.DoPickUp(item);
                }
            }
        }
        else
        {
            _pickUpText.SetActive(false);
        }
    }
}
