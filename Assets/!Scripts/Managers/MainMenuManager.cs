using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string startSceneName;
    
    public void StarGame()
    {
        SceneManager.LoadScene(startSceneName);
    }

    public void QuitGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit();
    }
}