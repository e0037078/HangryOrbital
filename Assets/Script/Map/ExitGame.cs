using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {

    public Button exitYes = null;
    public Button exitNo = null;
    public GameObject confirmPrompt = null;

    Button exitGame = null;

    // Use this for initialization
    void Start () {
        exitGame = gameObject.GetComponent<Button>();
        exitGame.onClick.AddListener(endGamePrompt);
        confirmPrompt.SetActive(false);
    }

    void endGamePrompt()
    {
        SfxManager.PlaySound("Click");
        confirmPrompt.SetActive(true);
        exitYes.onClick.AddListener(endGame);
        exitNo.onClick.AddListener(continueGame);
    }

    void continueGame()
    {
        SfxManager.PlaySound("Click");
        confirmPrompt.SetActive(false);
    }

    void endGame()
    {
        Application.Quit();
    }
}
