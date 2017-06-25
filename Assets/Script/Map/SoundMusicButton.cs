using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        musicOn.gameObject.SetActive(true);
        musicOff.gameObject.SetActive(false);
        sfxOn.gameObject.SetActive(true);
        sfxOff.gameObject.SetActive(false);

        MusicManager.PlayBGM("BGM1");
    }

    public void ToggleMusic()
    {
        if (!SfxManager.muteSfx)
            SfxManager.PlaySound("Click");
        MusicManager.muteMusic = !MusicManager.muteMusic;
        if (MusicManager.muteMusic)
        {
            musicSrc.Stop();
            musicSrc.mute = true;

            musicOff.gameObject.SetActive(true);
            musicOn.gameObject.SetActive(false);
        }
        else
        {
            musicSrc.mute = false;
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

            sfxOff.gameObject.SetActive(true);
            sfxOn.gameObject.SetActive(false);
        }
        else
        {
            sfxSrc.mute = false;
            sfxOn.gameObject.SetActive(true);
            sfxOff.gameObject.SetActive(false);
        }
    }
}
