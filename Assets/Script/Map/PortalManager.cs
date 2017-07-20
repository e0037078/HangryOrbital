using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (gameObject.scene.name)
            {
                case ("City map"):
                    SceneManager.LoadScene("Forest map");
                    break;
                case ("Forest map"):
                    // SceneManager.LoadScene("Forest map"); // should be one more level
                    break;
            }
        }
    }
}
