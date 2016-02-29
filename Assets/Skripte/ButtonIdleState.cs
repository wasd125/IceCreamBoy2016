using UnityEngine;
using System.Collections;
using System;

public class ButtonIdleState : IButtonState
{
    private Button button;

    public ButtonIdleState(Button _button)
    {
        this.button = _button;
    }

    public void DoUpdate()
    {
        if (Input.GetKeyDown(button.keyCodes[(int)button.enumButtonLetter]))
        {
            ToPressedState();
        }
    }

    public void ToIdleState()
    {
        Debug.Log("Can not go to own state");
    }

    public void ToPressedState()
    {
        button.currentButtonState = button.buttonPressedState;
    }

    public void ToReleasedState()
    {
        button.currentButtonState = button.buttonReleasedState;
    }
}
