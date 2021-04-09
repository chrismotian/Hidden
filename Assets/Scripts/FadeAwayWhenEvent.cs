using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAwayWhenEvent : MonoBehaviour
{
    [SerializeField] int gamearrayindex = 0;
    Color color = Color.white;
    private void Start()
    {
        color = this.GetComponent<Image>().color;
        GameManager scoreEvent = GameObject.FindObjectOfType<GameManager>();
        scoreEvent.OnScoreUp += GameManager_OnScoreUp;
    }

    private void GameManager_OnScoreUp(object sender, GameManager.OnScoreUpEventArgs e)
    {
        Debug.Log("Event at the index " + e.gamearrayindex + gamearrayindex);
        if (e.gamearrayindex == gamearrayindex) StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        for (float ft = 1f; ft >= -1; ft -= 0.1f)
        {
            Color c = this.gameObject.GetComponent<Image>().color;
            c.a = ft;
            this.gameObject.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(.1f);
        }
    }
}
