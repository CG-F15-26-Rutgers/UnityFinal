using UnityEngine;
using System.Collections;
using TreeSharpPlus;
using RootMotion.FinalIK;
using UnityEngine.UI;

public class HeroTree : MonoBehaviour {

    public GameObject Hero;
    public GameObject Princess;
    public GameObject OldMan;
    public GameObject Gate;

    public Transform princessTalkPos;
    public Transform oldmanTalkPos;
    public Transform cameraPos;
    public Transform fountainPos;
    public Transform gateUnlockPos;
    public Transform treasurePos;
    public Transform gemPos;
    public Transform marker;
    public Transform noteInfoMarker;
    public Transform pickUpSlab;
    public Transform lovePoemMarker;

    public Collider trigger;

    public Canvas Menu;
    public Button Replay;
    public Button MainMenu;

    public bool complete = false;

    //Pick up slab
    public FullBodyBipedEffector Effector;
    public InteractionObject slab;

    //Pick up poem
    public FullBodyBipedEffector EffectorTwo;
    public InteractionObject LovePoem;

    //Give gifts
    public FullBodyBipedEffector EffectorThreePt1;
    public FullBodyBipedEffector EffectorThreePt2;
    public InteractionObject giftGive;

    //Pick up fountain info
    public FullBodyBipedEffector EffectorFour;
    public InteractionObject FInfo;

    //Open treasure chest
    public FullBodyBipedEffector EffectorFive;
    public InteractionObject treasureChest;

    //Unlock gate
    public FullBodyBipedEffector EffectorSix;
    public InteractionObject gateLock;

    //Pick up gem
    public FullBodyBipedEffector EffectorSeven;
    public InteractionObject Gem;


    private BehaviorAgent heroAgent;

    private bool IsGateOpen = false;
    private bool hasGem = false;

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
            this.Think(),
            this.SelectStoryArc()
            );
    }

    #region
    //
    //The Two Sub-Story Arcs
    //



    //
    //Root of the Love Poem Arc
    //
    protected Node LovePoemArc()
    {
        SequenceShuffle info = new SequenceShuffle(
            this.PrincessInfo(),
            this.OldGuyInfo(),
            this.getNoteInfo()
        );

        Sequence cont = new Sequence(
            this.GoToMarker(),
            this.Think(), 
            this.CheckInfo(), 
            this.Realization()
        );

        Sequence giveItToHer = new Sequence(
            this.GoToLion(),
            this.ST_PickUpLovePoem(),
            this.GoToPrincess(),
            this.HeroOrientToPrincess(), 
            new LeafWait(500),
            this.PrincessOrientToHero(),
            new LeafWait(500),
            this.GiveGift(),
            this.Celebrate()
            );

        return new Sequence(info, cont, giveItToHer);
    }



    //
    //Root of the Gem Arc
    //
    protected Node GemArc()
    {
        SequenceShuffle info = new SequenceShuffle(
            this.OldGuyInfo(),
            this.getNoteInfo(),
            this.getFountainInfo()
            );

        Sequence cont = new Sequence(
            this.GoToMarker(),
            this.Think(),
            this.CheckInfo(),
            this.Realization());

        Sequence unlockGem = new Sequence(
            this.GoToTreasure(),
            this.ST_OpenTreasureChest(),
            this.GoToGate(),
            this.ST_UnlockGate(),
            new LeafWait(500)
            );

            Sequence giveItToHer = new Sequence(
                this.GoToGem(),
                this.ST_PickUpGem(),
                this.GoToPrincess(),
                this.HeroOrientToPrincess(),
                new LeafWait(500),
                this.PrincessOrientToHero(),
                new LeafWait(500),
                this.GiveGift(),
                this.Celebrate()
            );
        

        return new Sequence(info, cont, unlockGem, giveItToHer);
    }

    #endregion

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

    protected Node CheckInfo()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Read", 5000));
    }

    protected Node GoToGate()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(gateUnlockPos.position));
    }

    protected Node GoToTreasure()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(treasurePos.position));
    }

    protected Node GoToGem()
    {
        Val<Vector3> pos = Val.V(() => gemPos.position); 
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(pos));
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

    protected Node GiveGift()
    {
        Val<FullBodyBipedEffector> effectorOne = Val.V(() => EffectorThreePt1);
        Val<FullBodyBipedEffector> effectorTwo = Val.V(() => EffectorThreePt2);
        Val<InteractionObject> giftGiving = Val.V(() => giftGive);
        return new SequenceParallel(new LeafTrace("Give Gift"),
            Hero.GetComponent<BehaviorMecanim>().Node_StartInteraction(effectorOne, giftGiving),
            Princess.GetComponent<BehaviorMecanim>().Node_StartInteraction(effectorTwo, giftGiving),
            new LeafWait(1000));
    }

    protected Node GoToMarker()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(marker.transform.position));
    }

    protected Node GoToFountain()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(fountainPos.position));
    }
    
    protected Node ST_PickUpFountain()
    {
        Val<FullBodyBipedEffector> effecting = Val.V(() => EffectorFour);
        Val<InteractionObject> takeFountain = Val.V(() => FInfo);
        return new SequenceParallel(new LeafTrace("Pick Up Fountain Info"), Hero.GetComponent<BehaviorMecanim>().Node_StartInteraction(effecting, takeFountain), new LeafWait(1000));
    }

    protected Node ST_PickUpGem()
    {
        Val<FullBodyBipedEffector> effecting = Val.V(() => EffectorSeven);
        Val<InteractionObject> getGem = Val.V(() => Gem);
        return new SequenceParallel(new LeafTrace("Pick Up Gem"), Hero.GetComponent<BehaviorMecanim>().Node_StartInteraction(effecting, getGem), new LeafWait(1000));
    }

    protected Node ST_UnlockGate()
    {
        Val<FullBodyBipedEffector> effecting = Val.V(() => EffectorSix);
        Val<InteractionObject> openGate = Val.V(() => gateLock);
        IsGateOpen = true;
        return new SequenceParallel(new LeafTrace("Interaction"), Hero.GetComponent<BehaviorMecanim>().Node_StartInteraction(effecting, openGate));
    }

    protected Node ST_OpenTreasureChest()
    {
        Val<FullBodyBipedEffector> effecting = Val.V(() => EffectorFive);
        Val<InteractionObject> openChest = Val.V(() => treasureChest);
        return new SequenceParallel(new LeafTrace("Open Chest"), Hero.GetComponent<BehaviorMecanim>().Node_StartInteraction(effecting, openChest));
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

    protected Node GoToNoteMarker()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(noteInfoMarker.position));
    }

    protected Node ST_PickUpTablet()
    {
        Val<FullBodyBipedEffector> effecting = Val.V(() => Effector);
        Val<InteractionObject> theThing = Val.V(() => slab);
        return new SequenceParallel(new LeafTrace("Pick Up Info"), Hero.GetComponent<BehaviorMecanim>().Node_StartInteraction(effecting, theThing), new LeafWait(1000));
    }

    protected Node ST_PickUpLovePoem()
    {
        Val<FullBodyBipedEffector> effecting = Val.V(() => EffectorTwo);
        Val<InteractionObject> theThingy = Val.V(() => LovePoem);
        return new SequenceParallel(new LeafTrace("Pick Up Love Poem"), Hero.GetComponent<BehaviorMecanim>().Node_StartInteraction(effecting, theThingy), new LeafWait(1000));
    }

    protected Node Realization()
    {
        return new SequenceShuffle(
            Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("CrowdPump", 4000),
            Hero.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("ChestPumpSalute", 4000));
    }

    protected Node Celebrate()
    {
        Val<Vector3> camPos = Val.V(() => cameraPos.position);

        SequenceParallel lookEyeCamera = new SequenceParallel(
            Hero.GetComponent<BehaviorMecanim>().Node_HeadLook(camPos),
            Princess.GetComponent<BehaviorMecanim>().Node_HeadLook(camPos));
        
        SequenceParallel dance = new SequenceParallel(
            Hero.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture("Breakdance", 5000),
            Princess.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture("Breakdance", 5000));

        SequenceParallel goodbye = new SequenceParallel(
            this.HeroWave(),
            this.PrincessWave()
            );

        complete = true;
        return new Sequence(new LeafWait(500), lookEyeCamera, new LeafWait(500), dance, new LeafWait(500), goodbye);
    }

    protected Node GoToLion()
    {
        return new Sequence(Hero.GetComponent<BehaviorMecanim>().Node_GoTo(lovePoemMarker.position));
    }

    #endregion

    #region
    //
    //Gathering Info Nodes
    //
    protected Node PrincessInfo()
    {
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
        return new Sequence(
            this.GoToOldGuy(),
            this.HeroOrientToOldMan(),
            this.HeroWave(),
            this.OldManOrientToHero(),
            this.OldManWave(),
            this.ST_GetOldGuyInfo()
            );
    }

    protected Node getNoteInfo()
    {
        return new Sequence(
            this.GoToNoteMarker(),
            this.ST_PickUpTablet()
            );
    }

    protected Node getFountainInfo()
    {
        return new Sequence(
            this.GoToFountain(),
            this.ST_PickUpFountain()
            );
    }

    #endregion

    protected Node SelectStoryArc()
    {
        return new SelectorShuffle(LovePoemArc(), GemArc());
    }

    void OnTriggerEnter(Collider trigger)
    {
        Vector3 NewGatePos = Gate.transform.position + new Vector3(0, 0, 3.0f);
        Gate.transform.position = NewGatePos;
    }

}
