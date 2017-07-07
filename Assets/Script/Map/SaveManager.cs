using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class SaveManager : MonoBehaviour {

    public static SaveManager Instance;
    public float BaseHP;
    public float DPS;
    public float gold = 0;
    public int level;

    public int numOfUpgrades = 12;
    public float increaseDPS;
    public float increaseHP;
    public float[] costs = new float[12];
    public int[] upgrades = new int[12];
    public String[] upgradesDes = new string[12];

    public float monsterDPS;
    public float monsterHP;

    public float goldDrop;

    Scene currentScene;
    public float goldEarned;


    public GameObject enemy;
    public GameObject enemyBoss;

    public double[] gestureProb = new double[5];
    public double[] gestureDMG = new double[5];

    //Overall Multiplier
    OverallStatsMultiplier multipliers = new OverallStatsMultiplier();

    //SAVE DATA 
    public float[] SaveArray;
    public static int[] SAVE { get; set; }

    //Offline Progress
    public bool offlineProgress = false;
    public float offlineGoldEarned;
    public int offlineTime;
    public bool shownOffline = false;

    //Daily Check-in Reward
    public bool checkInAvailable = false;
    public int numChecked;
    public int totNumDaily = 6;
    public GameObject CheckInPanel;

    // Use this for initialization
    void Awake () {

        SAVE = new int[17];
        //Basically make sure that there is only one Instance of SaveManager
        if (Instance != null)
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
        upgradesDes = new string[numOfUpgrades];
        for (int i = 0; i < numOfUpgrades; i++)
        {
            //Initialization of all values
            upgrades[i] = 0;
            upgradesDes[i] = desciption(i);
        }
        //Initialise Costs
        calculateCost();
        currentScene = SceneManager.GetActiveScene();


        calculateDPS();
        calculateHP();

        calculateGestureProbability();
        calculateGestureDMG();
        calculateMonsterStats();
    }
	
	// Update is called once per frame
	void Update () {
        //checks if Scene have Changed
        if (currentScene != SceneManager.GetActiveScene())
        {
            calculateMonsterStats();
            goldEarned = 0;
            currentScene = SceneManager.GetActiveScene();
        }
	}

    public bool buyUpgrade(int index)
    {
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
        calculateGestureProbability();
        calculateGestureDMG();
        Debug.Log("DPS"+DPS);
        Debug.Log("HP"+BaseHP);
        return true;
    }

    public void addGold()
    {
        //Debug.Log(gold+" "+goldDrop);
        gold += goldDrop;
        goldEarned += goldDrop;
        //Debug.Log(gold);

    }
    #region Calc
    string desciption(int index)
    {
        switch (index)
        {
            case 0:
                return "This cake increases the player's health.";
            case 1:
                return "This cake increases the player's passive DPS.";
            case 2:
                return "This cake increases the player's line gesture Damage.";
            case 3:
                return "This cake increases the player's line gesture autocast Probability.";
            case 4:
                return "This donut increases the player's backslash gesture Damage.";
            case 5:
                return "This donut increases the player's backslash gesture autocast Probability.";
            case 6:
                return "This donut increases the player's forward slash gesture Damage.";
            case 7:
                return "This drink increases the player's forward slash gesture autocast Probability.";
            case 8:
                return "This drink increases the player's Lightning Damage.";
            case 9:
                return "This donut increases the player's Lightning autocast Probability.";
            case 10:
                return "This donut increases the player's Fireball Damage.";
            case 11:
                return "This donut increases the player's Fireball autocast Probability.";
        }
        return "Error out of index";
    }
    void calculateDPS()
    {
        float tempDPS = 0;
        for(int i = 0; i < numOfUpgrades; i++)
        {
            //Temp upgrade 1 adds HP
            if(i == 0)
                //Temp formula for DPS
                tempDPS += (i+1) * upgrades[i]; 
        }
        DPS = tempDPS * multipliers.DPS +5;
    }

    void calculateHP()
    {
        float tempHP = 0;
        for(int i = 0; i < numOfUpgrades; i++)
        {
            if(i == 1)
            {
                //Temp formula for HP
                tempHP += 100 + i * upgrades[i];
            }
        }
        BaseHP = tempHP * multipliers.HP;
    }

    void calculateMonsterStats()
    {
        //Temp formula for goldDrop
        goldDrop = (float)(10 + level * 0.8 + DPS * 0.5 + BaseHP * 0.5);
        monsterDPS = (float)(15 + level * 0.5 + DPS * 0.2 + BaseHP * 0.075);
        monsterHP = (float)(20 + level * 0.5 + DPS * 0.2 + BaseHP * 0.25);
    }

    void calculateGestureProbability()
    {
        for (int i = 0; i < gestureProb.Length; i++)
        {
            switch (i)
            {
                case 0:
                    //Temp formula
                    gestureProb[i] = 0.07f + (float)upgrades[3] * 0.07 * multipliers.GestureProb;
                    break;
                case 1:
                    gestureProb[i] = 0.07f + (float)upgrades[5] * 0.07 * multipliers.GestureProb;
                    break;
                case 2:
                    gestureProb[i] = 0.07f + (float)upgrades[7] * 0.07 * multipliers.GestureProb;
                    break;
                case 3:
                    gestureProb[i] = 0.07f + (float)upgrades[9] * 0.07 * multipliers.GestureProb;
                    break;
                case 4:
                    gestureProb[i] = 0.07f + (float)upgrades[11] * 0.07 * multipliers.GestureProb;
                    break;
            }
        }
    }

    void calculateGestureDMG()
    {
        for(int i=0; i < gestureDMG.Length;i++)
        {
            switch (i)
            {
                case 0:
                    //Temp formula
                    gestureDMG[i] = 6 + upgrades[2] * 1.6 * multipliers.GestureDMG;
                    break;
                case 1:
                    gestureDMG[i] = 8 + upgrades[4] * 1.6 * multipliers.GestureDMG;
                    break;
                case 2:
                    gestureDMG[i] = 8 + upgrades[6] * 1.8 * multipliers.GestureDMG;
                    break;
                case 3:
                    gestureDMG[i] = 10 + upgrades[8] * 2.0 * multipliers.GestureDMG;
                    break;
                case 4:
                    gestureDMG[i] = 12 + upgrades[10] * 2.4 * multipliers.GestureDMG;
                    break;
            }
        }
    }


    void calculateCost()
    {
        for(int i = 0;i < numOfUpgrades; i++)
        {
            //temporary formula
            if (i == 0)
                costs[i] = 100f * Mathf.Pow(1.08f, upgrades[i]);
            else
                costs[i] = i * i * 100 * Mathf.Pow(1.08f, upgrades[i]);
        }
    }

    public void calculateDailyBenefits()
    {
        for ( int i = 0; i < numChecked; i++)
        {
            switch (i)
            {
                case 1:
                    multipliers.DPS += 0.05f;
                    break;
                case 2:
                    multipliers.HP += 0.05f;
                    break;
                case 3:
                    multipliers.DPS += 0.10f;
                    break;
                case 4:
                    multipliers.GestureProb *= 1.10f;
                    break;
                case 5:
                    multipliers.GestureDMG += 0.10f;
                    break;
                case 6:
                    multipliers.GameSpeed += 0.10f;
                    break;
            }
        }
        SaveManager.Instance.calculateDPS();
        SaveManager.Instance.calculateHP();

        SaveManager.Instance.calculateGestureProbability();
        SaveManager.Instance.calculateGestureDMG();
    }

    public static double calculateFixedUpdateProbability(double probability)
    {
        return 1-System.Math.Pow(10, System.Math.Log10(probability) / 3000);
    }

    
#endregion /Calc

    #region Save
    /* public float[] buildSaveArray()
     {
         SaveArray = new float[12];
         for(int i = 0; i< 14; i++)
         {
             switch (i)
             {
                 case 1:
                     SaveArray[i] = level;
                     break;
                 case 2:
                     SaveArray[i] = gold;
                     break;

                 default:
                     SaveArray[i] = upgrades[i - 2];
                 break;
             }
         }
         return SaveArray;
     }
     public void loadSaveArray(float[] SaveArray)
     {
         for (int i = 0; i < 14; i++)
         {
             switch (i)
             {
                 case 1:
                     level = (int)SaveArray[i];
                     break;
                 case 2:
                     gold = SaveArray[i];
                     break;

                 default:
                     upgrades[i - 2] = (int)SaveArray[i];
                     break;
             }
         }
         calculateDPS();
         calculateHP();
         calculateCost();

         calculateGestureProbability();
         calculateGestureDMG();
         calculateMonsterStats();
     }*/

    public static void updateSave()
    {
        for (int i = 0; i < SAVE.Length; i++)
        {
            switch (i)
            {
                case 12:
                    SAVE[i] = SaveManager.Instance.level;
                    break;
                case 13:
                    SAVE[i] = (int)SaveManager.Instance.gold;
                    break;
                case 14:
                    SAVE[i] = getTime();
                    break;
                case 15:
                    SAVE[i] = SaveManager.Instance.numChecked;
                    break;
                case 16:
                    //Date of Now , DDMM
                    SAVE[i] = DateTime.Now.Day * 100 + DateTime.Now.Month;
                    break;
                

                default:
                    SAVE[i] = SaveManager.Instance.upgrades[i];
                    break;
            }
        }
        printLoop(SAVE);
    }
    public static void loadSave()
    {
        for (int i = 0; i < SAVE.Length; i++) 
        {
            switch (i)
            {
                case 12:
                    SaveManager.Instance.level = (int)SAVE[i];
                    break;
                case 13:
                    SaveManager.Instance.gold = SAVE[i];
                    break;
                case 14:
                    SaveManager.Instance.calculateOfflineProgress(getTime() - SAVE[i]);
                    break;
                case 15:
                    SaveManager.Instance.numChecked = SAVE[i];
                    break;
                case 16:
                    SaveManager.Instance.checkDaily(SAVE[i]);
                    break;
                

                default:
                    SaveManager.Instance.upgrades[i] = (int)SAVE[i];
                    break;
            }
        }
        //printLoop(SAVE);

        SaveManager.Instance.calculateDailyBenefits();
        
        SaveManager.Instance.calculateCost();

        SaveManager.Instance.calculateMonsterStats();
    }
    //Checks whether the Date saved and load is different 
    //if so set check in avaiable to true.
    public void checkDaily(int daymonth)
    {
        //if new game or havent checked in before
        if(SaveManager.Instance.numChecked == 0)
        {
            checkInAvailable = true;
            CheckInPanel.SetActive(true);
            return;
        }
        //%100 gets the month
        if(DateTime.Now.Month > daymonth % 100)
        {
            checkInAvailable = true;
            CheckInPanel.SetActive(true);
        }
        // Month same, check day
        else if(DateTime.Now.Day > daymonth / 100)
        {
            checkInAvailable = true;
            CheckInPanel.SetActive(true);
        }
    }
    public void toFight()
    {
        SceneManager.LoadScene("Fight scene");

    }
    public void toCity()
    {
        SceneManager.LoadScene("City map");
    }
    public static int getTime()
    {
        return (int)((DateTime.Now.ToUniversalTime() - new DateTime(2000, 1, 1)).TotalSeconds + 0.5);
    }
    void calculateOfflineProgress(int time)
    {
        Debug.Log("(Hangry)Gained :" + (time / 60) + " for " + time + "s");
        gold += time/60;
        if (time / 60 / 60 > 10000) 
        {
            gold = 10f;
        }
        if(time/60 != 0)
        {
            offlineProgress = true;
        }
        offlineGoldEarned = time / 60;
        offlineTime = time / 60;
    }
    #endregion /Save
    static void printLoop<T>(T arr) where T : IList
    {
        for (int i = 0; i < arr.Count; i++)
            Debug.Log("(Hangry)" + arr[i].ToString());
    }
}
[System.Serializable]
public class OverallStatsMultiplier
{
    public float DPS = 1;
    public float HP = 1;
    public float GameSpeed = 1;
    public float GestureDMG = 1;
    public float GestureProb = 1;
}

