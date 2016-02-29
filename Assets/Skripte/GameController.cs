using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private Vector3 OriginPos;

    public ScoreController scoreControl;

    public DayFader fader;

    private int currentDay = 0;

    public List<Day> days = new List<Day>();

    public GameObject BoyPrefap;
    public GameObject BigBoy;
    public SpriteRenderer BigBoyBody;
    public SpriteRenderer BigBoyHead;

    public EisBuilder eisBuilder;

    public GameObject a, s, d, f, space;

    public AnleitungText anleitungText;

    public PressBar Skillbar;

    public GameObject KaufPosition;
    public GameObject GehenPosition;

    public Wish wish;

    public enum EnumGameState { DayScreen,CustomerComming,ShowingWish,PreparePickTube, PickingTube,PreparePickIce, PickingIce,FeedBack,CustomerGoing,PrepareNextDay,ShowScore, Ende }
    public EnumGameState gameState = EnumGameState.DayScreen;

    public enum EnumPressBarResult {Waiting, Bad,Good,Perfect };
    public EnumPressBarResult PressbarResult { get; set; }

    private List<KeyCode> playersInputs;

    public static GameController instance;

    private List<GameObject> boys;

    public int anzahlBoys;

    float waitingTime = 0f;

    int overallPoints = 0;

	// Use this for initialization
	void Start () {

        if (instance == null && instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        fader.Show(currentDay);

        for (int i = 1; i < 7; i++)
        {
            days.Add(new Day(i));
        }

        OriginPos = BoyPrefap.transform.position;

        anzahlBoys = days[0].GetAnzahlBoys();

        playersInputs = new List<KeyCode>();
        boys = new List<GameObject>();


        InitBoys();
        HideIceButtons();
        HideSpaceButton();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (waitingTime > 0)
        {
            waitingTime -= Time.deltaTime;
            return;
        }

        switch (gameState)
        {
            case EnumGameState.DayScreen:
                DayScreen();
                break;
            case EnumGameState.CustomerComming:
                CustomerComing();
                break;
            case EnumGameState.ShowingWish:
                ShowingWish();
                break;
            case EnumGameState.PreparePickTube:
                PreparePickTube();
                break;
            case EnumGameState.PickingTube:
                PickingTube();
                break;
            case EnumGameState.PreparePickIce:
                PreparePickIce();
                break;
            case EnumGameState.PickingIce:
                PickingIce();
                break;
            case EnumGameState.FeedBack:
                Feedback();
                break;
            case EnumGameState.CustomerGoing:
                CustomerGoing();
                break;
            case EnumGameState.PrepareNextDay:
                PrepareNextDay();
                break;
            case EnumGameState.ShowScore:
                ShowScore();
                break;
            case EnumGameState.Ende:
                Ende();
                break;
        }
	}

    void InitBoys()
    {
        boys.Add(BoyPrefap);

        for (int i = 0; i < anzahlBoys-1; i++)
        {
            GameObject newBoy = Instantiate(BoyPrefap) as GameObject;
            Vector3 temp = new Vector3(0.25f, 0, 0);
            newBoy.transform.position = BoyPrefap.transform.position +(temp* (i +1)) ;

            boys.Add(newBoy);
        }
    }

    void TransitionToNextState(float waitTime)
    {
        gameState++;
        this.waitingTime = waitTime;
    }

    void DayScreen()
    {
        TransitionToNextState(2f);
    }

    void CustomerComing()
    {
        fader.Hide();
        boys[0].GetComponent<Boy>().MoveTowards(KaufPosition.transform.position);

        for (int i = 1; i < boys.Count; i++)
        {
            boys[i].GetComponent<Boy>().MoveTowards(boys[i - 1].transform.position);
        }
        BigBoyBody.color = boys[0].GetComponent<Boy>().GetColor();
        BigBoyHead.color = boys[0].GetComponent<Boy>().GetSkinColor();
        BigBoy.GetComponent<Animator>().SetTrigger("MoveIn");
        TransitionToNextState(1.5f);
    }

    void ShowingWish()
    {
        wish.InitWish(days[currentDay].GetAnzahlKugeln());
        wish.ShowWish();

        TransitionToNextState(2f);
    }

    void PreparePickTube()
    {
        wish.HideWish();     
        anleitungText.ShowPickConeText();
        TransitionToNextState(0.01f);
        Skillbar.InitPressBar(PressBarCallBack, new KeyCode[] { KeyCode.Space }, days[currentDay].GetRandomDifficulty());
        ShowSpaceButton();
    }

    void PickingTube()
    {

        switch (PressbarResult)
        {
            case EnumPressBarResult.Bad:
                overallPoints -= 10;
                PressbarResult = EnumPressBarResult.Waiting;
                gameState = EnumGameState.PreparePickTube;
                Skillbar.Hide();
                HideSpaceButton();
                scoreControl.ShowScore((float)overallPoints);
                break;
            case EnumPressBarResult.Good:
                overallPoints += 10;
                Skillbar.Hide();
                HideSpaceButton();
                scoreControl.ShowScore((float)overallPoints);
                break;
            case EnumPressBarResult.Perfect:
                overallPoints += 20;
                Skillbar.Hide();
                HideSpaceButton();
                scoreControl.ShowScore((float)overallPoints);
                break;
        }

        if (PressbarResult != EnumPressBarResult.Waiting)
        {
            TransitionToNextState(0.3f);
            PressbarResult = EnumPressBarResult.Waiting;
            eisBuilder.PlaceTuete();
        }

    }

    void PreparePickIce()
    {
        anleitungText.ShowPickScoopText();
        TransitionToNextState(0.01f);
        Skillbar.InitPressBar(PressBarCallBack, new KeyCode[] { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F }, days[currentDay].GetRandomDifficulty());
        ShowIceButtons();
    }

    void PickingIce()
    {
        switch (PressbarResult)
        {
            case EnumPressBarResult.Bad:
                overallPoints -= 10;
                PressbarResult = EnumPressBarResult.Waiting;
                gameState = EnumGameState.PreparePickIce;
                scoreControl.ShowScore((float)overallPoints);
                Skillbar.Hide();
                HideIceButtons();
                break;
            case EnumPressBarResult.Good:
                overallPoints += 10;
                scoreControl.ShowScore((float)overallPoints);
                Skillbar.Hide();
                HideIceButtons();
                break;
            case EnumPressBarResult.Perfect:
                overallPoints += 20;
                scoreControl.ShowScore((float)overallPoints);
                Skillbar.Hide();
                HideIceButtons();
                break;
        }

        if (playersInputs.Count == wish.wishKugeln.Count)
        {
            eisBuilder.InDenStaender();
            wish.ShowWish();
            TransitionToNextState(0.5f);
        }
        else
        if (PressbarResult !=  EnumPressBarResult.Waiting)
        {
            gameState = EnumGameState.PreparePickIce;
            PressbarResult = EnumPressBarResult.Waiting;
        }
        
    }

    void Feedback()
    {
        anleitungText.HideText();
        for (int i = 0; i < playersInputs.Count; i++)
        {
            int index = 0;
            switch (playersInputs[i])
            {
                case KeyCode.A:
                    index = 0;
                    break;
                case KeyCode.S:
                    index = 1;
                    break;
                case KeyCode.D:
                    index = 2;
                    break;
                case KeyCode.F:
                    index = 3;
                    break;
            }
            Color c = wish.wishKugeln[i].GetComponent<SpriteRenderer>().color;
            if (
                wish.KugelColors[index].r == c.r &&
                wish.KugelColors[index].g == c.g &&
                wish.KugelColors[index].b == c.b
                )
            {
                overallPoints += 5;
                scoreControl.ShowScore((float)overallPoints);
            }
            else
            {
                overallPoints -= 25;
                scoreControl.ShowScore((float)overallPoints);
            }
        }

        TransitionToNextState(1.5f);

    }

    void CustomerGoing()
    {
        eisBuilder.Hide();
        wish.HideWishInstant();

        boys[0].GetComponent<Boy>().MoveTowards(GehenPosition.transform.position);
        boys.RemoveAt(0);
        BigBoy.GetComponent<Animator>().SetTrigger("MoveOut");
        playersInputs.Clear();
        PressbarResult = EnumPressBarResult.Waiting;

        if (boys.Count > 0)
        {          
            gameState = EnumGameState.CustomerComming;
            waitingTime = 2f;
        }
        else
        {
            TransitionToNextState(3f);
        }

    }

    void PrepareNextDay()
    {
        if (currentDay < 5)
        {
            currentDay++;
            
            BoyPrefap.transform.position = OriginPos;
            BoyPrefap.GetComponent<Boy>().MoveTowards(OriginPos);

            anzahlBoys = days[currentDay].GetAnzahlBoys();

            playersInputs = new List<KeyCode>();
            boys = new List<GameObject>();

            InitBoys();
            HideIceButtons();
            HideSpaceButton();

            fader.Show(currentDay);
            waitingTime = 3f;
            gameState = EnumGameState.DayScreen;
        }
        else
        {
            TransitionToNextState(0.1f);
        }
    }

    void ShowScore()
    {
        fader.ShowScore((float)overallPoints);
        TransitionToNextState(3f);

        float highScore = PlayerPrefs.GetFloat("Score");
        if (overallPoints > (int)highScore) PlayerPrefs.SetFloat("Score", (float)overallPoints);
    }

    void Ende()
    {
        SceneManager.LoadScene(0);
    }

    void PressBarCallBack(EnumPressBarResult result, KeyCode code)
    {
        if (code != KeyCode.Space)
        {
            if (result != EnumPressBarResult.Bad)
            {
                playersInputs.Add(code);
                switch (code)
                {
                    case KeyCode.A:
                        eisBuilder.PlaceKugel(playersInputs.Count - 1, wish.KugelColors[0]);
                        break;
                    case KeyCode.S:
                        eisBuilder.PlaceKugel(playersInputs.Count - 1, wish.KugelColors[1]);
                        break;
                    case KeyCode.D:
                        eisBuilder.PlaceKugel(playersInputs.Count - 1, wish.KugelColors[2]);
                        break;
                    case KeyCode.F:
                        eisBuilder.PlaceKugel(playersInputs.Count - 1, wish.KugelColors[3]);
                        break;
                }
            }
            PressbarResult = result;
        }
        else
        {
            PressbarResult = result;
        }

    }

    void ShowIceButtons()
    {
        a.SetActive(true);
        s.SetActive(true);
        d.SetActive(true);
        f.SetActive(true);
    }

    void HideIceButtons()
    {
        a.SetActive(false);
        s.SetActive(false);
        d.SetActive(false);
        f.SetActive(false);
    }

    void ShowSpaceButton()
    {
        space.SetActive(true);
    }

    void HideSpaceButton()
    {
        space.SetActive(false);
    }
}
