using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextWhenEvent : MonoBehaviour
{
    private void Start()
    {
        GameManager revealEvent = GameObject.FindObjectOfType<GameManager>();
        revealEvent.OnReveal += GameManager_OnReveal;
    }

    private void GameManager_OnReveal(object sender, GameManager.OnRevealEventArgs e)
    {
        Debug.Log("Event at the index " + e.gamearrayindex);
        this.GetComponent<Text>().text = "Score: " + e.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
