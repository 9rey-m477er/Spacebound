using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WorldDoorTest : MonoBehaviour
{
    public Transform teleportTarget;
    public Image fadeImage;
    public float fadeDuration = 0.3f;

    private void Start()
    {
        fadeImage.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportWithFade(other));
        }
    }

    private IEnumerator TeleportWithFade(Collider2D player)
    {
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));
        player.transform.position = teleportTarget.position;
        yield return StartCoroutine(Fade(0));
        fadeImage.gameObject.SetActive(false);
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
