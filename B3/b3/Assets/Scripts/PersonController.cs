using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class PersonController : MonoBehaviour
{
    //public Animation Walk;

    private bool nonInteractive = true;
    private float moveSpeed = 5.0f;
    private float rotateSpeed = 3.0f;

    void Start()
    {
       //Walk = GetComponent<AnimationClip>();
    }


    void Update()
    {
        if(nonInteractive)
        {

        }
        else
        {
            CharacterController control = GetComponent<CharacterController>();
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float currentSpeed = moveSpeed * Input.GetAxis("Vertical");

            control.SimpleMove(forward * currentSpeed);
            if (currentSpeed >= 5)
            {
                //Walk.CrossFade("Walk");
            }
            else
            {
                // Walk.CrossFade("Idle");
            }
        }
        

    }

}

