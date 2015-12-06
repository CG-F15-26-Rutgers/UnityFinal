using UnityEngine;
using System.Collections;

public class WhichCamera : MonoBehaviour {

    private bool IsInteractive;
    public Camera InteractiveCam;
    public Camera NonInterCam;

	// Use this for initialization
	void Start () {
        IsInteractive = false;
        InteractiveCam.enabled = false;
        NonInterCam.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(IsInteractive == true)
        {
            NonInterCam.enabled = false;
            InteractiveCam.enabled = true;
        }
    
	}
}
