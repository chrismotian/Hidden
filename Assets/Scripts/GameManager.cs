using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event EventHandler<OnScoreUpEventArgs> OnScoreUp;
    public class OnScoreUpEventArgs: EventArgs
    {
        public int gamearrayindex;
    }
    //The number of buttons/picks on the screen
    int gamearraylenght = 9;
    //This array is filled with awards while numbers are coding the awards ( 0 is the jackpot and the normal awards are a number higher than 0)
    int[] gamearray = null;
    //Every pick is raising the score
    int score = 0;
    //Just an amount of picks are possible and this number is defining this limit
    int pickamountlimit = 3;
    //Temporary varaible of the pickamountlimit to store how many picks are done
    int pickamount = 0;
    //The position of the jackpot in the gamearray while this can be a number from 0 to gamearray.Length
    int jackpotindex = 0;
    //The done picks are stored here because we are not allowed to pick the same gamearrayindex twice, but there is an exception: After a jackpot is picked we are resetting this because we are allowed to pick all indices again
    List<int> pickedgamearray = null;

    void Start()
    {
        gamearray = new int[gamearraylenght];
        pickamount = 0;
        InitilisePicks();
        Debug.Log("No pick yet while the jackpot is at " + jackpotindex);
    }

    //The UI buttons accessing this and Jackpot() 
    public void Pick(int gamearrayindex)
    {
        if (!pickedgamearray.Contains(gamearrayindex) && pickamount < pickamountlimit)
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
            }
            OnScoreUp?.Invoke(this, new OnScoreUpEventArgs { gamearrayindex = gamearrayindex });
        }
        Debug.Log("Picked now for the "+ pickamount + " time while the jackpot is at " + jackpotindex + " and the score is " + score);
    }

    void Jackpot()
    {
        for (int i = 0; i < gamearray.Length; i++)
        {
            if (i != jackpotindex && !pickedgamearray.Contains(i))
            {
                //Since Pick(i) is incrementing pickamount we have to decrement it here
                pickamount--;
                Pick(i);
            }
        }
        InitilisePicks();
    }

    //Randomly placing new awards for all picks while placing the one jackpot to a random index too
    void InitilisePicks()
    {
        pickedgamearray = new List<int>();
        jackpotindex = UnityEngine.Random.Range(0, gamearray.Length);
        gamearray[jackpotindex] = 0;
        for (int i = 0; i < gamearray.Length; i++)
        {
            if (i != jackpotindex) gamearray[i] = UnityEngine.Random.Range(1, 3);
        }
    }
}