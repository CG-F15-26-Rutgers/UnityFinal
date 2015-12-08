using UnityEngine;
using System.Collections;
using TreeSharpPlus;
using UnityEngine.UI;


public class HeroTree : MonoBehaviour {

    public GameObject Hero;
    public GameObject Princess;
    public GameObject OldMan;
    public GameObject Building;
    //public Transform IntermediatePoint;
    //public Text ThinkOfPrincess;


    private BehaviorAgent agent;
    private int whichArc;

	// Use this for initialization
	void Start () {
        //whichArc = (int)Random.Range(1, 5);
        //ThinkOfPrincess.text = ""; 
        whichArc = 1;
        Debug.Log("Arc Selected: " + whichArc);
        agent = new BehaviorAgent(this.HeroTreeRoot());
        BehaviorManager.Instance.Register(agent);
        agent.StartBehavior();

       

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
       return new DecoratorLoop(
             new Sequence(                                                                         
                this.ThinkAboutPrincess(),
                this.GoToPrincess(),
                this.HeroWaveAt(this.Princess),
                this.PrincessOrientTo(this.Hero),
                this.PrincessWaveAt(this.Hero),
                this.FirstEncounter(),
                this.PrincessOrientTo(this.OldMan),
                this.PrincessWaveAt(this.OldMan),


                //PEOPLE WONT MOVE TO A NEW LOCATION BUT THEY WILL DO OTHER THINGS
                //Want this to work
                //Show this to Mahyar, this part
                this.GoToBuilding(this.Building.transform),
                //Tick on non running node, but this SHOULD NOT HAPPEN 
                //I dont know why this happens...


                this.ThinkAboutPrincess(),
                this.PickArc(whichArc)
                ));
    }



    //--------------------------------------------------------------
    //These are the root nodes for the two different story arcs
    //LovePoemArc, GemArc
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



    protected Node GoToBuilding(Transform target)
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(target.position, 6.0f));
    }
    


    protected Node PrincessGoTo(Transform target)
    {
        return new Sequence(Princess.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(target.position, 5.0f));
    }

    #region
    //
    //CONVERSATION NODES
    //
    protected Node FirstEncounter()
    {
        return new Sequence(
            Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Wonderful", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Surprised", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Shock", 4000),
            Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Writing", 4000),
            Hero.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("Acknowledge", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Cheer", 4000)
            );
        
    }

    protected Node TalkPrincess()
    {
        Sequence getInfo = new Sequence(
            this.GoToPrincess(),
            this.HeroWaveAt(Princess),
            this.PrincessWaveAt(Hero),
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
    protected Node PickArc(int whichStory)
    {
        if(whichStory == 1)
            return new Sequence(LovePoemArc());
        else
            return new Sequence(GemArc());
    }


}
