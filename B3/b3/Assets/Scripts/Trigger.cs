using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RootMotion.FinalIK;
using TreeSharpPlus;

public class Trigger : MonoBehaviour {

    public Text E;
    public Text HeartText;
    public Text GemText;
    public Text KeyText;
    public Text SlabText;
    public Text FountainText;
    public Text PrincessTalk;
    public Text OldGuyTalk;
    public Text FountainTalk;
    public Text SlabTalk;
    public Text KeyTalk;
    public Text LockedTalk;
    public GameObject Panel;
    public GameObject Princess;
    public GameObject OldMan;
    public GameObject Gate;
    public GameObject Heart;
    public GameObject Key;
    public GameObject Gem;

    private BehaviorAgent theAgent;

    private bool inTriggerPrincess = false;
    private bool inTriggerOldMan = false;
    private bool inTriggerTreasure = false;
    private bool inTriggerHeart = false;
    private bool inTriggerGem = false;
    private bool inTriggerGate = false;
    private bool inTriggerGiveGift = false;
    private bool inTriggerSlab = false;
    private bool inTriggerFountain = false;
    private bool hasGift = false;
    private bool hasKey = false;

    void Start()
    {
        Panel.SetActive(false);
        E.text = "";
        HeartText.text = "";
        GemText.text = "";
        KeyText.text = "";
        SlabText.text = "";
        FountainText.text = "";
        PrincessTalk.text = "";
        OldGuyTalk.text = "";
        FountainTalk.text = "";
        SlabTalk.text = "";
        KeyTalk.text = "";
        LockedTalk.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inTriggerPrincess)
        {
            E.text = "";
            theAgent = new BehaviorAgent(this.PrincessInfo());
            BehaviorManager.Instance.Register(theAgent);
            theAgent.StartBehavior();
        }

        if (Input.GetKeyDown(KeyCode.E) && inTriggerOldMan)
        {
            E.text = "";
            theAgent = new BehaviorAgent(this.OldGuyInfo());
            BehaviorManager.Instance.Register(theAgent);
            theAgent.StartBehavior();
        }

        if (Input.GetKeyDown(KeyCode.E) && inTriggerHeart)
        {
            E.text = "";
            PickUpHeart();
        }

        if (Input.GetKeyDown(KeyCode.E) && inTriggerTreasure)
        {
            E.text = "";
            PickUpTreasure();
        }

        if (Input.GetKeyDown(KeyCode.E) && inTriggerSlab)
        {
            E.text = "";
            PickUpSlab();
        }

        if (Input.GetKeyDown(KeyCode.E) && inTriggerFountain)
        {
            E.text = "";
            PickUpFountain();
        }

        if(hasKey)
        {
            if (Input.GetKeyDown(KeyCode.E) && inTriggerGate)
            {
                E.text = "";
                UnlockGate();
            }
        }
        if(!hasKey)
        {
            if(Input.GetKeyDown(KeyCode.E) && inTriggerGate)
            {
                Panel.SetActive(true);
                LockedTalk.text = "I think I need a key to open this.";
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && inTriggerGem)
        {
            E.text = "";
            PickUpGem();
        }

        if(hasGift)
        {
            if (Input.GetKeyDown(KeyCode.E) && inTriggerGiveGift)
            {
                E.text = "";
                theAgent = new BehaviorAgent(this.Celebrate());
                BehaviorManager.Instance.Register(theAgent);
                theAgent.StartBehavior();
            }
        }

    }


    void OnTriggerEnter(Collider trigger)
    {
        if (!hasGift)
        {
            if (trigger.gameObject.CompareTag("PrincessConvoTrigger"))
            {
                inTriggerPrincess = true;
                E.text = "Press E To Talk";

            }
        }

        if (trigger.gameObject.CompareTag("PickUpHeartTrigger"))
        {
            inTriggerHeart = true;
            E.text = "Press E To Interact";

        }

        if (trigger.gameObject.CompareTag("PickUpTreasureTrigger"))
        {
            inTriggerTreasure = true;
            E.text = "Press E To Interact";

        }

        if (trigger.gameObject.CompareTag("PickUpSlabTrigger"))
        {
            inTriggerSlab = true;
            E.text = "Press E To Interact";

        }

        if (trigger.gameObject.CompareTag("OldManConvoTrigger"))
        {
            inTriggerOldMan = true;
            E.text = "Press E To Talk";

        }
        if (trigger.gameObject.CompareTag("PickUpFountain"))
        {
            inTriggerFountain = true;
            E.text = "Press E To Interact";

        }

        if (trigger.gameObject.CompareTag("PickUpDiamond"))
        {
            inTriggerGem = true;
            E.text = "Press E To Interact";
        }

        if(hasGift)
        {
            if (trigger.gameObject.CompareTag("GiveGiftTrigger"))
            {
                inTriggerGiveGift = true;
                E.text = "Press E To Talk";
            }
        }


        if (trigger.gameObject.CompareTag("UnlockGate"))
        {
            inTriggerGate = true;
            E.text = "Press E To Interact";

        }

    }

    void OnTriggerExit()
    {
        E.text = "";
        Panel.SetActive(false);
        inTriggerPrincess = false;
        inTriggerOldMan = false;
        inTriggerTreasure = false;
        inTriggerHeart = false;
        inTriggerGem = false;
        inTriggerFountain = false;
        inTriggerGate = false;
        inTriggerSlab = false;
        inTriggerGiveGift = false;
        PrincessTalk.text = "";
        OldGuyTalk.text = "";
        FountainTalk.text = "";
        SlabTalk.text = "";
        KeyTalk.text = "";
        LockedTalk.text = "";
    }


    //
    //Tree Nodes
    //

    #region
    protected Node PrincessInfo()
    {
        Panel.SetActive(true);
        PrincessTalk.text = "If you want to impress me, go find something shiny or pretty.";
        return new Sequence(
            this.PrincessOrientToHero(),
            this.PrincessWave(),
            this.ST_GetPrincessInfo()
            );
    }

    protected Node Celebrate()
    {
        SequenceParallel dance = new SequenceParallel(
            Princess.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture("Breakdance", 10000),
            Princess.GetComponent<BehaviorMecanim>().Node_OrientTowards(transform.position));

        SequenceParallel goodbye = new SequenceParallel(
            this.PrincessWave()
            );
        Panel.SetActive(true);
        PrincessTalk.text = "Wow this is so cool! You're the best! Thank You!";
        return new Sequence(new LeafWait(500), dance, new LeafWait(500), goodbye);
    }

    protected Node PrincessWave()
    {
        return new Sequence(Princess.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Wave", 4000));
    }

    protected Node OldManWave()
    {
        return new Sequence(OldMan.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("Wave", 4000));
    }

    protected Node PrincessOrientToHero()
    {
        return new Sequence(Princess.GetComponent<BehaviorMecanim>().Node_OrientTowards(transform.position));
    }

    protected Node OldManOrientToHero()
    {
        return new Sequence(OldMan.GetComponent<BehaviorMecanim>().Node_OrientTowards(transform.position));
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

    protected Node root()
    {
        return new DecoratorLoop(new Sequence());
    }

    protected Node OldGuyInfo()
    {
        Panel.SetActive(true);
        OldGuyTalk.text = "I saw some cool stuff by the fountain and the ruins. Check them out!";
        return new Sequence(
            this.OldManOrientToHero(),
            this.OldManWave(),
            this.ST_GetOldGuyInfo()
            );
    }
    #endregion



    private void PickUpHeart()
    {
        Heart.SetActive(false);
        HeartText.text = "Purple Heart";
        hasGift = true;

    }

    private void PickUpTreasure()
    {
        Key.SetActive(false);
        Panel.SetActive(true);
        KeyText.text = "Key";
        KeyTalk.text = "I bet this unlocks something...";
        hasKey = true;
    }

    private void PickUpGem()
    {
        Gem.SetActive(false);
        GemText.text = "Gem";
        hasGift = true;
    }

    private void PickUpSlab()
    {
        SlabText.text = "Ancient Text";
        Panel.SetActive(true);
        SlabTalk.text = "The heart of a Lion";
    }
   
    private void PickUpFountain()
    {
        FountainText.text = "Ancient Text"; 
        Panel.SetActive(true);
        FountainTalk.text = "A hidden secret locked away...";
    }

    private void UnlockGate()
    {
        Gate.transform.Translate(new Vector3(0, 0, 3) * 1);
    }

}
