using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach This to the VRCamera on SteamVR Player
//Make sure all objects that you want to gaze at are on WermholeObject Layer. Peace
public class GazeTrigger : MonoBehaviour {

    public float DurationToTrigger = 1f;
    private float currGazeDuration = 0f;
    private int layerMask = 1 << 8; //only raycast check on WermholeObjects
    private GameObject currObject;
    private bool isFocused = false;

    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if(currGazeDuration > DurationToTrigger && !isFocused) //we looked at object long enough to trigger
        {
            currGazeDuration = 0f;
            currObject.GetComponent<Valve.VR.InteractionSystem.WermholeObject>().onGazeTrigger.Invoke();

            isFocused = true;
        }

        //Debug.DrawRay(transform.position, fwd*10f, Color.green);

        if (Physics.Raycast(transform.position, fwd, out hit, 10f, layerMask))
        {
            if(!GameObject.ReferenceEquals(currObject, hit.collider.gameObject)) //new object we are looking at
            {
                currGazeDuration = 0f;
                currObject = hit.collider.gameObject;
            }
            else if (!isFocused) //continue gazing at the object
            {
                currGazeDuration += Time.deltaTime;
            }

        }else //looked away
        {
            currGazeDuration = 0f;
            if (currObject) //remove current object
            {
                currObject.GetComponent<Valve.VR.InteractionSystem.WermholeObject>().onGazeExit.Invoke();
                currObject = null;
                isFocused = false;
            }
        }
            
    }
}
