using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour {

    public SpriteRenderer letterRend;

    public enum EnumButtonLetter {A,S,D,F,Space };

    public IButtonState currentButtonState;

    public List<KeyCode> keyCodes = new List<KeyCode>();

    public ButtonIdleState buttonIdleState;
    public ButtonPressedState buttonPressedState;
    public ButtonReleasedState buttonReleasedState;

    public bool pressed;

    public EnumButtonLetter enumButtonLetter;

    public Sprite[] letterSprites;

	// Use this for initialization
	void Start () {
        InitLetter(enumButtonLetter);

        keyCodes.Add(KeyCode.A);
        keyCodes.Add(KeyCode.S);
        keyCodes.Add(KeyCode.D);
        keyCodes.Add(KeyCode.F);
        keyCodes.Add(KeyCode.Space);

        buttonIdleState = new ButtonIdleState(this);
        buttonPressedState = new ButtonPressedState(this);
        buttonReleasedState = new ButtonReleasedState(this);

        currentButtonState = buttonIdleState;
    }

    void Update(){
        currentButtonState.DoUpdate();
    }

    public void InitLetter(EnumButtonLetter _enumButtonLetter)
    {
        enumButtonLetter = _enumButtonLetter;
        if(letterSprites.Length > 0)
        letterRend.sprite = letterSprites[(int)enumButtonLetter];
    }

}


