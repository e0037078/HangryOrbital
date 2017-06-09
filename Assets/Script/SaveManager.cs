using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    static SaveManager Instance;
    public float BaseHP;
    public float DPS;
    public float gold;
    public int level;

    public int numOfUpgrades = 5;
    public float[] costs;
    public int[] upgrades;
    
    //TODO Actual Save

	// Use this for initialization
	void Start () {
        //Basically make sure that there is only one Instance of SaveManager
        if(Instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        //Protects SaveManager from Being Destroyed when changing Scene
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        //5 is estimated number of upgrades
        costs =  new float[numOfUpgrades];
        upgrades = new int[numOfUpgrades]; 
        for(int i = 0; i < numOfUpgrades; i++)
        {
            //Initialization of all values
            upgrades[i] = 0;
            //initialised as 1 2 3 4 5.
            costs[i] = i;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool buyUpgrade(int index)
    {
        if(gold < costs[index])
        {
            return false;
        }
        else
        {
            costs[index] *= 1.08f; //8% increase temp value
            gold -= costs[index];
            upgrades[index]++;  
        }
        return true;
    }

    void calculateDPS()
    {
        float tempDPS = 0;
        for(int i = 0; i < numOfUpgrades; i++)
        {
            //Temp upgrade 1 adds HP
            if(i != 1)
                //Temp formula for DPS
                tempDPS += i * upgrades[i]; 
        }
        DPS = tempDPS;
    }

    void calculateHP()
    {
        float tempHP = 0;
        for(int i = 0; i < numOfUpgrades; i++)
        {
            if(i == 1)
            {
                //Temp formula for HP
                tempHP += i * upgrades[i];
            }
        }
        BaseHP = tempHP;
    }
}
