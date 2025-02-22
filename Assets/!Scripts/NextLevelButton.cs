using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    private Button myButton;

    private void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(LevelManager.Instance.NextLevel);
    }
}