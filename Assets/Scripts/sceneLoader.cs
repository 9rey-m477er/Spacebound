using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneLoader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 0.3f;
    public float fadeDuration2 = 0.3f;

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

    public IEnumerator goToMain()
    {
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade2(1));
        Debug.Log("Loading Main Menu");
        SceneManager.LoadScene("Main Menu");
    }
    public void MainMenu()
    {
        StartCoroutine(goToMain());
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

    private IEnumerator Fade2(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration2)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration2);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        // Ensure the final alpha value is set
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }
}
