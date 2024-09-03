using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    public Button resumeGame;
    public Button returnToMenu;
    public Button quitGame;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Pause;
        resumeGame.onClick.AddListener(JumpBack);
        returnToMenu.onClick.AddListener(() => GameManager.Instance.LoadScene("Title"));
        quitGame.onClick.AddListener(QuitGame);
    }
}
