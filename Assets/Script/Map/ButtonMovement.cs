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
    Rigidbody2D playerRB;
    Animator playerAnim;
    static bool facingRight;
    public static bool isJumping;

    public GUITexture buttonTexture = null;
    
	// Use this for initialization
	void Start ()
    {
        playerRB = playerObject.GetComponent<Rigidbody2D>();
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
                    Jump();
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
                    Jump();
                }
                break;

            
        }
    }

    void OnFirstTouch() //  for subsequent frames
    { 
        switch (buttonType)
        {
            case type.LeftButton:
                Left();
                break;
            case type.RightButton:
                Right();
                break;
        }
    }

    void OnSecondTouch() // for subsequent frames
    {
        switch (buttonType)
        {
            case type.LeftButton:
                Left();
                break;
            case type.RightButton:
                Right();
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

    void Left()
    {
        SfxManager.PlaySound("Walking");
        if (facingRight)
        {
            Flip();
        }
        if (!isJumping)
        {
            playerAnim.SetInteger("State", 1);
        }
        playerRB.velocity = new Vector2(-moveSpeed, playerRB.velocity.y);
        //playerObject.transform.Translate(-Vector2.right * moveSpeed * Time.deltaTime);
    }

    void Right()
    {
        SfxManager.PlaySound("Walking");
        if (!facingRight)
        {
            Flip();
        }
        if (!isJumping)
        {
            playerAnim.SetInteger("State", 1);
        }
        playerRB.velocity = new Vector2(moveSpeed, playerRB.velocity.y);
        //playerObject.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
    
    void Jump()
    {
        SfxManager.PlaySound("Jumping");
        playerRB.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        playerAnim.SetInteger("State", 2);
        isJumping = true;
        
    }
    void Flip()
    {
            facingRight = !facingRight;
            Vector3 temp = playerObject.transform.localScale;
            temp.x *= -1;
            playerObject.transform.localScale = temp;
    }

}
