using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalManager : MonoBehaviour
{
    public static bool passed = false;
    public static bool unlocked = false;

    public Image black;
    public Animator fadeAnim;

    public GameObject endOfGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (gameObject.scene.name == "Snow map")
            {
                endOfGame.SetActive(true);
            }
            else
            {
                StartCoroutine(FadingIntoCityMap());
            }
        }
    }
    IEnumerator FadingIntoCityMap()
    {
        fadeAnim.SetBool("FadeOut", true);

        yield return new WaitUntil(() => black.color.a == 1);
        switch (gameObject.scene.name)
        {
            case ("City map"):
                SaveManager.Instance.resetMap();
                SaveManager.Instance.level++;
                SaveManager.Instance.playerPos = Vector3.zero;
                PlayGamesScript.Instance.SaveData();
                SceneManager.LoadScene("Forest map");
                break;
            case ("Forest map"):
                SaveManager.Instance.resetMap();
                SaveManager.Instance.level++;
                SaveManager.Instance.playerPos = Vector3.zero;
                PlayGamesScript.Instance.SaveData();
                SceneManager.LoadScene("Snow map");
                break;
        }
    }
}
