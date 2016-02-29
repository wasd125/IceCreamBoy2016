using UnityEngine;
using System.Collections;

public class Eiskugel : MonoBehaviour {

    public Color colorYellow;
    public Color colorBrown;
    public Color colorGreen;
    public Color colorRed;

    public enum EnumIceSort { Yellow, Brown, Green, Red}

    private SpriteRenderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitIceKugel(EnumIceSort iceSort)
    {
        switch (iceSort)
        {
            case EnumIceSort.Yellow:
                rend.color = colorYellow;
                break;
            case EnumIceSort.Brown:
                rend.color = colorBrown;
                break;
            case EnumIceSort.Green:
                rend.color = colorGreen;
                break;
            case EnumIceSort.Red:
                rend.color = colorRed;
                break;
        }
    }
}
