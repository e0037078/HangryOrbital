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
    public static AudioClip enemyDie2;
    public static AudioClip forwardSlash;
    public static AudioClip backwardSlash;
    public static AudioClip zap;
    public static AudioClip click;
    public static AudioClip fireball;
    public static AudioClip fireballhit;
    public static AudioClip line;
    public static bool muteSfx;

    static AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {
        muteSfx = false;
        playerWalk = Resources.Load<AudioClip>("Walking");
        playerJump = Resources.Load<AudioClip>("Jumping");
        gameOver = Resources.Load<AudioClip>("GameOver");
        enemyDie = Resources.Load<AudioClip>("EnemyDie");
        enemyDie2 = Resources.Load<AudioClip>("Meow");
        forwardSlash = Resources.Load<AudioClip>("forwardSlash");
        backwardSlash = Resources.Load<AudioClip>("backwardSlash");
        zap = Resources.Load<AudioClip>("Zap");
        click = Resources.Load<AudioClip>("ButtonClick");
        fireball = Resources.Load<AudioClip>("fireball");
        fireballhit = Resources.Load<AudioClip>("explosion");
        line = Resources.Load<AudioClip>("Hit");
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
            case ("GameOver"):
                audioSrc.PlayOneShot(gameOver);
                break;
            case ("EnemyDie"):
                if (audioSrc.isPlaying)
                    audioSrc.Stop();
                audioSrc.PlayOneShot(enemyDie);
                break;
            case ("EnemyDie2"):
                if (audioSrc.isPlaying)
                    audioSrc.Stop();
                audioSrc.PlayOneShot(enemyDie2);
                break;
            case ("ForwardSlash"):
                if (audioSrc.isPlaying)
                    audioSrc.Stop();
                audioSrc.PlayOneShot(forwardSlash);
                break;
            case ("BackwardSlash"):
                if (audioSrc.isPlaying)
                    audioSrc.Stop();
                audioSrc.PlayOneShot(backwardSlash);
                break;
            case ("Zap"):
                if (audioSrc.isPlaying)
                    audioSrc.Stop();
                audioSrc.PlayOneShot(zap);
                break;
            case ("Click"):
                audioSrc.PlayOneShot(click);
                break;
            case ("FireBall"):
                if (audioSrc.isPlaying)
                    audioSrc.Stop();
                  audioSrc.PlayOneShot(fireball);
                break;
            case ("FireBallHit"):
                //if (!audioSrc.isPlaying)
                    //audioSrc.PlayOneShot(fireballhit);
                break;
            case ("Line"):
                if (audioSrc.isPlaying)
                    audioSrc.Stop();
                audioSrc.PlayOneShot(line);
                break;
        }
    }

    public void playClick()
    {
        audioSrc.PlayOneShot(click);
    }

}