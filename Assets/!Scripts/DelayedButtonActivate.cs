using System.Collections;
using UnityEngine;

public class DelayedButtonActivate : MonoBehaviour
{

    [SerializeField] private float delay;

    private void Start()
    {
        buttons.SetActive(false);
        StartCoroutine(ShowButtonsAfterDelay());
    }

    private IEnumerator ShowButtonsAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        buttons.SetActive(true);
    }
}