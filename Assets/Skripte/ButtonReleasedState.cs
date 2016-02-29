using UnityEngine;
using System.Collections;
using System;

public class ButtonReleasedState : IButtonState
{
    private Button button;

    private Vector3 goToScale;

    public ButtonReleasedState(Button _button)
    {
        this.button = _button;
        goToScale = button.transform.localScale;
    }

    public void DoUpdate()
    {
        button.transform.localScale = Vector3.MoveTowards(button.transform.localScale, goToScale, 5 * Time.deltaTime);
        if (button.transform.localScale == goToScale)
            ToIdleState();
    }

    public void ToIdleState()
    {
        button.pressed = false;
        button.currentButtonState = button.buttonIdleState;
    }

    public void ToPressedState()
    {
        button.currentButtonState = button.buttonPressedState;
    }

    public void ToReleasedState()
    {
        Debug.Log("Can not go to own state");
    }
}
