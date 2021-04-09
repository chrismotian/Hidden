using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePictureWhenEvent : MonoBehaviour
{
    [SerializeField] int gamearrayindex = 0;
    GameManager instance = null;
    private void Start()
    {
        instance = GameObject.FindObjectOfType<GameManager>();
        instance.OnReveal += GameManager_OnReveal;
    }

    private void GameManager_OnReveal(object sender, GameManager.OnRevealEventArgs e)
    {
        Debug.Log("Event at the index " + e.gamearrayindex);
        if (e.gamearrayindex == gamearrayindex) this.gameObject.GetComponent<Image>().sprite = instance.spriteArray[e.item];
    }
}