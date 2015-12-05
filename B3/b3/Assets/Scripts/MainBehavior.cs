using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class MainBehavior : MonoBehaviour {

    public Transform GoTo;
    public Transform GoTo2;
    public Transform GoTo3;
    public Transform GoTo4;
    public Transform GoTo5;

    public GameObject participant;

    BehaviorAgent behaviorA;

	// Use this for initialization
	void Start () {
        behaviorA = new BehaviorAgent(this.tree());
        BehaviorManager.Instance.Register(behaviorA);
        behaviorA.StartBehavior();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    protected Node ST_GoToHere(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(target.position), new LeafWait(500));
    }



    protected Node tree()
    {
        return new DecoratorLoop(
            new SequenceShuffle(
                    this.ST_GoToHere(GoTo),
                    this.ST_GoToHere(GoTo2),
                    this.ST_GoToHere(GoTo3),
                    this.ST_GoToHere(GoTo4),
                    this.ST_GoToHere(GoTo5)));
    }
}
