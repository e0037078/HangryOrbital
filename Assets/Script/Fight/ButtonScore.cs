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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void backToMap()
    {
        SceneManager.LoadScene("City map");
    }
}
