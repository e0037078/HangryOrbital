using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour {

    BoxCollider2D col;
    Transform trans;

    GameObject player;
    Transform playerTrans;
    BoxCollider2D playerCol;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        col = gameObject.GetComponent<BoxCollider2D>();
        trans = gameObject.GetComponent<Transform>();

        playerTrans = player.GetComponent<Transform>();
        playerCol = player.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

        //TODO
        /*
		if(playerTrans.position.y+playerCol.offset.y-1f  < trans.position.y )
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
        */
    }
}
