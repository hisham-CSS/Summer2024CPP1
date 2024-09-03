using TMPro;


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

    private void OnLifeValueChanged(int lives)
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLifeValueChanged -= OnLifeValueChanged;
    }
}
