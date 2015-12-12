using UnityEngine;
using System.Collections;

public class takegem : MonoBehaviour {

    public GameObject gem;

    private bool isInteraction = false;
    private bool isUserInteraction = true;


    void OnInteractionEnd(Transform t)
    {
        isInteraction = true;
        isUserInteraction = false;
    }

    // Use this for initialization
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isInteraction == true)
        {
            gem.SetActive(false);
        }

    }
}
