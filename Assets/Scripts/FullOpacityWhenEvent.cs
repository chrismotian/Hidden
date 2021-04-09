using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullOpacityWhenEvent : MonoBehaviour
{
    private void Start()
    {
        GameManager fullOpacityEvent = GameObject.FindObjectOfType<GameManager>();
        fullOpacityEvent.OnJackpot += GameManager_OnJackpot;
    }

    void GameManager_OnJackpot(object sender, System.EventArgs e)
    {
        StartCoroutine("FullOpacity");
    }

    IEnumerator FullOpacity()
    {
        yield return new WaitForSeconds(2);
        Color c = this.gameObject.GetComponent<Image>().color;
        c.a = 1;
        this.gameObject.GetComponent<Image>().color = c;
    }
}
