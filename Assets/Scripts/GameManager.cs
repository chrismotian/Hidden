using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public class GameManager : MonoBehaviour
{

    //The number of buttons on the screen
    int gamearraylenght = 9;
    //This array is filled with items while numbers are coding the items (0 is the jackpot and other items are a number higher than 0)
    int[] gamearray = null;
    //Every reveal is raising the score
    int score = 0;
    //Just an certain amount of picks is possible and this number defines this limit
    int pick = 3;
    //Saves how many choices are revealed while this can be a number from 0 to pick
    int revealed = 0;
    //The position of the jackpot in the gamearray while this can be a number from 0 to gamearray.Length
    int jackpotindex = 0;
    //The revealed gamearray indicies are stored here because we are not allowed to reveal the same gamearrayindex twice, but there is an exception: After a jackpot is revealed there is a reset of this because the whole gamearray is initialized again
    List<int> revealedindicies = null;
    //UI control is done with this event
    public event EventHandler<OnRevealEventArgs> OnReveal;
    public class OnRevealEventArgs : EventArgs
    {
        public int gamearrayindex;
    }

    void Start()
    {
        gamearray = new int[gamearraylenght];
        revealed = 0;
        InitilizeGamearray();
        Debug.Log("Nothing revealed yet while the jackpot is at " + jackpotindex);
    }

    //The UI buttons accessing this and the function Jackpot
    public void TryReveal(int gamearrayindex)
    {
        if (!revealedindicies.Contains(gamearrayindex) && revealed < pick)
        {
            switch (gamearray[gamearrayindex])
            {
                case 0:
                    Jackpot();
                    revealed++;
                    break;
                case 1:
                    score = score + 20;
                    revealedindicies.Add(gamearrayindex);
                    revealed++;
                    break;
                case 2:
                    score = score + 10;
                    revealedindicies.Add(gamearrayindex);
                    revealed++;
                    break;
            }
            OnReveal?.Invoke(this, new OnRevealEventArgs { gamearrayindex = gamearrayindex });
        }else if(revealed == pick){
            Assert.IsTrue(score <= (gamearray.Length - 1) * 3 * 20);
        }
        Debug.Log("Revealed now the "+ revealed + " time while the jackpot is at " + jackpotindex + " and the score is " + score);
    }

    void Jackpot()
    {
        for (int i = 0; i < gamearray.Length; i++)
        {
            if (i != jackpotindex && !revealedindicies.Contains(i))
            {
                //Since Pick(i) is incrementing pickamount we have to decrement it here
                revealed--;
                TryReveal(i);
            }
        }
        InitilizeGamearray();
    }

    //Randomly placing new awards for all picks while placing the one jackpot to a random index too
    void InitilizeGamearray()
    {
        revealedindicies = new List<int>();
        jackpotindex = UnityEngine.Random.Range(0, gamearray.Length);
        gamearray[jackpotindex] = 0;
        for (int i = 0; i < gamearray.Length; i++)
        {
            if (i != jackpotindex) gamearray[i] = UnityEngine.Random.Range(1, 3);
        }
    }
}