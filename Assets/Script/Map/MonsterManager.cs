using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MonsterManager : MonoBehaviour {
    public static MonsterManager Instance;
    public GameObject[] monsters;
    public Text numMonstersLeft;
    public Image monsterImage;
    public GameObject portalPanel;
    bool unlocked = false;

	// Use this for initialization
	void Start () {
        //Singleton Code
        if (Instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        int index = -1;
        switch (monsterImage.gameObject.scene.name)
        {
            case ("City map"):
                index = 0;
                break;
            case ("Forest map"):
                index = 1;
                break;
        }
        if (index >= 0)
            monsterImage.sprite = monsters[index].gameObject.GetComponent<SpriteRenderer>().sprite;

        int monstersLeft = SaveManager.Instance.monsterToClear - SaveManager.Instance.monsterCleared;
        numMonstersLeft.text = monstersLeft.ToString() + " left";

        updateMonsters();
	}
	
	// Update is called once per frame
	void Update ()
    {
        int monstersLeft = SaveManager.Instance.monsterToClear - SaveManager.Instance.monsterCleared;
        numMonstersLeft.text = monstersLeft.ToString() + " left";
        if (monstersLeft == 0 && !unlocked)
        {
            // unlock portal;
            unlocked = true;
            portalPanel.SetActive(true);
        }
    }

    public void encounteredMonsterLevel(GameObject monster)
    {
        for( int i = 0; i < monsters.Length; i++)
        {
            if(monsters[i] == monster)
            {
                //Found
                SaveManager.Instance.monsterLevel = i + 1;
                return;
            } 
        }
        Debug.Log("BUG!!!!!! Cannot find monster in list, shouldnt happen!!");
    }

    public void updateMonsters()
    {
        /*
        //Getting the Monsters Cleared 
        int cleared = SaveManager.Instance.monsterCleared;
        int i = 0;
        while (cleared > 0)
        {
            //Stored as Binary , %2 gets the last bit
            if (cleared % 2 == 1)
            {
                //1 means cleared hence deleted
                Destroy(monsters[i]);
            }
            cleared /= 2;
            i++;
        }
        */
    }
}
