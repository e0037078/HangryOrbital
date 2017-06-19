using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBoss : TouchManager {

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
        FightManager.spawnBoss = true;
    }
}
