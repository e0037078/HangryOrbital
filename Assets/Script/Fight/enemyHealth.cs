using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour {

    public float enemyMaxHealth;
    //public GameObject enemyDeathFX;
    public Slider enemySlider;

    //public bool drops;
    //public GameObject theDrop;

    //public AudioClip deathKnell;

    float currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = enemyMaxHealth;
        enemySlider.maxValue = currentHealth;
        enemySlider.value = currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            addDamage(1f);
        }
    }
    public void addDamage(float damage)
    {
        enemySlider.gameObject.SetActive(true);
        currentHealth -= damage;

        enemySlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            makeDead();
        }
    }
    void makeDead()
    {
        //make sound
        //death animation
        AutoMove.playerContact = false;
        Destroy(gameObject.transform.parent.gameObject);
       /* AudioSource.PlayClipAtPoint(deathKnell, transform.position);
        Instantiate(enemyDeathFX, transform.position, transform.rotation);
        if (drops)
        {
            Instantiate(theDrop,transform.position,transform.rotation);
        }
        */
    }
    
    

}
