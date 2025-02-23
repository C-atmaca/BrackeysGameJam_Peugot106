using UnityEngine;

public class NextTutorialButton : WorldButton
{
    public override void OnClick()
    {
        MainMenuManager.Instance.TutorialNextPage();
    }
}