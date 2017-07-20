using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScore : TouchManager {

    public enum type { Again, BackToMap };
    public type buttonType;

    public GUITexture buttonTexture = null;

    public Image black;
    public Animator fadeAnim;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        checkMouseDown();
        touchInput(buttonTexture);
    }

    void OnFirstTouchBegan()
    {
        switch (buttonType)
        {
            case type.Again:
                again();
                break;

            case type.BackToMap:
                backToMap();
                break;
        }
    }
    void again()
    {
        if (ButtonShop.paused)
        {
            ButtonShop.togglePause();
        }
        SaveManager.updateSave();
        PlayGamesScript.Instance.SaveData();
        //SaveManager.loadSave();
        StartCoroutine(FadingIntoFightScene());
    }

    IEnumerator FadingIntoFightScene()
    {
        fadeAnim.SetBool("FadeOut", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void backToMap()
    {
        if (ButtonShop.paused)
        {
            ButtonShop.togglePause();
        }
        SaveManager.updateSave();
        PlayGamesScript.Instance.SaveData();
        //SaveManager.loadSave();
        StartCoroutine(FadingIntoCityMap());
    }

    IEnumerator FadingIntoCityMap()
    {
        fadeAnim.SetBool("FadeOut", true);
        yield return new WaitUntil(() => black.color.a == 1);
        if (gameObject.scene.name == "Fight scene")
            SceneManager.LoadScene("City map");
        else if (gameObject.scene.name == "Fight scene 1")
            SceneManager.LoadScene("Forest map");
    }
}
