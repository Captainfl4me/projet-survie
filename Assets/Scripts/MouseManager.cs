using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public List<MenuMouseHandler> menuWhereMouseNeeded;
    [SerializeField] private ThirdPersonOrbitCamBasic orbitCamScript;
    [SerializeField] private AimBehaviourBasic aimBehaviourBasic;
    private bool _mouseNeeded = false;

    private void Update()
    {
        _mouseNeeded = false;
        foreach (MenuMouseHandler menu in menuWhereMouseNeeded)
        {
            if (menu.isMouseNeeded())
                _mouseNeeded = true;
        }

        if (_mouseNeeded)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        orbitCamScript.canOrbit = !_mouseNeeded;
        aimBehaviourBasic.canAim = !_mouseNeeded;
    }
}
