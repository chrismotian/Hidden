using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScoreWhenEvent : MonoBehaviour
{
    private void Start()
    {
        GameManager revealEvent = GameObject.FindObjectOfType<GameManager>();
        revealEvent.OnReveal += GameManager_OnReveal;
    }

    private void GameManager_OnReveal(object sender, GameManager.OnRevealEventArgs e)
    {
        Debug.Log("Event at the index " + e.gamearrayindex);
        if(e.score < 10)
        {
            this.GetComponent<Text>().text = "000"+ e.score;
        }
        else if(e.score < 100)
        {
            this.GetComponent<Text>().text = "00" + e.score;
        }
        else if (e.score < 1000)
        {
            this.GetComponent<Text>().text = "0" + e.score;
        }
        else
        {
            this.GetComponent<Text>().text = "" + e.score;
        }
    }
}
