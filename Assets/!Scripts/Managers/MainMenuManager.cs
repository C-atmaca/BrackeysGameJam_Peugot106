using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField] private string startSceneName;
    [SerializeField] private LayerMask worldButtonLayer;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;
    private Transform camTransform;

    private int pageNumber = -1;

    private void Start()
    {
        camTransform = Camera.main.transform;
        image.gameObject.SetActive(false);
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

    public void TutorialNextPage()
    {
        pageNumber++;

        Debug.Log(pageNumber);
        Debug.Log(image.sprite);
        switch (pageNumber)
        {
            case 0:
                image.gameObject.SetActive(true);
                foreach (var item in objects)
                {
                    item.SetActive(false);
                }
                image.sprite = sprites[0];
            break;
            case 1:
                image.sprite = sprites[1];
            break;
            case 2:
                image.sprite = sprites[2];
            break;
            case 3:
                image.sprite = sprites[3];
            break;
            case 4:
                image.sprite = sprites[4];
            break;
            case 5:
                foreach (var item in objects)
                {
                    item.SetActive(true);
                }
                image.gameObject.SetActive(false);
                pageNumber = -1;
            break;
        }

    }


    public void QuitGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Application.Quit();
    }
}