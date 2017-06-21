using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureDamage : MonoBehaviour {

    public Transform slash;
    public Transform lightning;

    Animator slashAnim;
    Animator lightningAnim;
	// Use this for initialization
	void Start () {
		
        slashAnim = slash.GetComponent<Animator>();
        lightningAnim = lightning.GetComponent<Animator>();  
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = 0; i < SaveManager.Instance.gestureProb.Length; i++)
        {
            if (Random.value <= SaveManager.calculateFixedUpdateProbability(SaveManager.Instance.gestureProb[i]))
            {
                switch (i)
                {
                    case 1:
                        damage("line", 1);
                        Debug.Log("Prob Line");
                        break;
                    case 2:
                        damage("forward slash", 1);
                        Debug.Log("Prob forward");
                        break;
                    case 3:
                        damage("back slash", 1);
                        Debug.Log("Prob back");
                        break;
                    case 4:
                        damage("Lightning", 1);
                        Debug.Log("Prob Lightning");
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
					lightning.position = FightManager.currPlayer.transform.position;
                    lightningAnim.SetTrigger("On");                        
                    //Temp formula 
                    float damage =(float) SaveManager.Instance.gestureDMG[3];
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;

            case "forward slash":
                if (FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
					slash.position = FightManager.currPlayer.transform.position;
                    slashAnim.SetTrigger("On");
                    //Temp formula 
                    float damage = (float)SaveManager.Instance.gestureDMG[2];
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;

            case "back slash":
                if (FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    //Temp formula 
                    float damage = (float)SaveManager.Instance.gestureDMG[1];
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;

            case "line":
                if (FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    //Temp formula 
                    float damage = (float)SaveManager.Instance.gestureDMG[0];
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;
            
            
        }
    }
}
