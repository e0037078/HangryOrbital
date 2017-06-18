using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {

    public Transform[] allEnemies = new Transform [5];
    public Transform[] allPlayers = new Transform[5];
    public float[] playerDamage = new float[5];

    Transform currEnemy = null;
    Transform currPlayer = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // player's attacks onto monster
        currEnemy = GetClosestEnemy();
        if (currEnemy == null) // no more enemies
        {
            // yay end
        }
        else // attacks only affect currEnemy and not the rest
        {
            float playerDamage = getTotalPlayerDamage();
            currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(playerDamage);
        }

        // enemy's attacks onto player
        currPlayer = GetClosestPlayer();
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
            sum += t.gameObject.GetComponent<enemyDamage>().damage;
        }
        return sum;
    }

    Transform GetClosestEnemy()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in allEnemies)
        {
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

    Transform GetClosestPlayer()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in allPlayers)
        {
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
}
