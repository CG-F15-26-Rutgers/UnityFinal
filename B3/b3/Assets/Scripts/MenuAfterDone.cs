﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuAfterDone : MonoBehaviour {

    public Canvas Menu;
    public Button Replay;
    public Button MainMenu;

	// Use this for initialization
    void Start()
    {
        Menu = Menu.GetComponent<Canvas>();
        Replay = Replay.GetComponent<Button>();
        MainMenu = MainMenu.GetComponent<Button>();

    }


    public void ReplayClick()
    {
        Application.LoadLevel(2);
    }

    public void MMClick()
    {
        Application.LoadLevel(0);
    }

}
