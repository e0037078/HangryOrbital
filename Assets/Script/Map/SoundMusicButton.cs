using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundMusicButton : MonoBehaviour {
    public Button musicOn;
    public Button musicOff;
    public Button sfxOn;
    public Button sfxOff;

    public AudioSource sfxSrc;
    public AudioSource musicSrc;

    // Use this for initialization
    void Start ()
    {
        musicOn.onClick.AddListener(ToggleMusic);
        musicOff.onClick.AddListener(ToggleMusic);

        sfxOn.onClick.AddListener(ToggleSfx);
        sfxOff.onClick.AddListener(ToggleSfx);
        
        if (SaveManager.muteBGM)
        {
            MusicManager.muteMusic = false;
            ToggleMusic();
        }
        else if (!SaveManager.muteBGM)
        {
            MusicManager.muteMusic = true;
            ToggleMusic();

            if (musicOn.gameObject.scene.name == "Fight scene")
                MusicManager.PlayBGM("BGM2");
            else
                MusicManager.PlayBGM("BGM1");
        }

        if (SaveManager.muteSfx)
        {
            SfxManager.muteSfx = false;
            ToggleSfx();
        }
        else if (!SaveManager.muteSfx)
        {
            SfxManager.muteSfx = true;
            ToggleSfx();
        }
    }

    private void Update()
    {
        if ((!SaveManager.muteBGM && MusicManager.muteMusic) || (SaveManager.muteBGM && !MusicManager.muteMusic))// supposed to play
        {
            ToggleMusic();
        }
        if ((!SaveManager.muteSfx && SfxManager.muteSfx) || (SaveManager.muteSfx && !SfxManager.muteSfx)) // supposed to play
        {
            ToggleSfx();
        }
    }

    public void ToggleMusic()
    {
        if (!SfxManager.muteSfx)
            SfxManager.PlaySound("Click");
        MusicManager.muteMusic = !MusicManager.muteMusic;
        if (MusicManager.muteMusic)
        {
            musicSrc.Stop();
            musicSrc.volume = 0f;
            musicSrc.mute = true;
            SaveManager.muteBGM = true;

            musicOff.gameObject.SetActive(true);
            musicOn.gameObject.SetActive(false);
        }
        else
        {
            musicSrc.mute = false;
            SaveManager.muteBGM = false;
            musicSrc.volume = 0.5f;
            
            if (gameObject.scene.name == "Fight scene")
                MusicManager.PlayBGM("BGM2");
            else
                MusicManager.PlayBGM("BGM1");
            musicOn.gameObject.SetActive(true);
            musicOff.gameObject.SetActive(false);
        }
    }

    public void ToggleSfx()
    {
        if (!SfxManager.muteSfx)
            SfxManager.PlaySound("Click");
        SfxManager.muteSfx = !SfxManager.muteSfx;
        if (SfxManager.muteSfx)
        {
            sfxSrc.Stop();
            sfxSrc.mute = true;
            SaveManager.muteSfx = true;

            sfxOff.gameObject.SetActive(true);
            sfxOn.gameObject.SetActive(false);
        }
        else
        {
            sfxSrc.mute = false;
            SaveManager.muteSfx = false;
            sfxOn.gameObject.SetActive(true);
            sfxOff.gameObject.SetActive(false);
        }
    }
}
