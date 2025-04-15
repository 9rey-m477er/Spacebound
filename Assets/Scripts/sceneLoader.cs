using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneLoader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 0.3f;
    private void Start()
    {
        fadeImage.gameObject.SetActive(false);
    }
    public IEnumerator LoadGame()
    {
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public IEnumerator MainMenu()
    {
        yield return StartCoroutine(Fade(1));
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("Main Menu");
        yield return StartCoroutine(Fade(0));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        // Ensure the final alpha value is set
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }
}
