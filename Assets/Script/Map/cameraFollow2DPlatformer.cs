using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow2DPlatformer : MonoBehaviour {

    public Transform target; //what the camera is following;
    public float smoothing; //dampening effect

    Vector3 offset;//defference between the camera and the target

    float lowY; //lowest point camera goes
    float left; // camera should not move beyond this point
    float right;

	// Use this for initialization
	void Start () {
        offset = transform.position - target.position ;

        lowY = transform.position.y-10;
        left = -2f;
        right = 7f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetCamPos = target.position + offset;
        
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        if (transform.position.y < lowY)
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        if (transform.position.x < left)
            transform.position = new Vector3(left, transform.position.y, transform.position.z);
        if (transform.position.x > right)
            transform.position = new Vector3(right, transform.position.y, transform.position.z);

    }
}
