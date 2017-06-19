using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {

    static FightManager Instance;

    public GameObject[] allEnemies;
    public GameObject[] allPlayers ;

    public static GameObject currEnemy = null;
    public static GameObject currPlayer = null;

    float playerDamage;
    float nextDamage;

    //Enemy Prefab to spawn
    public GameObject enemy;
    public GameObject enemyBoss;
    public static bool spawnBoss = false;

    public int killsToActivateBoss;
    public GameObject bossButton;

    // Use this for initialization
    void Start () {
        ensureSingleton();
        allEnemies = GameObject.FindGameObjectsWithTag("Monster");
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        nextDamage = Time.time;

        //Initialisation to random
        currEnemy = allEnemies[0];
        currPlayer = allPlayers[0];

        currEnemy = GetClosestEnemy(currPlayer);
        currPlayer = GetClosestPlayer(currEnemy);


        playerDamage = SaveManager.Instance.DPS;
    }

    // Update is called once per frame
    void Update () {
        //Ensures dmg once per sec
        if (Time.time > nextDamage)
        {

            // player's attacks onto monster
            dmgEnemy();


            // enemy's attacks onto player  

            dmgPlayer();
        }
        if(currEnemy == null)
        {
            currEnemy = GetClosestEnemy(currPlayer);
            if(currEnemy == null)
            {
                //if all enemies are dead, spawn more
                //SpawnManager.spawnEnemy(); 
                spawnEnemy();
                currEnemy = GetClosestEnemy(currPlayer);

            }
        }
        if (currPlayer == null)
        {
            currPlayer = GetClosestPlayer(currEnemy);
        }

        if (enemyHealth.deathCounter >= killsToActivateBoss)
        {
            bossButton.SetActive(true);
        }

        if (spawnBoss)
        {
            foreach (GameObject i in allEnemies)
            {
                enemyHealth tempHealth = i.GetComponent<enemyHealth>();
                tempHealth.addDamage(tempHealth.currentHealth);
            }
            currEnemy = Instantiate(enemyBoss, (Vector2)currPlayer.transform.position + new Vector2(20f, 1f), currPlayer.transform.rotation);
            spawnBoss = false;
        }

        
    }

    float getTotalEnemyDamage()
    {
        float sum = 0.0f;
        foreach (GameObject t in allEnemies)
        {
            if (t == null)
                continue;
            sum += t.GetComponent<enemyDamage>().damage;
        }
        return sum;
    }


    GameObject GetClosestEnemy(GameObject currPlayer)
    {
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
            currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(playerDamage);
            nextDamage = Time.time + 1; //Damage every second
        }
    }

    void dmgPlayer()
    {
        if (currPlayer == null) // no more enemies
        {
            // yay end
            Debug.Log("player null");
        }
        else // attacks only affect currPlayer and not the rest
        {
            float enemyDamage = getTotalEnemyDamage();
            currPlayer.gameObject.GetComponent<playerHealth>().addDamage(enemyDamage);
            nextDamage = Time.time + 1; //Damage every second
        
        }
    }

    void ensureSingleton()
    {
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
    }

    void spawnEnemy()
    {
        Instantiate(enemy, (Vector2)currPlayer.transform.position + new Vector2(20f, 1f), currPlayer.transform.rotation);
        Instantiate(enemy, (Vector2)currPlayer.transform.position + new Vector2(18f, 1f), currPlayer.transform.rotation);

        allEnemies = GameObject.FindGameObjectsWithTag("Monster");

    }
}
