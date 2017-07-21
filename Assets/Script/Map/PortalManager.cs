using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour {
    public static bool passed = false;
    public static bool unlocked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (gameObject.scene.name)
            {
                case ("City map"):
                    SceneManager.LoadScene("Forest map");
                    passed = true;
                    break;
                case ("Forest map"):
                    // SceneManager.LoadScene("Forest map"); // should be one more level
                    break;
            }
        }
    }
}
