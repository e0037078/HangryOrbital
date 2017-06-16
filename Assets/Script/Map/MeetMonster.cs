using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetMonster : MonoBehaviour {

    public float chanceMob;


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Random.Range(0, 10) >= chanceMob)
        {
            changeScene();
        }    
    }

    void changeScene()
    {
        //TODO changing of scene , need ensure that mobs spawn according to what is encoutnered
        //Perhaps can use level in savemanager to determine

        //Application.LoadLevel("Fight Scene");
         
    }
}
