using UnityEngine;
using System.Collections;
using TreeSharpPlus;
using UnityEngine.UI;



//FUCK MECANIM
//FUCK UNITY
//NOTHING WORKS
//NOTHING MAKES SENSE
//FUCK KADAPT
//ANIMATIONS NEVER RUN HOW THEYRE SUPPOSED TO


public class HeroTree : MonoBehaviour {

    public GameObject Hero;
    public GameObject Princess;
    public GameObject OldMan;
    public Transform Marker1;
    //public Transform IntermediatePoint;
    //public Text ThinkOfPrincess;



    private BehaviorAgent agent;
    private int whichArc;

	// Use this for initialization
	void Start () {
        //ThinkOfPrincess.text = "";

        agent = new BehaviorAgent(this.HeroTreeRoot());
        BehaviorManager.Instance.Register(agent);
        agent.StartBehavior();

        //whichArc = (int)Random.Range(1, 5);
        whichArc = 1;
        print("Arc Selected: " + whichArc);

	}
	
	// Update is called once per frame
    void Update()
    {

    }


    //
    //MAIN TREE ROOT
    //
    protected Node HeroTreeRoot()
    {
        Sequence Arc = new Sequence(                                                                         
                this.ThinkAboutPrincess(),
                this.GoToPrincess(), 
                this.HeroWaveAt(this.Princess),
                this.PrincessOrientTo(this.Hero),
                this.PrincessWaveAt(this.Hero),
                this.FirstEncounter());

        Sequence newArc = new Sequence(
            this.ST_GoToInter(this.Marker1),
            this.PickArc());

        return new Sequence(Arc, newArc);
    }



    //--------------------------------------------------------------
    //These are the root nodes for the four different story arcs
    //LovePoemArc, GemArc, RocksArc, PoopArc
    //--------------------------------------------------------------
    #region
    //LOVE POEM STORY ARC ROOT NODE
    protected Node LovePoemArc()
    {
        Sequence Poem = new Sequence(ChooseInfo());

        return Poem;
    }
    #endregion  

    #region
    //GEM STORY ARC ROOT NODE
    protected Node GemArc()
    {
        return new Sequence();
    }
    #endregion

    #region
    //ROCKS STORY ARC ROOT NODE
    protected Node RocksArc()
    {
        return new Sequence();

    }
    #endregion

    #region
    //POOP STORY ARC ROOT NODE
    protected Node PoopArc()
    {
        return new Sequence();
    }
    #endregion


    protected Node ThinkAboutPrincess()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Think", 4000));
    }

    protected Node GoToPrincess()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(Princess.transform.position, 2.0f));
    }

    protected Node GoToOldMan()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(OldMan.transform.position, 2.0f));
    }

    protected Node HeroWaveAt(GameObject person)
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Wave", 4000));
    }

    protected Node PrincessWaveAt(GameObject person)
    {
        return new Sequence(Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Wave", 4000));
    }

    protected Node PrincessOrientTo(GameObject person)
    {
        return new Sequence(Princess.GetComponent<BehaviorMecanim>().Node_OrientTowards(person.transform.position));
    }




    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK!!!!!!!!!!!!!!!!!!!!!!!
    protected Node ST_GoToInter(Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(position));
    }
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK IT SHOULD WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK IT SHOULD WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK IT SHOULD WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK IT SHOULD WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK IT SHOULD WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK IT SHOULD WORK!!!!!!!!!!!!!!!!!!!!!!!
    //!!!!!!!!!!!!!!!!!WHY DOESNT THIS FUCKING WORK IT SHOULD WORK!!!!!!!!!!!!!!!!!!!!!!!


    protected Node ST_GoToMarker()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(Marker1.position));
    }

    #region
    //
    //CONVERSATION NODES
    //
    protected Node FirstEncounter()
    {
        Sequence HandP = new Sequence(
            Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Wonderful", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Surprised", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Shock", 4000),
            Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Writing", 4000),
            Hero.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("Acknowledge", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Cheer", 4000)
            );
        
        return new DecoratorForceStatus(RunStatus.Success, HandP);
    }

    protected Node TalkPrincess()
    {
        Sequence getInfo = new Sequence(
            GoToPrincess(),
            HeroWaveAt(Princess),
            PrincessWaveAt(Hero),
            Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("ReachingRight", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("LookAway", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Think", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("HeadNod", 4000),
            Hero.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("HeadNod", 4000),
            Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Writing", 4000)
            );

        return getInfo;
    }

    protected Node TalkOldMan()
    {
        return new Sequence();
    }

    #endregion



    protected Node FindNote()
    {
        return new Sequence();
    }





    protected Node ChooseInfo()
    {
        SelectorShuffle whichGuy = new SelectorShuffle(TalkPrincess() , TalkOldMan() , FindNote());
        return whichGuy;
    }



    //
    //Picks the sub-arc based on the random number generated after talking to the princess
    //
    protected Node PickArc()
    {
        if(whichArc == 1)
        {
            return new Sequence(LovePoemArc());
        }

        else if(whichArc == 2)
        {
            return new Sequence(GemArc());
        }

        else if(whichArc == 3)
        {
            return new Sequence(RocksArc());
        }

        else
        {
            return new Sequence(PoopArc());
        }
    }


}
