
public class QuitGameButton : WorldButton
{
    public override void OnClick()
    {
        MainMenuManager.Instance.QuitGame();
    }
}