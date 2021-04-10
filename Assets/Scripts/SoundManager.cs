using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip clickSound, revealSound, jackpotSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        clickSound = Resources.Load<AudioClip>("click");
        revealSound = Resources.Load<AudioClip>("reveal");
        jackpotSound = Resources.Load<AudioClip>("jackpot");

        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "click":
                audioSrc.PlayOneShot(clickSound);
                break;
            case "reveal":
                audioSrc.PlayOneShot(revealSound);
                break;
            case "jackpot":
                audioSrc.PlayOneShot(jackpotSound);
                break;
        }
    }
}
