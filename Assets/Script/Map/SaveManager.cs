using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveManager : MonoBehaviour {

    public static SaveManager Instance;
    public float BaseHP;
    public float DPS;
    public float gold;
    public int level;

    public int numOfUpgrades = 12;
    public float increaseDPS;
    public float increaseHP;
    public float[] costs = new float[12];
    public int[] upgrades = new int[12];

    public float monsterDPS;
    public float monsterHP;

    public float goldDrop;

    Scene currentScene;
    public float goldBef;


    public GameObject enemy;
    public GameObject enemyBoss;

    public double[] gestureProb = new double[4];


    //TODO Actual Save

    // Use this for initialization
    void Awake () {

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
        currentScene = SceneManager.GetActiveScene();


        calculateDPS();
        calculateHP();
        calculateMonsterStats();
    }
	
	// Update is called once per frame
	void Update () {
        //checks if Scene have Changed
        if (currentScene != SceneManager.GetActiveScene())
        {
            calculateMonsterStats();
            goldBef = gold;
            currentScene = SceneManager.GetActiveScene();
        }
	}

    public bool buyUpgrade(int index)
    {
        index--;
        if(gold < costs[index])
        {
            return false;
        }
        else
        {
            gold -= costs[index];
            upgrades[index]++;
            costs[index] *= 1.08f; //exponential 8% increase temp value
        }
        calculateDPS();
        calculateHP();
        Debug.Log("DPS"+DPS);
        Debug.Log("HP"+BaseHP);
        return true;
    }

    public void addGold()
    {
        Debug.Log(gold+" "+goldDrop);
        gold += goldDrop;
        Debug.Log(gold);

    }

    void calculateDPS()
    {
        float tempDPS = 0;
        for(int i = 0; i < numOfUpgrades; i++)
        {
            //Temp upgrade 1 adds HP
            if(i != 1)
                //Temp formula for DPS
                tempDPS += (i+1) * upgrades[i]; 
        }
        DPS = tempDPS+5;
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

    void calculateMonsterStats()
    {
        //Temp formula for goldDrop
        goldDrop = (float)(10 + level * 0.8 + DPS * 0.5 + BaseHP * 0.5);
        monsterDPS = (float)(5 + level * 0.6 + DPS * 0.6 + BaseHP * 0.6);
        monsterHP = (float)(10 + level * 0.75 + DPS * 0.6 + BaseHP * 0.6);
    }

    void calculateGestureProbability()
    {
        for(int i=0; i < gestureProb.Length;i++)
        {
            switch (i)
            {
                case 1:
                    //Temp formula
                    gestureProb[i] = Mathf.Pow(1.07f, (float)upgrades[4]);
                    break;
                case 2:
                    gestureProb[i] = Mathf.Pow(1.07f, (float)upgrades[6]);
                    break;
                case 3:
                    gestureProb[i] = Mathf.Pow(1.07f, (float)upgrades[8]);
                    break;
                case 4:
                    gestureProb[i] = Mathf.Pow(1.07f, (float)upgrades[10]);
                    break;
            }
        }
    }

    public static double calculateFixedUpdateProbability(double probability)
    {
        return Mathf.Pow(10, Mathf.Log10((float)probability) / 3000);
    }
}
