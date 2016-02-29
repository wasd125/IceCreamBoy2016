using UnityEngine;
using System.Collections;

public class Boy : MonoBehaviour {

    public Color[] colors;

    public Color[] skinColors;

    public SpriteRenderer bodyRend;
    public SpriteRenderer headRend;

    private Boy nextBoy;

    private Vector3 moveTowards;

    public void Update()
    {
        if (transform.position != moveTowards)
            transform.position = Vector3.MoveTowards(transform.position, moveTowards, 0.02f);
    }

    public void Start()
    {
        moveTowards = transform.position;

        int random = Random.Range(0, colors.Length);
        int randomSkin = Random.Range(0, skinColors.Length);

        bodyRend.color = colors[random];
        headRend.color = skinColors[randomSkin];
    }

    public Color GetColor()
    {
        return bodyRend.color;
    }

    public Color GetSkinColor()
    {
        return headRend.color;
    }

    public void MoveTowards(Vector3 position)
    {
        moveTowards = position;
    }
}
