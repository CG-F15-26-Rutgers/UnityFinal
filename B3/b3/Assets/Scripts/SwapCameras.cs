using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class SwapCameras : MonoBehaviour
{

	void Start(){
		camSwap(1);
	}

    void Update()
    {
        if (Input.GetKey("1"))
        {
            camSwap(1);
        }
        else if (Input.GetKey("2"))
        {
            camSwap(2);
        }
        else if (Input.GetKey("3"))
        {
            camSwap(3);
        }
        else if (Input.GetKey("4"))
        {
            camSwap(4);
        }
        else if (Input.GetKey("5"))
        {
            camSwap(5);
        }
        else if (Input.GetKey("6"))
        {
            camSwap(6);
        }
        else if (Input.GetKey("7"))
        {
            camSwap(7);
        }
		else if (Input.GetKey("8"))
        {
            camSwap(8);
        }
   
    }

    void camSwap(int currentCam)
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
		GameObject[] daniels = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject cams in cameras)
        {
            Camera theCam = cams.GetComponent<Camera>() as Camera;
            theCam.enabled = false;
        }

		foreach (GameObject dan in daniels){
			dan.GetComponent<FirstPersonController>().Disable();
		}

        string cameraToUse = "Camera" + currentCam;
		string danToUse = "Daniel" + currentCam;
        Camera usedCam = GameObject.Find(cameraToUse).GetComponent<Camera>() as Camera;
        usedCam.enabled = true;

		

		if (currentCam == 1)
			GameObject.Find("Hero").GetComponent<FirstPersonController>().Enable();
		else if (currentCam == 2)
			GameObject.Find("Princess").GetComponent<FirstPersonController>().Enable();
		else if (currentCam == 3)
			GameObject.Find("OldMan").GetComponent<FirstPersonController>().Enable();
		else
			GameObject.Find(danToUse).GetComponent<FirstPersonController>().Enable();
    }
}
