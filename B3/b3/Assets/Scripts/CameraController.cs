using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject person;
    public Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - person.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = person.transform.position + offset;
    }
}
