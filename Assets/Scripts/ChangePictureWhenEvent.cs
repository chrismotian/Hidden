using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePictureWhenEvent : MonoBehaviour
{
    int item = 1;
    [SerializeField] int gamearrayindex = 0;
    GameManager instance = null;
    private void Start()
    {
        instance = GameObject.FindObjectOfType<GameManager>();
        instance.OnReveal += GameManager_OnReveal;
        instance.OnJackpot += GameManager_OnJackpot;
    }

    private void GameManager_OnReveal(object sender, GameManager.OnRevealEventArgs e)
    {
        Debug.Log("Event at the index " + e.gamearrayindex);
        if (e.gamearrayindex == gamearrayindex)
        {
            item = e.item;
            this.gameObject.GetComponent<Image>().sprite = instance.spriteArray[item];
            if (item == 0) StartCoroutine("Rotate");
        }
    }

    IEnumerator Rotate()
    {
        for (int i = 0; i < 360; i=i+2)
        {
            transform.RotateAround(this.transform.position,Vector3.forward,2);
            yield return new WaitForSeconds(0.0001f);
        }
    }

    private void GameManager_OnJackpot(object sender, System.EventArgs e)
    {
        StartCoroutine("Split");
    }

    IEnumerator Split()
    {
        yield return new WaitForSeconds(2);
        if (item != 0) this.gameObject.GetComponent<Image>().sprite = instance.splitspriteArray[item];
    }
}