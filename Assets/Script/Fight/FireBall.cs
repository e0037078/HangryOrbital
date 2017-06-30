using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster" || collision.tag == "Boss")
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            SfxManager.PlaySound("FireBallHit");
            this.GetComponent<Animator>().SetTrigger("Hit");
            //duration of animation
            StartCoroutine(destroyAfterTime(0.77f)); 
        }
    }
    IEnumerator destroyAfterTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
