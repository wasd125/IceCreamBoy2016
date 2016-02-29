using UnityEngine;
using System.Collections;

public class Day {

    public int DayNumber { get; set; }

    public Day(int number)
    {
        this.DayNumber = number;
    }

    public PressBar.EnumBarDifficulty GetRandomDifficulty()
    {
        PressBar.EnumBarDifficulty result =  PressBar.EnumBarDifficulty.VeryEasy;

        float dayFloatMin = (float)DayNumber / 2f;

        int randomNumer = (int)Random.Range(0 + dayFloatMin, DayNumber);

        result += randomNumer;

        return result;
    }

    public int GetAnzahlBoys()
    {
        //return DayNumber * 5;
        return 1;
    }

    public int GetAnzahlKugeln()
    {
        float dayFloatMin = (float)DayNumber / 2f;

        return (int)Random.Range(0 + DayNumber + (DayNumber/2), DayNumber + DayNumber + 1);
    }
}
