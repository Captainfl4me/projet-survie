using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator _playerAnimator;
    
    [SerializeField]
    private Inventory _inventory;

    [SerializeField]
    private MoveBehaviour _playerMoveBehavior;

    private Item _currItem;
    public void DoPickUp(Item item)
    {
        if (_inventory.IsFull())
        {
            Debug.Log("Inventory full");
            return;
        }

        _currItem = item;
        
        _playerMoveBehavior.canMove = false;
        _playerAnimator.SetTrigger("Pickup");
    }

    public void AddItemToInventory()
    {
        _inventory.AddItem(_currItem.itemData);
        Destroy(_currItem.transform.gameObject);

        _currItem = null;
    }

    public void ReEnablePlayerMovement()
    {
        _playerMoveBehavior.canMove = true;
    }
}
