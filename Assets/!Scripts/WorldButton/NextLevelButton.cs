
public class NextLevelButton : WorldButton
{
    public override void OnClick()
    {
        LevelManager.Instance.NextLevel();
    }
}