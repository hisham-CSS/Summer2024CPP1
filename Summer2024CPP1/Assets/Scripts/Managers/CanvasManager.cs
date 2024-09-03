using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//OLD CANVAS MANAGER - USED FOR EXAMPLE PURPOSES AND CAN STILL BE USEFUL IN A SMALLER USE CASE
//FOR A BIGGER GAME - USE A STATE MACHINE OR SIMILAR SOLUTION
public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button playButton;
    public Button settingsButton;
    public Button backButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        if (playButton)
        {
            playButton.onClick.AddListener(() => GameManager.Instance.LoadScene("Level"));
            //add a listener to the onclick event
        }

        if (settingsButton)
        {
            settingsButton.onClick.AddListener(() => SetMenus(settingsMenu, mainMenu));
            //add a listener to the onclick event
        }

        if (backButton)
        {
            backButton.onClick.AddListener(() => SetMenus(mainMenu, settingsMenu));
        }
    }

    private void SetMenus(GameObject menuToActivate, GameObject menuToDisable)
    {
        if (menuToActivate)
            menuToActivate.SetActive(true);
        
        if (menuToDisable)
            menuToDisable.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
