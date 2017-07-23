using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float speedX;
    public float jumpSpeedY;

    bool facingRight = true;
    float speed;

    Animator anim;
    Rigidbody2D rb;

    public Animator fadeAnim;
    public Image black;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {

        // move right, stop
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed = -speedX;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            speed = 0;
        }

        // move left, stop
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed = speedX;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ButtonMovement.isJumping = true;
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
            anim.SetInteger("State", 2);
        }

        MovePlayer(speed); // player movement

        Flip();
    }

    // code for player movement and animation
    void MovePlayer(float playerSpeed)
    {
        if (!ButtonMovement.isJumping && (playerSpeed < 0 || playerSpeed > 0))
        {
            anim.SetInteger("State", 1);
        }
        if (!ButtonMovement.isJumping && playerSpeed == 0)
        {
            anim.SetInteger("State", 0);
        }
        rb.velocity = new Vector3(playerSpeed, rb.velocity.y, 0);

    }
    
    void Flip()
    {
        if (speed>0 && !facingRight || speed<0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }

    // for jump
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "GROUND"&& rb.velocity.y == 0) 
        {
            ButtonMovement.isJumping = false;  // landed
            speed = 0;
            anim.SetInteger("State", 1);
        }
        else if (collision.gameObject.tag == "City Monster")
        {
            // Check what level monster encountered.
            MonsterManager.Instance.encounteredMonsterLevel(collision.gameObject);
            // should add some animation like pokemon hahaha
            StartCoroutine(FadingIntoFightMap());
        }
        // in future, can add on collision with PORTAL --> change map scene
        else if (collision.gameObject.tag == "Portal")
        {
            // TODO load new scene
            // or load same level, diff layout (change tempLayout in level generator)
            // PortalManager script in portal prefab

        }
    }

    IEnumerator FadingIntoFightMap()
    {
        fadeAnim.SetBool("FadeOut", true);
        SaveManager.Instance.savePlayerPos();
        ButtonShop.togglePause();
        yield return new WaitUntil(() => black.color.a == 1);
        if (gameObject.scene.name == "City map")
            SceneManager.LoadScene("Fight scene");
        else if (gameObject.scene.name == "Forest map")
            SceneManager.LoadScene("Fight scene 1");
        else if (gameObject.scene.name == "Snow map")
            SceneManager.LoadScene("Fight scene 2"); 
    }
}
