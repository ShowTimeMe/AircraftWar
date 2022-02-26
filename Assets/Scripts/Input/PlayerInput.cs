using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;



[CreateAssetMenu(menuName ="PlayerInput")]
public class PlayerInput : ScriptableObject,InputActions.IGamePlayerActions
{
    //移动事件
    public event UnityAction<Vector2> onMove=delegate { };
    public event UnityAction onStopMove = delegate { };


    //开火事件
    public event UnityAction onFire = delegate { };
    public event UnityAction onStopFire = delegate { };

    public event UnityAction onDodge = delegate { };


    InputActions inputActions;

    void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.GamePlayer.SetCallbacks(this);
    }

    void OnDisable()
    {
        DisableAllInput(); 
    }
    public void DisableAllInput()
    {
        inputActions.GamePlayer.Disable();
    }
    public void EnableGamePlayerInput()
    {
        inputActions.GamePlayer.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //移动
    public void OnMove(InputAction.CallbackContext context)
    {
        //按下按键
        if (context.performed)
        {
            onMove.Invoke(context.ReadValue<Vector2>());
        }
        //松开按键
        if (context.canceled)
        {
            onStopMove.Invoke();
        }
    }

    //开火
    public void OnFire(InputAction.CallbackContext context)
    {
        //按下按键
        if (context.performed)
        {
            onFire.Invoke();
        }
        //松开按键
        if (context.canceled)
        {
            onStopFire.Invoke();
        }
    }

    public void OnDodge1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onDodge.Invoke();
        }
    }
}

