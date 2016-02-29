using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

    public Sprite[] ziffernSprites;

    public SpriteRenderer Tausender;
    public SpriteRenderer Hunderter;
    public SpriteRenderer Zehner;
    public SpriteRenderer Einer;

    public void ShowScore(float score)
    {
        Tausender.enabled = true;
        Zehner.enabled = true;
        Hunderter.enabled = true;
        Einer.enabled = true;

        if (score > 9999) score = 9999;
        if (score < 0) score = 0;

        float[] digits = new float[] { 0, 0, 0, 0 };
        float temp = 0f;


        if (score > 1000f)
        {
            temp = score / 1000f;

            digits[0] = (int)temp;
            score -= (digits[0] * 1000f);
        }
        if (score > 100f)
        {
            temp = score / 100f;

            digits[1] = (int)temp;
            score -= (digits[1] * 100f);
        }
        if (score > 10f)
        {
            temp = score / 10f;

            if (temp < 0) temp = 0;
            if (temp > 10) temp = 0;

            digits[2] = (int)temp;
            score -= (digits[2] * 10f);
        }
        if((int) score < 0)
        {
            score = 0;
        }
        if ((int)score > 10)
        {
            score = 0;
        }
        digits[3] = (int)score;

        Tausender.sprite = ziffernSprites[(int)digits[0]];
        Hunderter.sprite = ziffernSprites[(int)digits[1]];
        Zehner.sprite = ziffernSprites[(int)digits[2]];
        Einer.sprite = ziffernSprites[(int)digits[3]];
    }

    public void HideScore()
    {
        Tausender.enabled = false;
        Zehner.enabled = false;
        Hunderter.enabled = false;
        Einer.enabled = false;
    }
}
