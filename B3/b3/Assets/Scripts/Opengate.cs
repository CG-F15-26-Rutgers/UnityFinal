using UnityEngine;
using System.Collections;

public class Opengate : MonoBehaviour {

    private bool isInteraction = false;
    private bool isUserInteraction = true;
    private int time = 5;
    private bool open = false;

    void OnInteractionEnd(Transform t)
    {
        isInteraction = true;
        isUserInteraction = false;
        time = 200;
    }

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0 && isInteraction == true)
        {
            transform.Translate(new Vector3(0, 0, 3) * Time.deltaTime);
            time--;
        }

    }
}
