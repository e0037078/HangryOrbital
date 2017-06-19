using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {

    public Transform[] allEnemies = new Transform [2];
    public Transform[] allPlayers = new Transform[1];
    public float[] playerDamage = new float[5];

    public static Transform currEnemy = null;
    public Transform currPlayer;

    float nextDamage;

	// Use this for initialization
	void Start () {
        nextDamage = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate () {
        // player's attacks onto monster
        currEnemy = GetClosestEnemy(currPlayer);
        dmgEnemy();
        

        // enemy's attacks onto player  
        currPlayer = GetClosestPlayer(currEnemy);
        if (currPlayer == null) // no more players
        {
            // ko
        }
        else // attacks only affect currPlayer and not the rest
        {
            float enemyDamage = getTotalEnemyDamage();
            currPlayer.gameObject.GetComponent<playerHealth>().addDamage(enemyDamage);
        }
       
	}

    float getTotalPlayerDamage()
    {
        float sum = 0.0f;
        foreach(float f in playerDamage)
        {
            sum += f;
        }
        return sum;
    }

    float getTotalEnemyDamage()
    {
        float sum = 0.0f;
        foreach (Transform t in allEnemies)
        {
            if (t == null)
                continue;
            sum += t.gameObject.GetComponent<enemyDamage>().damage;
        }
        return sum;
    }

    Transform GetClosestEnemy(Transform currPlayer)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = currPlayer.position;
        foreach (Transform potentialTarget in allEnemies)
        {
            if (potentialTarget == null)
            {
                continue;
            }
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
             
            }
        }
        
        return bestTarget;
    }

    Transform GetClosestPlayer(Transform currEnemy)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = currEnemy.position;
        foreach (Transform potentialTarget in allPlayers)
        {
            if (potentialTarget == null)
            {
                continue;
            }
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    void dmgEnemy()
    {
        if (currEnemy == null) // no more enemies
        {
            // yay end
            Debug.Log("enemy null");
        }
        else // attacks only affect currEnemy and not the rest
        {
            if(Time.time > nextDamage)
            {
                float playerDamage = getTotalPlayerDamage();
                currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(playerDamage);
                nextDamage = Time.time + 1; //Damage every second
            }
        }
    }
}
