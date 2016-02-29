using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wish : MonoBehaviour {

    public SpriteRenderer Tuete;

    public GameObject[] Kugeln;
    public Color[] KugelColors;

    private int anzahlKugeln;
    public List<GameObject> wishKugeln;

    private SpriteRenderer rend;

    private bool show;

	// Use this for initialization
	void Start () {
        wishKugeln = new List<GameObject>();
        rend = GetComponent<SpriteRenderer>();
        ResetWish();
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject kugel in wishKugeln)
        {
            ColorMoveTowards(kugel.GetComponent<SpriteRenderer>());
        }
        ColorMoveTowards(rend);
        ColorMoveTowards(Tuete);
    }

    public void InitWish(int anzahlKugeln)
    {
        wishKugeln.Clear();
        if (anzahlKugeln > Kugeln.Length) anzahlKugeln = Kugeln.Length;
        if (anzahlKugeln < 1) anzahlKugeln = 1;

        for (int i = 0; i < anzahlKugeln; i++)
        {
            int randomColorIndex = (int)Random.Range(0, KugelColors.Length);

            Color kugelColorTransparent = new Color(
                KugelColors[randomColorIndex].r,
                KugelColors[randomColorIndex].g,
                KugelColors[randomColorIndex].b,
                0
                );

            Kugeln[i].GetComponent<SpriteRenderer>().color = kugelColorTransparent;

            wishKugeln.Add(Kugeln[i]);
        }
    }

    public void ShowWish()
    {
        rend.enabled = true;
        Tuete.enabled = true;
        foreach (GameObject kugel in wishKugeln)
        {
            kugel.GetComponent<SpriteRenderer>().enabled = true;
        }
        show = true;
    }

    public void HideWish()
    {
        show = false;
    }

    public void HideWishInstant()
    {
        HideWish();
        rend.enabled = false;
        Tuete.enabled = false;
        foreach (GameObject kugel in wishKugeln)
        {
            kugel.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void ResetWish()
    {
        wishKugeln.Clear();
        foreach (GameObject kugel  in Kugeln)
        {
            kugel.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0);
        Tuete.color = new Color(Tuete.color.r, Tuete.color.g, Tuete.color.b, 0);
    }

    private void ColorMoveTowards(SpriteRenderer _rend)
    {
        if (show && _rend.color.a < 1)
        {
            float a = _rend.color.a;
            a += Time.deltaTime;

            Color temp = _rend.color;

            _rend.color = a <= 1 ? new Color(temp.r, temp.g, temp.b, a) : new Color(temp.r, temp.g, temp.b, 1);
        }
        else if (!show && _rend.color.a > 0)
        {
            float a = _rend.color.a;
            a -= Time.deltaTime;

            Color temp = _rend.color;

            _rend.color = a >= 0 ? new Color(temp.r, temp.g, temp.b, a) : new Color(temp.r, temp.g, temp.b, 0);
        }
    }
}
