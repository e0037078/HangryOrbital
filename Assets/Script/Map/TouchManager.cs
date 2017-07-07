using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour
{
    public static bool guiTouch = false;
    bool mouseDown = false;
    
    private void OnMouseDown()
    {
        SendMessage("OnFirstTouchBegan", SendMessageOptions.DontRequireReceiver);
        SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
        mouseDown = true;
    }
    /* Somehow this don't work
    private void OnMouseDrag()
    {
        SendMessage("OnFirstTouchStayed", SendMessageOptions.DontRequireReceiver);
        SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
        guiTouch = true;
    }
    */
    //This called from child to check mouseDown
    public void checkMouseDown()
    {
        if (mouseDown)
        {
            SendMessage("OnFirstTouchStayed", SendMessageOptions.DontRequireReceiver);
            SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
        }
    }
    private void OnMouseUp()
    {
        SendMessage("OnFirstTouchEnded", SendMessageOptions.DontRequireReceiver);
        mouseDown = false;
    }
    public void touchInput(GUITexture texture)
    {
     

        if (Input.touchCount > 0)
        {
            if (texture.HitTest(Input.GetTouch(0).position))
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        //do stuff
                        // calling function
                        SendMessage("OnFirstTouchBegan", SendMessageOptions.DontRequireReceiver);
                        SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
                        guiTouch = true;
                        break;
                    case TouchPhase.Stationary:
                        //do stuff
                        SendMessage("OnFirstTouchStayed", SendMessageOptions.DontRequireReceiver);
                        SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
                        guiTouch = true;
                        break;
                    case TouchPhase.Moved:
                        //do stuff
                        SendMessage("OnFirstTouchMoved", SendMessageOptions.DontRequireReceiver);
                        SendMessage("OnFirstTouch", SendMessageOptions.DontRequireReceiver);
                        guiTouch = true;
                        break;
                    case TouchPhase.Ended:
                        //do stuff
                        SendMessage("OnFirstTouchEnded", SendMessageOptions.DontRequireReceiver);
                        guiTouch = false;
                        break;
                }
            }
        }
        if (Input.touchCount > 1)
        {
            if (texture.HitTest(Input.GetTouch(1).position))
            {
                switch (Input.GetTouch(1).phase)
                {
                    case TouchPhase.Began:
                        //do stuff
                        SendMessage("OnSecondTouchBegan", SendMessageOptions.DontRequireReceiver);
                        SendMessage("OnSecondTouch", SendMessageOptions.DontRequireReceiver);
                        break;
                    case TouchPhase.Stationary:
                        //do stuff
                        SendMessage("OnSecondTouchStayed", SendMessageOptions.DontRequireReceiver);
                        SendMessage("OnSecondTouch", SendMessageOptions.DontRequireReceiver);
                        break;
                    case TouchPhase.Moved:
                        //do stuff
                        SendMessage("OnSecondTouchMoved", SendMessageOptions.DontRequireReceiver);
                        SendMessage("OnSecondTouch", SendMessageOptions.DontRequireReceiver);
                        break;
                    case TouchPhase.Ended:
                        //do stuff
                        SendMessage("OnSecondTouchEnded", SendMessageOptions.DontRequireReceiver);
                        break;
                }
            }
        }
    }
}