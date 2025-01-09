using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class doorscrip : MonoBehaviour
{
    public GameObject fadePanel; // Assign your fade panel in the Inspector
    public float fadeDuration = 1f; // Time it takes to fade
    public KeyCode interactKey = KeyCode.E; // Key to press for interaction
    public string nextScene = "NextScene"; // Replace with your scene name
    private bool isPlayerNearby = false;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            StartCoroutine(FadeOut());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private IEnumerator FadeOut()
    {
        CanvasGroup canvasGroup = fadePanel.GetComponent<CanvasGroup>();

        // Fade in (screen turns black)
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
        canvasGroup.alpha = 1;

        // Wait briefly, then load the next scene
        yield return new WaitForSeconds(0.5f);

        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
