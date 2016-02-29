using UnityEngine;
using System.Collections;
using System;

public class ButtonPressedState : IButtonState
{
    private Button button;

    private Vector3 goToScale;

    public ButtonPressedState(Button _button)
    {
        this.button = _button;
        this.goToScale = button.transform.localScale * 0.8f;
    }

    public void DoUpdate()
    {
        button.transform.localScale = Vector3.MoveTowards(button.transform.localScale, goToScale, 5 * Time.deltaTime);
        if (button.transform.localScale == goToScale)
            ToReleasedState();
    }

    public void ToIdleState()
    {
        button.currentButtonState = button.buttonIdleState;
    }

    public void ToPressedState()
    {
        Debug.Log("Can not go to own state");
    }

    public void ToReleasedState()
    {
        button.currentButtonState = button.buttonReleasedState;
    }
}
