
public class StartGameWorldButton : WorldButton
{
    public override void OnClick()
    {
        MainMenuManager.Instance.StartGame();
    }
}