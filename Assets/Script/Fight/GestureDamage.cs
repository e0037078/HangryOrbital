﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = 0; i < SaveManager.Instance.gestureProb.Length; i++)
        {
            if (Random.value >= SaveManager.calculateFixedUpdateProbability(SaveManager.Instance.gestureProb[i]))
            {
                switch (i)
                {
                    case 1:
                        damage("line", 1);
                        break;
                    case 2:
                        damage("forward slash", 1);
                        break;
                    case 3:
                        damage("back slash", 1);
                        break;
                    case 4:
                        damage("Lightning", 1);
                        break;
                }
            }
        }

    }

    public static void damage(string gestureClass, float gestureScore)
    {
        switch (gestureClass)
        {
            case "Lightning":
                if (FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    //Temp formula 
                    float damage =(float) (10 + SaveManager.Instance.upgrades[4] * 1.8);
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;

            case "forward slash":
                if (FightManager.currEnemy!=null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    //Temp formula 
                    float damage = (float)(8 + SaveManager.Instance.upgrades[3] * 1.6);
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;

            case "line":
                if (FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    //Temp formula 
                    float damage = (float)(6 + SaveManager.Instance.upgrades[2] * 1.6);
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;
            
            
        }
    }
}
