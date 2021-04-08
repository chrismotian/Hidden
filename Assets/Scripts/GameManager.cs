using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int[] gamearray;

    int[] probabilityPerFruit;

    int score;
    int highscore;
    int ninjaindex;

    List<int> pickedgamearray;

    int pickamount;

    void Start()
    {
        int pickamount = 0;
        pickedgamearray = new List<int>();
        gamearray = new int[9];
        ninjaindex = Random.Range(1,9);
        gamearray[ninjaindex] = 0;
        for (int i = 0; i < gamearray.Length; i++)
        {
            if (i != ninjaindex) gamearray[i] = Random.Range(1, 3);
        } 
    }

    public void Pick(int gamearrayindex)
    {
        if (!pickedgamearray.Contains(gamearrayindex) && pickamount < 3)
        {
            switch (gamearray[gamearrayindex])
            {
                case 0:
                    Jackpot();
                    pickedgamearray = new List<int>();
                    pickamount++;
                    break;
                case 1:
                    score = score + 20;
                    pickedgamearray.Add(gamearrayindex);
                    pickamount++;
                    break;
                case 2:
                    score = score + 10;
                    pickedgamearray.Add(gamearrayindex);
                    pickamount++;
                    break;
                default:
                    break;
            }
        }
        Debug.Log("Picked now for the "+ pickamount + " time while the ninja is at " + ninjaindex +" and the score is " + score);
    }

    void Jackpot()
    {
        for (int i = 0; i < gamearray.Length; i++)
        {
            if (i != ninjaindex) Pick(i);
        }
        ninjaindex = Random.Range(1, 9);
        gamearray[ninjaindex] = 0;
        for (int i = 0; i < gamearray.Length; i++)
        {
            if (i != ninjaindex) gamearray[i] = Random.Range(1, 2);
        }
    }
}