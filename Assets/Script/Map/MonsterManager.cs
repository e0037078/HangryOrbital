using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {
    public static MonsterManager Instance;
    public GameObject[] monsters;

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

        updateMonsters();
	}
	
	// Update is called once per frame
	void Update () {
		
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
    }
}
