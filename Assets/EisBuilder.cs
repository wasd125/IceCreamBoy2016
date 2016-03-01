using UnityEngine;
using System.Collections;

public class EisBuilder : MonoBehaviour {

    public GameObject[] Kugeln;
    public GameObject Tuete;

    public GameObject Staender;

	// Use this for initialization
	void Start () {
        Hide();
	}

    public void Hide()
    {
        Tuete.transform.position = transform.position;
        Tuete.SetActive(false);
        foreach (GameObject kugel in Kugeln)
        {
            kugel.SetActive(false);
        }
    }

    public void PlaceKugel(int position, Color color)
    {
        Kugeln[position].SetActive(true);
        Kugeln[position].GetComponent<SpriteRenderer>().color = color;
    }

    public void PlaceTuete()
    {
        Tuete.SetActive(true);
    }

    public void InDenStaender()
    {
        Tuete.transform.position = Staender.transform.position;
    }
}
