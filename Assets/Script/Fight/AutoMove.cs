using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {

    public float moveSpeed = 0.0f;

    Animator anim;
    Rigidbody2D RB;
    public static bool playerContact;
    public static bool damaged;
    enemyDamage enemyDMG;   

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        RB = gameObject.GetComponent<Rigidbody2D>();

        enemyDMG = gameObject.GetComponent<enemyDamage>();

        playerContact = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Monster" || gameObject.tag == "Boss")
        {
            if (enemyDMG.contact)
            {
                playerContact = true;
                anim.SetInteger("State", 0);
            }

            else
            {
                playerContact = false;
                anim.SetInteger("State", 1);
            }

        }
        else if (playerContact && gameObject.tag == "Player")
        { 
            anim.SetInteger("State", 0);

        }
        else
            anim.SetInteger("State", 1);

    }

    void FixedUpdate ()
    {
            if (gameObject.tag == "Player")
            {
                if (!playerContact)
                {
                
                    RB.velocity = new Vector2(moveSpeed, RB.velocity.y);
               }
            }
            else if (gameObject.tag == "Monster")
            {
                if (!enemyDMG.contact || !playerContact)
                {
                    RB.velocity = new Vector2(-moveSpeed, RB.velocity.y);

                }
            }
    }

}
