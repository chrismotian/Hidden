using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScoreWhenEvent : MonoBehaviour
{
    [SerializeField] bool highscore = false;
    private void Start()
    {
        GameManager revealEvent = GameObject.FindObjectOfType<GameManager>();
        revealEvent.OnReveal += GameManager_OnReveal;
    }

    private void GameManager_OnReveal(object sender, GameManager.OnRevealEventArgs e)
    {
        if((highscore && e.gamearrayindex == -1) || (!highscore && e.gamearrayindex >= 0)) {
            Debug.Log("Event at the index " + e.gamearrayindex + " and the score is " + e.score);
            ChangeScore(e.score);
        }
    }
    void ChangeScore(int score)
    {
        if (score < 10)
        {
            this.GetComponent<Text>().text = "000" + score;
        }
        else if (score < 100)
        {
            this.GetComponent<Text>().text = "00" + score;
        }
        else if (score < 1000)
        {
            this.GetComponent<Text>().text = "0" + score;
        }
        else
        {
            this.GetComponent<Text>().text = "" + score;
        }
    }
    public void ScoreQuery() {
        Highscore instance = GameObject.FindObjectOfType<Highscore>();
        int score = instance.score;
        ChangeScore(score);
        if(score > Highscore.LoadFile())
        {
            SoundManager.PlaySound("win");
            Highscore.SaveFile(score);
        }
        else
        {
            SoundManager.PlaySound("lose");
        }
    }
    public void ScoreHighQuery()
    {
        ChangeScore(Highscore.LoadFile());
    }
    public void DestroyHighscore()
    {
        Highscore instance = GameObject.FindObjectOfType<Highscore>();
        Destroy(instance.gameObject);
    }
}