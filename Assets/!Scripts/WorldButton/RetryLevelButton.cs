
public class RetryLevelButton : WorldButton
{
    public override void OnClick()
    {
        LevelManager.Instance.TryAgain();
    }
}