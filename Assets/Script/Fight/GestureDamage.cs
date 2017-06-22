using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureDamage : MonoBehaviour {

    public GameObject slash;
    public GameObject lightning;


    Animator slashAnim;
    Animator lightningAnim;
	// Use this for initialization
	void Start () {

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

    public void damage(string gestureClass, float gestureScore)
    {
        switch (gestureClass)
        {
            case "Lightning":
                if (FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    GameObject tempLightning = Instantiate(lightning, FightManager.currPlayer.transform.position, Quaternion.identity);
                    float lightningLength = tempLightning.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                    float dist = -FightManager.currPlayer.transform.position.x + FightManager.currEnemy.transform.position.x;
                    float toScale = dist / lightningLength;
                    tempLightning.transform.localScale = new Vector3(toScale/2, tempLightning.transform.localScale.y, tempLightning.transform.localScale.z);
                    tempLightning.transform.position = FightManager.currPlayer.transform.position + new Vector3(lightningLength * tempLightning.transform.localScale.x, -1f, 0);

                    lightningAnim = tempLightning.GetComponent<Animator>();
                    lightningAnim.SetTrigger("On");
                    StartCoroutine(destroyAfterTime(0.5f, tempLightning));
                    //Temp formula 
                    float damage =(float) SaveManager.Instance.gestureDMG[3];
                    FightManager.currEnemy.gameObject.GetComponent<enemyHealth>().addDamage(damage);

                }
                Debug.Log(gestureClass + " " + gestureScore);
                break;

            case "forward slash":
                if (FightManager.currEnemy != null && FightManager.currEnemy.GetComponent<SpriteRenderer>().isVisible)
                {
                    GameObject tempSlash = Instantiate(slash, FightManager.currEnemy.transform.position, Quaternion.identity);

                    slashAnim = tempSlash.GetComponent<Animator>();
                    slashAnim.SetTrigger("On");
                    StartCoroutine(destroyAfterTime(0.5f, tempSlash));
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

                    GameObject tempSlash = Instantiate(slash, FightManager.currEnemy.transform.position, Quaternion.identity);
                    tempSlash.transform.localScale = new Vector3(-tempSlash.transform.localScale.x, tempSlash.transform.localScale.y, tempSlash.transform.localScale.z);

                    slashAnim = tempSlash.GetComponent<Animator>();
                    slashAnim.SetTrigger("On");
                    StartCoroutine(destroyAfterTime(0.5f, tempSlash));
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
    IEnumerator destroyAfterTime(float waitTime, GameObject temp)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Destroy(temp);
    }
}
