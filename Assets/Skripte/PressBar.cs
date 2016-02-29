using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PressBar : MonoBehaviour {

    public delegate void RetunPressbarResult(GameController.EnumPressBarResult result,KeyCode code);
    private RetunPressbarResult ReturnMyResult;

    public enum EnumBarDifficulty {VeryEasy,Easy,Normal,Hard,VeryHard }

    Vector3 goodOriginalScale;
    Vector3 perfectOriginalScale;

    public GameObject goodMarker;
    public GameObject perfectMarker;
    public GameObject currentPositionMarker;

    public GameObject ResultIndicator;
    public Sprite[] ResultIndicatorSprites;

    private int moveSpeed;

    private List<KeyCode> myKeysToWatch;

    public EnumBarDifficulty enumBarDifficulty = EnumBarDifficulty.VeryEasy;

    private Vector3 maxXPosRight;
    private Vector3 maxXPosLeft;

    private SpriteRenderer rend;

    bool movingRight = true;

    public void InitPressBar( RetunPressbarResult callback,KeyCode[] keysToWatch, EnumBarDifficulty difficulty = EnumBarDifficulty.VeryEasy)
    {
        this.enumBarDifficulty = difficulty;
        this.ReturnMyResult = callback;
        this.myKeysToWatch = new List<KeyCode>(keysToWatch);
        SetZoneSizeForDifficulty();
        Show();
    }

    public void Show()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        goodMarker.GetComponent<SpriteRenderer>().enabled = true;
        perfectMarker.GetComponent<SpriteRenderer>().enabled = true;
        currentPositionMarker.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        goodMarker.GetComponent<SpriteRenderer>().enabled = false;
        perfectMarker.GetComponent<SpriteRenderer>().enabled = false;
        currentPositionMarker.GetComponent<SpriteRenderer>().enabled = false;

    }

    // Use this for initialization
    void Start () {
        rend = GetComponent<SpriteRenderer>();
        myKeysToWatch = new List<KeyCode>();
        // Set Current Marker Pos to the Middle
        currentPositionMarker.transform.position = transform.position;

        goodOriginalScale = goodMarker.transform.localScale;
        perfectOriginalScale = perfectMarker.transform.localScale;

        SetZoneSizeForDifficulty();



        maxXPosLeft = new Vector3
            (
                transform.position.x - (rend.sprite.bounds.size.x / 2) -0.5f ,
                transform.position.y,
                transform.position.z
            );
        maxXPosRight = new Vector3
            (
                transform.position.x + (rend.sprite.bounds.size.x / 2) +0.5f,
                transform.position.y,
                transform.position.z
            );

        Hide();
    }
	
	// Update is called once per frame
	void Update () {
        if (myKeysToWatch.Count < 1) return;

        foreach (KeyCode  code  in myKeysToWatch)
        {
            if (Input.GetKeyDown(code))
            {
                if (currentPositionMarker.transform.position.x > perfectMarker.transform.position.x - (perfectMarker.GetComponent<SpriteRenderer>().bounds.size.x / 2)
                    &&
                  currentPositionMarker.transform.position.x < perfectMarker.transform.position.x + (perfectMarker.GetComponent<SpriteRenderer>().bounds.size.x / 2))
                {
                    ReturnMyResult(GameController.EnumPressBarResult.Perfect, code);
                    ShowResult(GameController.EnumPressBarResult.Perfect);           
                }
                else
                if (currentPositionMarker.transform.position.x > goodMarker.transform.position.x - (goodMarker.GetComponent<SpriteRenderer>().bounds.size.x / 2)
                    &&
                  currentPositionMarker.transform.position.x < goodMarker.transform.position.x + (goodMarker.GetComponent<SpriteRenderer>().bounds.size.x / 2))
                {
                    ReturnMyResult(GameController.EnumPressBarResult.Good, code);
                    ShowResult(GameController.EnumPressBarResult.Good);
                }
                else
                {
                    ReturnMyResult(GameController.EnumPressBarResult.Bad, code);
                    ShowResult(GameController.EnumPressBarResult.Bad);
                }
                myKeysToWatch.Clear();
                return;
            }
        }

        MoveMarker();
	}

    private void ShowResult(GameController.EnumPressBarResult result)
    {
        GameObject temp = Instantiate(ResultIndicator) as GameObject;
        switch (result)
        {
            case GameController.EnumPressBarResult.Bad:
                temp.GetComponent<IndicatorHolder>().childRender.sprite = ResultIndicatorSprites[0];
                break;
            case GameController.EnumPressBarResult.Good:
                temp.GetComponent<IndicatorHolder>().childRender.sprite = ResultIndicatorSprites[1];
                break;
            case GameController.EnumPressBarResult.Perfect:
                temp.GetComponent<IndicatorHolder>().childRender.sprite = ResultIndicatorSprites[2];
                break;
        }
        temp.transform.position = currentPositionMarker.transform.position;
        temp.GetComponent<IndicatorHolder>().childAnim.SetTrigger("ShowIndicator");
    }

    private void MoveMarker()
    {
        if (movingRight)
        {
            if (currentPositionMarker.transform.position.x < maxXPosRight.x)
            {
                currentPositionMarker.transform.position = Vector3.MoveTowards(currentPositionMarker.transform.position, maxXPosRight, moveSpeed * Time.deltaTime);
            }
            else
            {
                movingRight = false;
            }
        }
        else
        {
            if (currentPositionMarker.transform.position.x > maxXPosLeft.x)
            {
                currentPositionMarker.transform.position = Vector3.MoveTowards(currentPositionMarker.transform.position, maxXPosLeft, moveSpeed * Time.deltaTime);
            }
            else
            {
                movingRight = true;
            }
        }
    }

    private void SetZoneSizeForDifficulty()
    {
        perfectMarker.transform.localScale = perfectOriginalScale;
        goodMarker.transform.localScale = goodOriginalScale;
        switch (enumBarDifficulty)
        {
            case EnumBarDifficulty.VeryEasy:
                perfectMarker.transform.localScale = new Vector3( perfectMarker.transform.localScale.x * 1.5f, perfectMarker.transform.localScale.y, perfectMarker.transform.localScale.z);
                goodMarker.transform.localScale = new Vector3(perfectMarker.transform.localScale.x * 2f, perfectMarker.transform.localScale.y, perfectMarker.transform.localScale.z);
                moveSpeed = 3;
                break;
            case EnumBarDifficulty.Easy:
                perfectMarker.transform.localScale = new Vector3(perfectMarker.transform.localScale.x * 1.25f, perfectMarker.transform.localScale.y, perfectMarker.transform.localScale.z);
                goodMarker.transform.localScale = new Vector3(perfectMarker.transform.localScale.x * 1.75f, perfectMarker.transform.localScale.y, perfectMarker.transform.localScale.z);
                moveSpeed = 4;
                break;
            case EnumBarDifficulty.Normal:
                perfectMarker.transform.localScale = new Vector3(perfectMarker.transform.localScale.x * 1f, perfectMarker.transform.localScale.y, perfectMarker.transform.localScale.z);
                goodMarker.transform.localScale = new Vector3(perfectMarker.transform.localScale.x * 1.5f, perfectMarker.transform.localScale.y, perfectMarker.transform.localScale.z);
                moveSpeed = 5;
                break;
            case EnumBarDifficulty.Hard:
                perfectMarker.transform.localScale = new Vector3(perfectMarker.transform.localScale.x * 1f, perfectMarker.transform.localScale.y, perfectMarker.transform.localScale.z);
                goodMarker.transform.localScale = new Vector3(perfectMarker.transform.localScale.x * 1.25f, perfectMarker.transform.localScale.y, perfectMarker.transform.localScale.z);
                moveSpeed = 7;
                break;
            case EnumBarDifficulty.VeryHard:
                perfectMarker.transform.localScale = new Vector3(perfectMarker.transform.localScale.x * 1f, perfectMarker.transform.localScale.y, perfectMarker.transform.localScale.z);
                goodMarker.transform.localScale = new Vector3(perfectMarker.transform.localScale.x * 1f, perfectMarker.transform.localScale.y, perfectMarker.transform.localScale.z);
                moveSpeed = 10;
                break;
        }
        float minRandPosX = transform.position.x - (rend.sprite.bounds.size.x / 2) + goodMarker.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float maxRandPosX = transform.position.x + (rend.sprite.bounds.size.x / 2) - goodMarker.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        float goodMarkerPosX = Random.Range(minRandPosX, maxRandPosX);

        goodMarker.transform.position = new Vector3
            (
                goodMarkerPosX,
                 transform.position.y,
                transform.position.z
            );

        perfectMarker.transform.position = goodMarker.transform.position;
    }
}
