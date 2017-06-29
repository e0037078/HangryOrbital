using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckIn : MonoBehaviour {
    public GameObject[] Rewards;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (SaveManager.Instance.numChecked != 0 && Rewards[SaveManager.Instance.numChecked - 1].GetComponent<Image>().color.a == 1f)
        {
            for (int i = 0; i < SaveManager.Instance.numChecked; i++)
            {
                Rewards[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            }

        }
        if (SaveManager.Instance.checkInAvailable)
        {
            int numCheck = SaveManager.Instance.numChecked;
            SaveManager.Instance.checkInAvailable = false;
            Rewards[numCheck].GetComponent<Button>().onClick.AddListener(() => { SaveManager.Instance.numChecked += 1; SaveManager.Instance.calculateDailyBenefits(); Rewards[numCheck].GetComponent<Button>().onClick.RemoveAllListeners(); });
            for (int i = numCheck + 1; i < SaveManager.Instance.totNumDaily; i++)
            {
                Rewards[i].GetComponent<Button>().onClick.AddListener(() => showComment());
            }
        }

        
    }
    void showComment()
    {
        //TODO Show Text when cant get already
        // LIke show a text "Already got for alreadty have"(this need some debugging, line 30 accidentally removes it)
        // and "wait tmr for others"
        // Not sure where to place it though 
        Debug.Log("I'm Working");
    }
}
    
