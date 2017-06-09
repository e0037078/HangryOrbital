using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : TouchManager {
    // drag script to each button, then select accordingly
    public enum type {LeftButton, RightButton, JumpButton};
    public type buttonType;

    public float jumpHeight = 0.0f; //
    public float moveSpeed = 0.0f; //

    public GameObject playerObject = null;
    Rigidbody2D playerRigidbody;
    Animator playerAnim;
    static bool facingRight;
    public static bool isJumping;
    
    public GUITexture buttonTexture = null;
    
	// Use this for initialization
	void Start ()
    {
        playerRigidbody = playerObject.GetComponent<Rigidbody2D>();
        playerAnim = playerObject.GetComponent<Animator>();
        facingRight = true;
        isJumping = false;
    }
	
	// Update is called once per frame
	void Update () {
        touchInput(buttonTexture);
	}

    void OnFirstTouchBegan() // only for one frame
    {
        switch (buttonType)
        {
            case type.JumpButton:
                if (!isJumping)
                {
                    playerRigidbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                    playerAnim.SetInteger("State", 2);
                    isJumping = true;
                }
                break;
        }
    }

    void OnSecondTouchBegan() // only for one frame
    {
        switch (buttonType)
        {
            case type.JumpButton:
                if (!isJumping)
                {
                    playerRigidbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                    playerAnim.SetInteger("State", 2);
                    isJumping = true;
                }
                break;
        }
    }

    void OnFirstTouch() //  for subsequent frames
    { 
        switch (buttonType)
        {
            case type.LeftButton:
                if (facingRight)
                {
                    Flip();
                }
                if (!isJumping)
                {
                    playerAnim.SetInteger("State", 1);
                }
                playerObject.transform.Translate(-Vector2.right * moveSpeed * Time.deltaTime);
                break;
            case type.RightButton:
                if (!facingRight)
                {
                    Flip();
                }
                if (!isJumping)
                {
                    playerAnim.SetInteger("State", 1);
                }
                playerObject.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                break;
        }
    }

    void OnSecondTouch() // for subsequent frames
    {
        switch (buttonType)
        {
            case type.LeftButton:
                if (facingRight)
                {
                    Flip();
                }
                if (!isJumping)
                {
                    playerAnim.SetInteger("State", 1);
                }
                playerObject.transform.Translate(-Vector2.right * moveSpeed * Time.deltaTime);
                break;
            case type.RightButton:
                if (!facingRight)
                {
                    Flip();
                }
                if (!isJumping)
                {
                    playerAnim.SetInteger("State", 1);
                }
                playerObject.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                break;
        }
    }

    void OnFirstTouchEnded()
    {
       /* switch (buttonType)
        {
            case type.JumpButton:
                isJumping = false;
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
                break;
        }
        */

    }

    void OnSecondTouchEnded()
    {
        /*switch (buttonType)
        {
            case type.JumpButton:
                isJumping = false;
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
                break;
        }
        */
    }

    void Flip()
    {
            facingRight = !facingRight;
            Vector3 temp = playerObject.transform.localScale;
            temp.x *= -1;
            playerObject.transform.localScale = temp;
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GROUND")
        {
            isJumping = false;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerAnim.SetInteger("State", 0);
        }
    }
    */
}
