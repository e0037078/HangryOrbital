using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShop : TouchManager {

    public enum type {SettingToggleButton, ShopToggleButton};
    public type buttonType;
    public Button settingResume = null;

    public GameObject settingMenu = null;
    Animator settingAnim;
    public static bool paused = false;
    static float originalTimeScale;
    static bool settingOn = false;

    public GameObject shopMenu = null;

    // Animator shopAnim;
    static bool shopOn = false;

    //Hiding Check in 
    public GameObject checkInButton = null;
        
    SaveManager save;


    public GUITexture buttonTexture = null;
    // Use this for initialization
    void Start () {
        shopMenu.GetComponent<Canvas>().enabled = false;
        settingMenu.GetComponent<Canvas>().enabled = false;

        // shopAnim = shopMenu.GetComponent<Animator>();
        //settingAnim = settingMenu.GetComponent<Animator>();

        save = SaveManager.Instance;
        settingResume.onClick.AddListener(ResumeGame);

        if (paused)
        {
            togglePause();
        }
    }

    // Update is called once per frame
    void Update () {
        checkMouseDown();
        touchInput(buttonTexture);
    }

    void ResumeGame()
    {
        SfxManager.PlaySound("Click");

        if (settingOn)
        {
            settingMenu.GetComponent<Canvas>().enabled = false;
            settingOn = false;
            paused = true;
            togglePause();
            Debug.Log("resume game");
        }
    }

    void OnFirstTouchBegan()
    {
        if(checkInButton != null)
        {
            if (checkInButton.activeSelf)
            {
                checkInButton.SetActive(false);
            }
            else
            {
                checkInButton.SetActive(true);
            }
        }
        switch (buttonType)
        {
            case type.SettingToggleButton:
                SfxManager.PlaySound("Click");
                toggleSetting();
                break;

            case type.ShopToggleButton:
                SfxManager.PlaySound("Click");
                toggleShop();
                break;
        } 
    }

    void toggleSetting()
    {
        if (!shopOn)
        {
            if (!settingOn)
            {
                settingOn = true;
                settingMenu.GetComponent<Canvas>().enabled = true;

                paused = false;
                togglePause();

            }
            else if (settingOn)
            {
                settingOn = false;
                settingMenu.GetComponent<Canvas>().enabled = false;

                paused = true;
                togglePause();
            }
        }
        else if (shopOn)
        {

            settingOn = true;
            shopOn = false;

            // settingAnim.SetBool("Pause", false);
            shopMenu.GetComponent<Canvas>().enabled = false;
            //  shopAnim.gameObject.SetActive(true);
            settingMenu.GetComponent<Canvas>().enabled = true;
        }

    }

    void toggleShop()
    {
        if (!settingOn)
        {
            if (!shopOn)
            {
                shopOn = true;
                // shopAnim.gameObject.SetActive(true);
                shopMenu.GetComponent<Canvas>().enabled = true;

                paused = false;
                togglePause();
                //shopAnim.SetBool("Pause", true);
            }
            else if (shopOn)
            {
                shopOn = false;
                // shopAnim.gameObject.SetActive(false);
                shopMenu.GetComponent<Canvas>().enabled = false;

                paused = true;
                togglePause();
                //shopAnim.SetBool("Pause", false);
            }
        }
        else if (settingOn)
        {

            shopOn = true;
            settingOn = false;

            // settingAnim.SetBool("Pause", false);
            settingMenu.GetComponent<Canvas>().enabled = false;
            //  shopAnim.gameObject.SetActive(true);
            shopMenu.GetComponent<Canvas>().enabled = true;
        }
    }

    public static void togglePause()
    {
        if (!paused)
        {
            paused = !paused;
            if (Time.timeScale != 0)
                originalTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        else if (paused)
        {
            paused = !paused;
            Time.timeScale = originalTimeScale;
        }
    }
}
