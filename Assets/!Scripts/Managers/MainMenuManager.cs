using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField] private string startSceneName;
    [SerializeField] private LayerMask worldButtonLayer;

    private Transform camTransform;

    private void Start()
    {
        camTransform = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 rayOrigin = camTransform.position;
        Vector3 rayDirection = camTransform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit worldButtonHit, 10f, worldButtonLayer))
        {
            worldButtonHit.collider.TryGetComponent<WorldButton>(out WorldButton worldButton);

            if (Input.GetButton("Fire1"))
            {
                worldButton.OnClick();
            }
        }
    }

    public void StartGame()
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