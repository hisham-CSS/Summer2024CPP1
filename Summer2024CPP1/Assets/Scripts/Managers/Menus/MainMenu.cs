using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    public Button playButton;
    public Button settingsButton;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.MainMenu;
        playButton.onClick.AddListener(() => GameManager.Instance.LoadScene("Level"));
        settingsButton.onClick.AddListener(() => context.SetActiveState(MenuController.MenuStates.Settings));
    }
}
