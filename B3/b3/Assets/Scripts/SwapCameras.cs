using UnityEngine;
using System.Collections;

public class SwapCameras : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKey("1"))
        {
            camSwap(1);
        }
        if (Input.GetKey("2"))
        {
            camSwap(2);
        }
        if (Input.GetKey("3"))
        {
            camSwap(3);
        }
        if (Input.GetKey("4"))
        {
            camSwap(4);
        }
        if (Input.GetKey("5"))
        {
            camSwap(5);
        }
        if (Input.GetKey("6"))
        {
            camSwap(6);
        }
        if (Input.GetKey("7"))
        {
            camSwap(7);
        }
        if (Input.GetKey("8"))
        {
            camSwap(8);
        }
   
    }

    void camSwap(int currentCam)
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");

        foreach (GameObject cams in cameras)
        {
            Camera theCam = cams.GetComponent<Camera>() as Camera;
            theCam.enabled = false;
        }

        string oneToUse = "Camera" + currentCam;
        Camera usedCam = GameObject.Find(oneToUse).GetComponent<Camera>() as Camera;
        usedCam.enabled = true;
    }
}