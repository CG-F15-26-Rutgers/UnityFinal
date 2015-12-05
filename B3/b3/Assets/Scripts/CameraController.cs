using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject person;
    Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = person.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        float angle = person.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, angle, 0);

        transform.position = person.transform.position - (rotation * offset);
        transform.LookAt(person.transform);
	}
}
