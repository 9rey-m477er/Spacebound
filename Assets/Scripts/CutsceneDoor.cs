using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneDoor : MonoBehaviour, IDataPersistence
{
    public Transform teleportTarget;
    public Image fadeImage;
    public float fadeDuration = 0.3f;
    private DataPersistenceManager dpm;
    public DialogueController dC;
    public DialogueText cutscene;
    private bool cutscenePlayed;
    public string id;

    [ContextMenu("Generate GUID for cutscene ID")]
    private void generateGUID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Start()
    {
        dpm = GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>();
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
        if (!cutscenePlayed)
        {
            cutscenePlayed = true;
            dC.startCutscene(cutscene);
        }
        //dpm.SaveGame();
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

    public void LoadData(GameData data)
    {
        data.cutscenesWatched.TryGetValue(id, out cutscenePlayed);
    }

    public void SaveData(ref GameData data)
    {
        if (data.cutscenesWatched.ContainsKey(id))
        {
            data.cutscenesWatched.Remove(id);
        }
        data.cutscenesWatched.Add(id, cutscenePlayed);
    }
}
