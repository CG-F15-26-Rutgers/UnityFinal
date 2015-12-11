using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class HeroTree : MonoBehaviour {

    public GameObject Hero;
    public GameObject Princess;
    public GameObject OldMan;

    public Transform princessTalkPos;
    public Transform oldmanTalkPos;
    public Transform marker;


    private BehaviorAgent heroAgent;

    private bool princessInfo = false;
    private bool oldManInfo = false;


	// Use this for initialization
	void Start () {
        heroAgent = new BehaviorAgent(this.HeroTreeRoot());
        BehaviorManager.Instance.Register(heroAgent);
        heroAgent.StartBehavior();
    }
	
	void Update () {
	
	}



    //
    //Root of the Hero's Tree
    //
    protected Node HeroTreeRoot()
    {
        return new Sequence(
            this.Think(),
            this.GoToPrincess(),
            this.HeroOrientToPrincess(),
            this.HeroWave(),
            this.PrincessOrientToHero(),
            this.PrincessWave(),
            this.FirstConversation(),
            this.GoToMarker(),
            this.SelectStoryArc()
            );
    }

    //
    //Root of the Love Poem Arc
    //
    protected Node LovePoemArc()
    {
        SelectorShuffle info = new SelectorShuffle(PrincessInfo(), OldGuyInfo());
        //Sequence checkFirst = new Sequence(new LeafAssert(princessInfo));
        return info;
    }



    //
    //Root of the Gem Arc
    //
    protected Node GemArc()
    {
        return new Sequence();
    }



    #region
    //
    //Leaf Nodes
    //
    protected Node Think()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Think", 4000));
    }

    protected Node GoToPrincess()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(princessTalkPos.position));
    }

    protected Node GoToOldGuy()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(oldmanTalkPos.position));
    }

    protected Node HeroOrientToPrincess()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_OrientTowards(Princess.transform.position));
    }

    protected Node HeroOrientToOldMan()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_OrientTowards(OldMan.transform.position));
    }

    protected Node HeroWave()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Wave", 4000));
    }

    protected Node OldManWave()
    {
        return new Sequence(OldMan.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Wave", 4000));
    }

    protected Node PrincessOrientToHero()
    {
        return new Sequence(Princess.GetComponent<BehaviorMecanim>().Node_OrientTowards(Hero.transform.position));
    }

    protected Node OldManOrientToHero()
    {
        return new Sequence(OldMan.GetComponent<BehaviorMecanim>().Node_OrientTowards(Hero.transform.position));
    }

    protected Node PrincessWave()
    {
        return new Sequence(Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Wave", 4000));
    }

    protected Node FirstConversation()
    {
        return new Sequence(
            Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Wonderful", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Shock", 4000),
            Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Surprised", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("HeadShakeThink", 4000),
            Hero.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("HeadNod", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Cheer", 4000)
            );
    }

    protected Node GoToMarker()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(marker.transform.position));
    }

    protected Node ST_GetPrincessInfo()
    {
        return new Sequence(
            Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Think", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("Sad", 4000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("Roar", 4000)
            );
    }

    protected Node ST_GetOldGuyInfo()
    {
        return new Sequence(
            OldMan.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Think", 4000),
            OldMan.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("Sad", 4000),
            OldMan.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("Roar", 4000),
            OldMan.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("HandsUp", 4000)
            );
    }

















    #endregion



    protected Node SelectStoryArc()
    {
        return new SelectorShuffle(LovePoemArc(), GemArc());
    }

    protected Node PrincessInfo()
    {
        princessInfo = true;
        return new Sequence(
            this.GoToPrincess(),
            this.HeroOrientToPrincess(),
            this.HeroWave(), 
            this.PrincessOrientToHero(),
            this.PrincessWave(),
            this.ST_GetPrincessInfo()
            );
    }

    protected Node OldGuyInfo()
    {
        oldManInfo = true;
        return new Sequence(
            this.GoToOldGuy(),
            this.HeroOrientToOldMan(),
            this.HeroWave(),
            this.OldManOrientToHero(),
            this.OldManWave(),
            this.ST_GetOldGuyInfo()
            );
    }

    



















}
