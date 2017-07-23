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

	// Update is called once per frame
	void Update () {
        checkMouseDown();
        touchInput(buttonTexture);
    }

    void OnFirstTouchBegan()
    {
        FloatingTextController.pause = false;
        Debug.Log("floating text controller pause = false");
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
        StartCoroutine(FadingIntoFightScene());
        if (ButtonShop.paused)
        {
            ButtonShop.togglePause();
        }
        
        SaveManager.updateSave();
        PlayGamesScript.Instance.SaveData();
    }

    IEnumerator FadingIntoFightScene()
    {
        fadeAnim.SetBool("FadeOut", true);
        ButtonShop.togglePause();

        yield return new WaitUntil(() => black.color.a == 1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (gameObject.scene.name == "Fight scene")
        {
            SceneManager.LoadScene("Fight scene");
        }
        else if (gameObject.scene.name == "Fight scene 1")
        {
            SceneManager.LoadScene("Fight scene 1");
        }
        else if (gameObject.scene.name == "Fight scene 2")
        {
            SceneManager.LoadScene("Fight scene 2");
        }
    }

    void backToMap()
    {
        StartCoroutine(FadingIntoCityMap());
        if (ButtonShop.paused)
        {
            ButtonShop.togglePause();
        }
        SaveManager.updateSave();
        PlayGamesScript.Instance.SaveData();
    }

    IEnumerator FadingIntoCityMap()
    {
        fadeAnim.SetBool("FadeOut", true);

        yield return new WaitUntil(() => black.color.a == 1);
        if (gameObject.scene.name == "Fight scene")
            SceneManager.LoadScene("City map");
        else if (gameObject.scene.name == "Fight scene 1")
            SceneManager.LoadScene("Forest map");
        else if (gameObject.scene.name == "Fight scene 2")
            SceneManager.LoadScene("Snow map");
    }
}
