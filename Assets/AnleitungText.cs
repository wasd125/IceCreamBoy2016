using UnityEngine;
using System.Collections;

public class AnleitungText : MonoBehaviour {

    private SpriteRenderer rend;

    public Sprite pickCone;
    public Sprite pickScoop;

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
        HideText();
	}

    public void ShowPickScoopText()
    {
        rend.enabled = true;
        rend.sprite = pickScoop;
    }

    public void ShowPickConeText()
    {
        rend.enabled = true;
        rend.sprite = pickCone;
    }

    public void HideText()
    {
        rend.enabled = false;
    }

}
