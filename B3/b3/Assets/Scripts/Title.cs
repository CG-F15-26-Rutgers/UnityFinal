using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Title : MonoBehaviour {

    public Canvas QuitMenu;
    public Button Interactive;
    public Button NonInteractive;
    public Button Quit;


	// Use this for initialization
	void Start () {
        QuitMenu = QuitMenu.GetComponent<Canvas>();
        Interactive = Interactive.GetComponent<Button>();
        NonInteractive = NonInteractive.GetComponent<Button>();
        Quit = Quit.GetComponent<Button>();

        QuitMenu.enabled = false;
    }

    public void QuitClick()
    {
        QuitMenu.enabled = true;
        Interactive.enabled = false;
        NonInteractive.enabled = false;
        Quit.enabled = false;
    }

    public void NoPress()
    {
        QuitMenu.enabled = false;
        Interactive.enabled = true;
        NonInteractive.enabled = true;
        Quit.enabled = true;
    
    }

    public void StartInteractive()
    {
        Application.LoadLevel(1);
    }

    public void StartNonInteractive()
    {
        Application.LoadLevel(2);
    }

    public void Exit()
    {
        Application.Quit();
    }



}
