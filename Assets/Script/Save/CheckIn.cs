using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckIn : MonoBehaviour {
    public GameObject[] Rewards;
    bool colored = false;

    public GameObject unsuccessfulDisplay;
    public GameObject successfulDisplay;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (!colored && SaveManager.Instance.numChecked != 0 )
        {
            colored = true;
            for (int i = 0; i < SaveManager.Instance.numChecked; i++)
            {
                Rewards[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }

        }
        if (SaveManager.Instance.checkInAvailable)
        {
            int numCheck = SaveManager.Instance.numChecked;
            SaveManager.Instance.checkInAvailable = false;
            Rewards[numCheck].GetComponent<Button>().onClick.AddListener(() => 
            {
                SaveManager.Instance.numChecked += 1;
                SaveManager.Instance.calculateDailyBenefits();
                Rewards[numCheck].GetComponent<Button>().onClick.RemoveAllListeners();
                Rewards[numCheck].GetComponent<Button>().onClick.AddListener(() => showGotten());
                Rewards[numCheck].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
                successfulDisplay.SetActive(true);
                successfulDisplay.GetComponent<Button>().onClick.AddListener(() => successfulDisplay.SetActive(false));
                StartCoroutine(closeDisplayAfterTime(1f, successfulDisplay));

            });
            for (int i = numCheck + 1; i < SaveManager.Instance.totNumDaily; i++)
            {
                Rewards[i].GetComponent<Button>().onClick.AddListener(() => showWaitTomorrow());
            }
            for (int i = 0; i < numCheck; i++)
            {
                Rewards[i].GetComponent<Button>().onClick.AddListener(() => showGotten());
            }
        }

        
    }
    void showGotten()
    {
        unsuccessfulDisplay.GetComponentInChildren<Text>().text = "You already have this upgrade";
        unsuccessfulDisplay.SetActive(true);
        unsuccessfulDisplay.GetComponent<Button>().onClick.AddListener(() => unsuccessfulDisplay.SetActive(false));
        StartCoroutine(closeDisplayAfterTime(1f, unsuccessfulDisplay));
        Debug.Log("I'm gotten Working");
    }

    void showWaitTomorrow()
    {
        unsuccessfulDisplay.GetComponentInChildren<Text>().text = "You have to wait till tomorrow";
        unsuccessfulDisplay.SetActive(true);
        unsuccessfulDisplay.GetComponent<Button>().onClick.AddListener(() => unsuccessfulDisplay.SetActive(false));
        StartCoroutine(closeDisplayAfterTime(1f, unsuccessfulDisplay));

        Debug.Log("I'm Working");
    }
    IEnumerator closeDisplayAfterTime(float waitTime, GameObject display)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        display.SetActive(false);
    }
}
    
