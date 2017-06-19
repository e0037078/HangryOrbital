using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void damage(string gestureClass, float gestureScore)
    {
        switch (gestureClass)
        {
            case "Lightning":
                if (FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(5f);
                Debug.Log(gestureClass + " " + gestureScore);
                break;
            case "line":
                if (FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(5f);
                Debug.Log(gestureClass + " " + gestureScore);
                break;
            case "forward slash":
                if (FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(5f);
                Debug.Log(gestureClass + " " + gestureScore);
                break;
            
        }
    }
}
