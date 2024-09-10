using TMPro;
using UnityEngine;


public class IngameMenu : BaseMenu
{
    public TMP_Text livesText;
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.InGame;
        livesText.text = "Lives: " + GameManager.Instance.lives.ToString();
        GameManager.Instance.OnLifeValueChanged += OnLifeValueChanged;
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 1.0f;
    }

    private void OnLifeValueChanged(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLifeValueChanged -= OnLifeValueChanged;
    }
}
