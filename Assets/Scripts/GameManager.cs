using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public class GameManager : MonoBehaviour
{
    //Contains the sprites while the position [0] is for the jackpot and the lowest award is at [1] because from the index 1 the calculated award is: 2 to the power of index
    public Sprite[] spriteArray = null;
    public Sprite[] splitspriteArray = null;
    //Has to be the same as the number of buttons on the screen
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
    //UI control is done with this events
    public event EventHandler<OnRevealEventArgs> OnReveal;
    public class OnRevealEventArgs : EventArgs
    {
        public int gamearrayindex;
        public int score;
        public int item;
    }
    public event EventHandler OnJackpot;
    [SerializeField] GameObject Jackpotanimation = null;


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
            revealed++;
            int item = gamearray[gamearrayindex];
            if (item == 0)
            {
                SoundManager.PlaySound("jackpot");
                OnReveal?.Invoke(this, new OnRevealEventArgs { gamearrayindex = gamearrayindex, score = score, item = item });
                GameObject instance = (GameObject)Instantiate(Jackpotanimation, Vector3.zero, Quaternion.identity);
                instance.transform.parent = this.transform;
                Jackpot();
                score = score * 2;
                OnReveal?.Invoke(this, new OnRevealEventArgs { gamearrayindex = gamearrayindex, score = score, item = item });
            }
            else
            {
                SoundManager.PlaySound("reveal");
                revealedindicies.Add(gamearrayindex);
                int award = (int)Mathf.Pow(2, item);
                score = score + award;
                OnReveal?.Invoke(this, new OnRevealEventArgs { gamearrayindex = gamearrayindex, score = score, item = item });
            }
            if (revealed >= 3)
            {
                SceneLoader.LoadScene(3,4);
            }
        }
        else if(revealed == pick){
            int maxScore = 0;
            for(int i = 0; i< pick; i++)
            {
                maxScore = maxScore + ((gamearray.Length - 1) * (int)Mathf.Pow(2, spriteArray.Length));
                maxScore = maxScore * 2;
            }
            Assert.IsTrue(score <= maxScore);
        }
        Debug.Log("Revealed now the "+ revealed + " time while the jackpot is at " + jackpotindex + " and the score is " + score);
    }

    void Jackpot()
    {
        for (int i = 0; i < gamearray.Length; i++)
        {
            if (i != jackpotindex && !revealedindicies.Contains(i))
            {
                //Since TryReveal(i) is incrementing pickamount we have to decrement it here
                revealed--;
                TryReveal(i);
            }
        }
        OnJackpot?.Invoke(this, System.EventArgs.Empty);
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
            if (i != jackpotindex) gamearray[i] = UnityEngine.Random.Range(1, 6);
        }
    }
    public void LoadScene(int scenenumber)
    {
        SceneLoader.LoadScene(scenenumber, 1);
    }
}