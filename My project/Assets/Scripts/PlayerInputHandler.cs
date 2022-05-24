using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputHandler : MonoBehaviour
{

    public Vector2 move;
    public bool cast;
    public static event Action casting;

    public bool menu;
    public static event Action openingMenu;

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
    public void OnCast(InputValue value)
    {
        cast = value.isPressed;
        casting?.Invoke(); //Ativa o evento quando apertar o botão de cast (Mouse 1)
    }

    public void OnMenu(InputValue value)
    {
        menu = !menu;
        openingMenu?.Invoke(); //Ativa o evento de abrir o menu apertando ESC
    }
}
