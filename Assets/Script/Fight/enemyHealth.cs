using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enemyHealth : MonoBehaviour {

    public float enemyMaxHealth;
    //public GameObject enemyDeathFX;
    public Slider enemySlider;

    //public bool drops;
    //public GameObject theDrop;

    //public AudioClip deathKnell;

    public static int deathCounter = 0;

    public float currentHealth;

    bool isDead = false;

    // Use this for initialization
    void Start ()
    {
        enemyMaxHealth = SaveManager.Instance.monsterHP;
        currentHealth = enemyMaxHealth;
        enemySlider.maxValue = currentHealth;
        enemySlider.value = currentHealth;

        deathCounter = 0;

    }

    // Update is called once per frame
    void Update () {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetMouseButtonUp(0))
        {
           // addDamage(1f);
        }
    }
    public void addDamage(float damage)
    {
        enemySlider.gameObject.SetActive(true);
        currentHealth -= damage;

        enemySlider.value = currentHealth;

        if (!isDead && currentHealth <= 0)
        {
            makeDead();
        }
    }

    void makeDead()
    {
        //make sound
        isDead = true;
        if (gameObject.scene.name == "Fight scene")
            SfxManager.PlaySound("EnemyDie2");
        else
            SfxManager.PlaySound("EnemyDie");
        Destroy(this.GetComponent<AutoMove>());
        this.GetComponent<enemyDamage>().isDead = true;
        this.GetComponent<Animator>().SetBool("isDead", true); //death animation
        this.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(5f, 3.75f), this.GetComponent<Transform>().position, ForceMode2D.Impulse);

        deathCounter++;
        AutoMove.playerContact = false;
        SaveManager.Instance.addGold();

        if(gameObject.tag == "Boss")
        {
            SaveManager.Instance.monsterToClear--;
            SaveManager.Instance.wonLevel();
            MusicManager.StopBGM();
            SfxManager.PlaySound("PlayerWin");
            StartCoroutine(playAfterTime(1f));
            FightManager.winMap = true;
        }
        //Length of animation
        StartCoroutine(destroyAfterTime(0.9f, gameObject));

        /* AudioSource.PlayClipAtPoint(deathKnell, transform.position);
        Instantiate(enemyDeathFX, transform.position, transform.rotation);
        if (drops)
        {
            Instantiate(theDrop,transform.position,transform.rotation);
        }
        */
    }

    IEnumerator destroyAfterTime(float waitTime, GameObject temp)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Destroy(temp);
    }

    IEnumerator playAfterTime(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        MusicManager.PlayBGM("BGM2");
    }
}
