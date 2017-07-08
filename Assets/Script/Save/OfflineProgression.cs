using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineProgression : MonoBehaviour {

    public GameObject offlineScreen;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (SaveManager.Instance.offlineProgress&&!SaveManager.Instance.offlineShown)
        {
            offlineScreen.SetActive(true);
            GUIText[] allText = offlineScreen.GetComponentsInChildren<GUIText>();
            //Find the Text to change to correct amount
            foreach (GUIText i in allText)
            {
                if (i.text == "XXX Gold")
                {
                    float goldEarned = SaveManager.Instance.offlineGoldEarned;
                    int timeTaken = SaveManager.Instance.offlineTime;
                    if (timeTaken < 60)
                    {
                        i.text = goldEarned.ToString(".##") + " Gold for " + timeTaken + "min";
                    }
                    else    
                    {
                        i.text = goldEarned.ToString(".##") + " Gold for " + ((float)(timeTaken/60)).ToString(".##") + "hr";
                    }
                    break;
                }
            }
            SaveManager.Instance.offlineShown = true;
        }
    }
}
