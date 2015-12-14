using UnityEngine;
using System.Collections;

public class AnimThing : MonoBehaviour {

    Animator anim;
    NavMeshAgent agent;
    bool traversingLink;
    OffMeshLinkData currLink;
    int jumpHash = Animator.StringToHash("Jump");

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.autoTraverseOffMeshLink = false;
    }

    void Update()
    {
        if (agent.isOnOffMeshLink)
        {
            if (!traversingLink)
            {
                currLink = agent.currentOffMeshLinkData;

                anim.SetTrigger(jumpHash);
                traversingLink = true;
            }
            else if (!anim.IsInTransition(0))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).tagHash != jumpHash)
                {
                    agent.CompleteOffMeshLink();
                    agent.Resume();
                    agent.nextPosition = currLink.endPos;
                    traversingLink = false;
                }
            }
        }
        else
        {
            anim.SetFloat("Idle", agent.velocity.magnitude);
        }
    }

    void OnAnimatorMove()
    {
        if (traversingLink)
        {
            if (!anim.IsInTransition(0))
            {
                float tlerp = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                Vector3 newPos = Vector3.Lerp(currLink.startPos, currLink.endPos, tlerp);
                transform.position = newPos;
            }
        }
        else if (!agent.isOnOffMeshLink)
        {
            transform.position = agent.nextPosition;
        }
    }
}
