using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAwayWhenEvent : MonoBehaviour
{
    [SerializeField] int gamearrayindex = 0;
    private void Start()
    {
        GameManager revealEvent = GameObject.FindObjectOfType<GameManager>();
        revealEvent.OnReveal += GameManager_OnReveal;
    }

    private void GameManager_OnReveal(object sender, GameManager.OnRevealEventArgs e)
    {
        Debug.Log("Event at the index " + e.gamearrayindex);
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
