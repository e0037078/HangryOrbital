using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public Transform generationPoint;

    public GameObject platformGreen;
    public GameObject platformStone;
    public float distBetweenGreen;
    public float distBetweenStone;

    float platformGreenHeight; // so that don't spawn a platform on top of each other
    float platformStoneHeight; // so that don't spawn a platform on top of each other
    float currentEnd;

	// Use this for initialization
	void Start () {
        platformGreenHeight = platformGreen.GetComponent<BoxCollider2D>().size.y;
        platformStoneHeight = platformStone.GetComponent<BoxCollider2D>().size.y;
        currentEnd = generationPoint.position.y + distBetweenStone;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < generationPoint.position.y && transform.position.y < currentEnd) // build green
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + platformGreenHeight + distBetweenGreen, 0);
            Instantiate(platformGreen, transform.position, transform.rotation);
        }
        else if (transform.position.y >= currentEnd) // build stone
        {
            Instantiate(platformStone, transform.position, transform.rotation);
            currentEnd += distBetweenStone;
            transform.position = new Vector3(transform.position.x, transform.position.y + distBetweenGreen - 0.7f, 0);
        }
        generationPoint.transform.position = new Vector3(transform.position.x, transform.position.y + platformGreenHeight + distBetweenGreen, 0);
	}
}
