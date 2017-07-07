 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour {

    public float damage;
    public float damageRate;
    public float pushBackForce;
    public bool isDead = false;

    public bool contact;
    float nextDamage;

	// Use this for initialization
	void Start () {
        damage = SaveManager.Instance.monsterDPS;
        nextDamage = Time.time;//or Time.time
        contact = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!isDead && other.tag == "Player" && nextDamage<Time.time)
        {
            contact = true;

            //playerHealth thePlayerHealth = FightManager.currPlayer.GetComponent<playerHealth>();
            //thePlayerHealth.addDamage(damage);
            dmgPlayer();
            nextDamage = Time.time + damageRate;


            pushback(other.transform);
        }
    }


    void dmgPlayer()
    {
        if (FightManager.currPlayer == null) // no more players
        {
            // yay end
            Debug.Log("player null");
        }
        else // attacks only affect currPlayer and not the rest
        {
            FightManager.currPlayer.gameObject.GetComponent<playerHealth>().addDamage(damage);
            nextDamage = Time.time + 1; //Damage every second

        }
    }


    // make player fly
    void pushback(Transform pushedObject)
    {
        //TODO fly top left instead of up
        /*
        Vector2 pushDirection = new Vector2(0, (pushedObject.position.y - transform.position.y)).normalized;
        pushDirection *= pushBackForce;
        Rigidbody2D pushRB = pushedObject.gameObject.GetComponent<Rigidbody2D>();
        pushRB.velocity = Vector2.zero;
        pushRB.AddForce(pushDirection, ForceMode2D.Impulse);
        */
    }
}
