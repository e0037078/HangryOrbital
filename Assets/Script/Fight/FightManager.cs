using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour {

    //public static FightManager Instance;

    public static GameObject[] allEnemies;
    public static GameObject[] allPlayers ;

    public static GameObject currEnemy = null;
    public static GameObject currPlayer = null;

    float playerDamage;
    float nextDamage;

    //Enemy Prefab to spawn
    public int killsToActivateBoss;
    public GameObject bossButton;
    static bool bossSpawned;
    public static bool winMap;
    public GameObject scoreScreen;

    public static int monsterDeathCounter = 0;

    // Use this for initialization
    void Start () {

        winMap = false;
        bossSpawned = false;

        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        nextDamage = Time.time;

        //Initialisation to random
        currPlayer = allPlayers[0];

        spawnEnemy();
        currEnemy = GetClosestEnemy(currPlayer);
        currPlayer = GetClosestPlayer(currEnemy);


        playerDamage = SaveManager.Instance.DPS;
        monsterDeathCounter = 0;
    }

    void FixedUpdate()
    {
        // player's attacks onto monster
        dmgEnemy();


        // enemy's attacks onto player 
        //dmgPlayer();
    }

    // Update is called once per frame
    void Update () {

        if (currEnemy == null || currEnemy.GetComponent<enemyHealth>().currentHealth <= 0 && !bossSpawned)
        {
            currEnemy = GetClosestEnemy(currPlayer);
            if (currEnemy == null && !winMap && !bossSpawned)
            {
                //if all enemies are dead, spawn more unless won Map
                //SpawnManager.spawnEnemy(); 
                spawnEnemy();
                currEnemy = GetClosestEnemy(currPlayer);

            }
        }
        if (currPlayer == null)
        {
            currPlayer = GetClosestPlayer(currEnemy);
        }

        if (!bossSpawned && monsterDeathCounter >= killsToActivateBoss)
        {
            bossButton.SetActive(true);
        }


        //Checks whether map won
        if (winMap && !scoreScreen.activeInHierarchy)
        {
            win();
        }
        
    }
    /*
    public static float getTotalEnemyDamage()
    {
        float sum = 0.0f;
        foreach (GameObject t in allEnemies)
        {
            if (t == null)
                continue;
            sum += t.GetComponent<enemyDamage>().damage;
        }
        return sum;
    }*/


    public static GameObject GetClosestEnemy(GameObject currPlayer)
    {
       /* if(currPlayer == null)
        {
            allPlayers = GameObject.FindGameObjectsWithTag("Player");
            currPlayer = allPlayers[0];

            spawnEnemy();
        }*/
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = currPlayer.transform.position;
        foreach (GameObject potentialTarget in allEnemies)
        {
            if (potentialTarget == null)//added to prevent exception when potentialTarget is null
            {
                continue;
            }
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
             
            }
        }
        if (bestTarget == null)
            return null;
        return bestTarget.gameObject;
    }

    GameObject GetClosestPlayer(GameObject currEnemy)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = currEnemy.transform.position;
        foreach (GameObject potentialTarget in allPlayers)
        {
            if (potentialTarget == null)//added to prevent exception when potentialTarget is null
            {
                continue;
            }
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        if (bestTarget == null)
            return null;
        return bestTarget.gameObject;
    }

    void dmgEnemy()
    {

        if (currEnemy == null) // no more enemies
        {
            // yay end
            Debug.Log("enemy null");
        }
        // attacks only affect currEnemy in camera view and not the rest
        else if (currEnemy.GetComponent<SpriteRenderer>().isVisible) 
        {
            //Damage every 0.02s i.e. 50/sec
            currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(playerDamage/50);
        }
    }

    void spawnEnemy()
    {
        int index = 0;
        switch (gameObject.scene.name)
        {
            case ("Fight scene"):
                break;
            case ("Fight scene 1"):
                index = 1;
                break;
            case ("Fight scene 2"):
                index = 2;
                break;
        }
        setEnemyStats(Instantiate(SaveManager.Instance.enemy[index], (Vector2)currPlayer.transform.position + new Vector2(10f, 1f), currPlayer.transform.rotation));
        setEnemyStats(Instantiate(SaveManager.Instance.enemy[index], (Vector2)currPlayer.transform.position + new Vector2(15f, 1f), currPlayer.transform.rotation));

        allEnemies = GameObject.FindGameObjectsWithTag("Monster");

    }
    static void setEnemyStats(GameObject enemy)
    {
        SaveManager.Instance.calculateMonsterStats();
        if (enemy.tag == "Boss")
        {
            enemy.GetComponent<enemyHealth>().enemyMaxHealth = SaveManager.Instance.monsterHP * 10;
            enemy.GetComponent<enemyDamage>().damage = SaveManager.Instance.monsterDPS * 5;
        }
        else
        {
            enemy.GetComponent<enemyHealth>().enemyMaxHealth = SaveManager.Instance.monsterHP;
            enemy.GetComponent<enemyDamage>().damage = SaveManager.Instance.monsterDPS;
        }
    }

    void win()
    {
        //pause game
        ButtonShop.togglePause();
        FloatingTextController.pause = true;
        scoreScreen.SetActive(true);
        GUIText[] allText = scoreScreen.GetComponentsInChildren<GUIText>();
        //Find the Text to change to correct amount
        foreach(GUIText i in allText)
        {
            if(i.text == "XXX Gold")
            {
                float goldEarned = SaveManager.Instance.goldEarned;
                i.text = goldEarned.ToString(".##") + " Gold";
                break;
            }
        }
    }
    
    //Called in Button Boss
    public static void spawnBoss()
    {
        if (bossSpawned)
            return;
        bossSpawned = true;

        foreach (GameObject i in allEnemies)
        {
            if (i == null|| i.tag == "Boss")
                continue;
            enemyHealth tempHealth = i.GetComponent<enemyHealth>();
            tempHealth.addDamage(tempHealth.currentHealth);
        }

        int index = 0;
        switch (currPlayer.scene.name)
        {
            case ("Fight scene"):
                break;
            case ("Fight scene 1"):
                index = 1;
                break;
            case ("Fight scene 2"):
                index = 2;
                break;
        }
        currEnemy = Instantiate(SaveManager.Instance.enemyBoss[index], (Vector2)currPlayer.transform.position + new Vector2(10f, 1f), currPlayer.transform.rotation);
        setEnemyStats(currEnemy);
    }
    
}
