using UnityEngine;
using System.Collections;

public class DayFader : MonoBehaviour {

    public SpriteRenderer dayZahl;
    public SpriteRenderer dayText;

    public Sprite[] zahlen;

    public ScoreController scoreController;

    void Start()
    {
        scoreController.HideScore();
    }

    public void Show(int day)
    {
        dayZahl.sprite = zahlen[day];

        GetComponent<SpriteRenderer>().enabled = true;
        dayZahl.enabled = true;
        dayText.enabled = true;
    }

    public void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        dayZahl.enabled = false;
        dayText.enabled = false;

    }

    public void ShowScore(float score)
    {
        GetComponent<SpriteRenderer>().enabled = true;
        scoreController.ShowScore(score);
    }

}
