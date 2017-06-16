using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public float fullHealth;
    //public GameObject deathFX;
    //public AudioClip playerHurt;

    float currentHealth;

    //public AudioClip playerDeathSound;
    //AudioSource playerAS;
    
    //HUD Variables
    public Slider healthSlider;


    bool damaged = false;

	// Use this for initialization
	void Start () {
        currentHealth = fullHealth;
        
        //HUD initialisation
        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;

        //playerAS = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void addDamage(float damage)
    {
        if (damage <= 0) return;

        healthSlider.gameObject.SetActive(true);
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        damaged = true;

        //playerAS.clip = playerHurt;
        //playerAS.Play();

 //       playerAS.PlayOneShot(playerHurt); //same

        if (currentHealth <= 0)
        {
                makeDead();
        }
    }

    public void addHealth(float heathAmount)
    {
     
        currentHealth += heathAmount;
        if (currentHealth > fullHealth)
            currentHealth = fullHealth;
        healthSlider.value = currentHealth;

    }

    public void makeDead()
    {
        if (currentHealth > 0)
        {
            currentHealth -= currentHealth;
            healthSlider.value = currentHealth;
        }
        /*
        Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(playerDeathSound, transform.position);
        damageScreen.color = damagedColor;

        Animator gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        gameOverAnimator.SetTrigger("gameOver");
        theGameManager.restartTheGame();
        */
    }

    public void winGame()
    {
        /*
        Destroy(gameObject);
        theGameManager.restartTheGame();
        Animator winGameAnimator = winGameScreen.GetComponent<Animator>();
        winGameAnimator.SetTrigger("gameOver");
        */
    }
}
