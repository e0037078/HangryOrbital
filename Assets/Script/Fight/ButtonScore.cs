using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScore : TouchManager {

    public enum type { Again, BackToMap };
    public type buttonType;

    public GUITexture buttonTexture = null;


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
        SceneManager.LoadScene("City map");
    }
}
