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

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1.0f;
    }

    private void OnDisable()
    {
        resumeGame.onClick.RemoveListener(JumpBack);
        quitGame.onClick.RemoveListener(QuitGame);
    }

    private void OnDestory()
    {
        resumeGame.onClick.RemoveListener(JumpBack);
        quitGame.onClick.RemoveListener(QuitGame);
    }
}
