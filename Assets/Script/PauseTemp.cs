﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseTemp : MonoBehaviour {

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(changePaused);
    }
    
    void changePaused()
    {
        ButtonShop.togglePause();
    }


}
