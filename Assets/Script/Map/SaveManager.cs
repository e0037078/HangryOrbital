using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

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
    public float goldEarned;

    public bool isFighting = false;
    Scene currentScene;

    public GameObject[] enemy;
    public GameObject[] enemyBoss;

    public double[] gestureProb = new double[5];
    public double[] gestureDMG = new double[5];

    public int monsterCleared = 0;
    public int monsterToClear = 0;

    public Vector3 playerPos;

    //Overall Multiplier
    public OverallStatsMultiplier multipliers = new OverallStatsMultiplier();

    //SAVE DATA 
    [Header("Save Data")]
    public float[] SaveArray;
    public static int[] SAVE { get; set; }

    //Offline Progress
    [Header("Offline Progress")]
    public bool offlineProgress = true;
    public bool offlineShown = false;
    public float offlineGoldEarned;
    public int offlineTime;

    //Daily Check-in Reward
    [Header("Daily Check-in Reward")]
    public bool checkInAvailable = false;
    public int numChecked;
    public int totNumDaily = 6;
    public GameObject CheckInPanel;
    public Button checkInButton;

    [Header("Music")]
    public static bool muteSfx;
    public static bool muteBGM;

    [Header("Gender")]
    public static bool isBoy = true;

    // Use this for initialization
    void Awake () {

        SAVE = new int[23];
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
        calculateMonsterToClear();

        muteSfx = SfxManager.muteSfx;
        muteBGM = MusicManager.muteMusic;
    }
	
	// Update is called once per frame
	void Update () {
        //checks if Scene have Changed
        if (currentScene != SceneManager.GetActiveScene())
        {
            if((SceneManager.GetActiveScene().name == "City map" || SceneManager.GetActiveScene().name == "Forest map" || SceneManager.GetActiveScene().name == "Snow map"))
            {
                isFighting = false;
            }
            else
            {
                isFighting = true;
            }

            if(!isFighting && (currentScene.name != "City map"))
            {
                playerSpawnPos(playerPos);
            }
            calculateMonsterStats();
            goldEarned = 0;
            currentScene = SceneManager.GetActiveScene();
            
        }
        /*
         * don't need, see monstermanager
        if (monsterToClear - monsterCleared == 0)
        {
            // portal unlocked, level ++ if portal unlocked, delete all monsters in map
            DestroyAllCityMonsters();
        }
        */
        
        muteSfx = SfxManager.muteSfx;
        muteBGM = MusicManager.muteMusic;

        if (!isBoy)
            changeToGirl();
    }

    public void toggleGender()
    {
        isBoy = !isBoy;
    }

    void changeToGirl()
    {
        GameObject player = GameObject.Find("Boy");
        Animator animator = player.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Girl");
        //Sprite playerSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        //playerSprite = Resources.Load<Sprite>("girlSprite");
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
        gold += goldDrop;
        goldEarned += goldDrop;

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
                tempDPS += 10 * Mathf.Pow(1.07f, upgrades[i]); 
        }
        DPS = Mathf.Round(tempDPS * multipliers.DPS +5);
    }

    void calculateHP()
    {
        float tempHP = 0;
        for(int i = 0; i < numOfUpgrades; i++)
        {
            if(i == 1)
            {
                //Temp formula for HP
                tempHP += 100 * Mathf.Pow(1.07f, upgrades[i]);
            }
        }
        BaseHP = Mathf.Round(tempHP * multipliers.HP);
    }

    public void calculateMonsterStats()
    {
        //Get highest DPS gesture
        float topGestureDPS = 0;
        foreach(float i in gestureDMG)
        {
            if(topGestureDPS < i)
            {
                topGestureDPS = i;
            }
        }
        //Temp formula for goldDrop
        goldDrop = Mathf.Round((float)(DPS * 0.5 + topGestureDPS + BaseHP * 0.5) * (level + 1) + 20);
        monsterDPS = Mathf.Round((float)(DPS * 0.2 + topGestureDPS + BaseHP * 0.075) * (level + 1) + 20);
        monsterHP = Mathf.Round((float)(DPS * 0.2 + topGestureDPS + BaseHP * 0.25) * (level+1) + 20);
    }

    void calculateMonsterToClear()
    {
        // temp formula;
        monsterToClear = level + 3;
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
                    gestureDMG[i] = Mathf.Round(6 + upgrades[2] * 1.6f * multipliers.GestureDMG);
                    break;
                case 1:
                    gestureDMG[i] = Mathf.Round(8 + upgrades[4] * 1.6f * multipliers.GestureDMG);
                    break;
                case 2:
                    gestureDMG[i] = Mathf.Round(8 + upgrades[6] * 1.8f * multipliers.GestureDMG);
                    break;
                case 3:
                    gestureDMG[i] = Mathf.Round(10 + upgrades[8] * 2.0f * multipliers.GestureDMG);
                    break;
                case 4:
                    gestureDMG[i] = Mathf.Round(12 + upgrades[10] * 2.4f * multipliers.GestureDMG);
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
                costs[i] = Mathf.Round(50f * Mathf.Pow(1.08f, upgrades[i]));
            else
                costs[i] = Mathf.Round(i * 50f * Mathf.Pow(1.08f, upgrades[i]));
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

    public void wonLevel()
    {
        //monsterCleared += (int)Mathf.Pow(2f, (float)(Level - 1));
        monsterCleared++;
    }
    void DestroyAllCityMonsters()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("City Monster");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
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
                    SAVE[i] = SaveManager.Instance.monsterCleared;
                    break;
                case 15:
                    SAVE[i] = SaveManager.Instance.monsterToClear;
                    break;
                case 16:
                    SAVE[i] = (int)SaveManager.Instance.playerPos.x;
                    break;
                case 17:
                    SAVE[i] = (int)(SaveManager.Instance.playerPos.x * 1000000 % 1000000);
                    break;
                case 18:    
                    SAVE[i] = (int)SaveManager.Instance.playerPos.y;
                    break;
                case 19:
                    SAVE[i] = (int)(SaveManager.Instance.playerPos.y * 1000000 % 1000000);
                    break;
                case 20:
                    SAVE[i] = SaveManager.Instance.numChecked;
                    break;
                case 21:
                    //Date of Now , DDMM
                    SAVE[i] = DateTime.Now.Day * 100 + DateTime.Now.Month;
                    break;
                case 22:
                    SAVE[i] = getTime();
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
                    SaveManager.Instance.StartCoroutine(SaveManager.Instance.updateMap());
                    break;
                case 13:
                    SaveManager.Instance.gold = SAVE[i];
                    break;
                case 14:
                    SaveManager.Instance.monsterCleared = SAVE[i];
                    break;
                case 15:
                    SaveManager.Instance.monsterToClear = SAVE[i];
                    MonsterManager.Instance.updateMonsters();
                    break;
                case 16:
                case 17:
                case 18:
                    break;
                case 19:
                    //Since Stored as int need convert back to float
                    SaveManager.Instance.playerSpawnPos((float)(SAVE[16] + (float)(SAVE[17] / 1000000)), (float)(SAVE[18] + (float)(SAVE[19] / 1000000)));
                    break;
                case 20:
                    SaveManager.Instance.numChecked = SAVE[i];
                    break;
                case 21:
                    SaveManager.Instance.checkDaily(SAVE[i]);
                    break;
                case 22:                    
                    SaveManager.Instance.calculateOfflineProgress(getTime() - SAVE[i]);
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
            checkInButton.onClick.Invoke();
            return;
        }
        //%100 gets the month
        if(DateTime.Now.Month > daymonth % 100)
        {
            checkInAvailable = true;
            checkInButton.onClick.Invoke();
        }
        // Month same, check day
        else if(DateTime.Now.Day > daymonth / 100)
        {
            checkInAvailable = true;
            checkInButton.onClick.Invoke();
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
        if (offlineShown)
        {
            return;
        }
        Debug.Log("(Hangry)Gained :" + (time / 60) + " for " + time + "s");
        if(time/60 > 0 )
        {
            offlineProgress = true;
        }
        gold += time/60;
        if (time / 60 / 60 > 10000) 
        {
            return;
        }
        offlineGoldEarned = time / 60;
        offlineTime = time / 60;
    }

    void playerSpawnPos(float x , float y)
    {
        if(x == 0 || y == 0)
        {
            return;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.localPosition = new Vector3(x, y, player.transform.localPosition.z);
    }
    void playerSpawnPos(Vector3 pos)
    {
        if (pos.x == 0 || pos.y == 0)
        {   
            return;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.localPosition = pos;
    }

    public void savePlayerPos()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.localPosition; 
    }
    IEnumerator updateMap()
    {
        switch (level)
        {
            case 0:
                break;
            case 1:
                if(currentScene.name == "City map")
                {
                    SceneManager.LoadScene("Forest map");
                    yield return new WaitForSecondsRealtime(1f);
                    SaveManager.Instance.playerSpawnPos((float)(SAVE[16] + (float)(SAVE[17] / 1000000)), (float)(SAVE[18] + (float)(SAVE[19] / 1000000)));
                    offlineShown = false;
                    SaveManager.Instance.checkDaily(SAVE[21]);
                    SaveManager.Instance.calculateOfflineProgress(getTime() - SAVE[22]);


                }
                break;
            case 2:
                if (currentScene.name == "City map")
                {
                    SceneManager.LoadScene("Snow map");
                    yield return new WaitForSecondsRealtime(1f);
                    SaveManager.Instance.playerSpawnPos((float)(SAVE[16] + (float)(SAVE[17] / 1000000)), (float)(SAVE[18] + (float)(SAVE[19] / 1000000)));
                    offlineShown = false;
                    SaveManager.Instance.checkDaily(SAVE[21]);
                    SaveManager.Instance.calculateOfflineProgress(getTime() - SAVE[22]);


                }
                break;
        }
    }

    #endregion /Save
    static void printLoop<T>(T arr) where T : IList
    {
        for (int i = 0; i < arr.Count; i++)
            Debug.Log("(Hangry)" + arr[i].ToString());
    }

    //Called in PortalManager
    public void resetMap()
    {
        monsterCleared = 0;
        calculateMonsterToClear(); // level+1
        PortalManager.passed = false;
        PortalManager.unlocked = false;
        playerPos = Vector3.zero;
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

