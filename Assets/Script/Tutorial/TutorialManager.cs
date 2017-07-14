using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public Image black;
    public Animator fadeAnim;

    public Button quitTutorial;
    public GameObject confirmPrompt;
    public Button yesQuit;
    public Button noQuit;

    private void Start()
    {
        confirmPrompt.SetActive(false);
        quitTutorial.onClick.AddListener(QuitTutorial);
        yesQuit.onClick.AddListener(ReturnToMap);
        noQuit.onClick.AddListener(ReturnToTutorial);
    }

    void QuitTutorial()
    {
        confirmPrompt.SetActive(true);
    }

    void ReturnToMap()
    {
        StartCoroutine(FadingIntoCityMap());
    }

    IEnumerator FadingIntoCityMap()
    {
        fadeAnim.SetBool("FadeOut", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene("City map"); // will need to change depending on levels
    }

    void ReturnToTutorial()
    {
        confirmPrompt.SetActive(false);
    }

}
