using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{

    public static AudioClip playerWalk;
    public static AudioClip playerJump;
    public static AudioClip gameOver;
    public static AudioClip playerWin;
    public static AudioClip enemyDie;

    public static bool muteSfx;

    static AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {
        muteSfx = false;
        playerWalk = Resources.Load<AudioClip>("Walking");
        playerJump = Resources.Load<AudioClip>("Jumping");
        gameOver = Resources.Load<AudioClip>("GameOver");

        audioSrc = GetComponent<AudioSource>();

    }

    public static void PlaySound(string clip)
    {
       
        switch (clip)
        {
            case ("Walking"):
                if (!audioSrc.isPlaying)
                    audioSrc.PlayOneShot(playerWalk);
                break;
            case ("Jumping"):
                audioSrc.PlayOneShot(playerJump);
                break;
            case ("gameOver"):
                audioSrc.PlayOneShot(gameOver);
                break;
        }
    }

}