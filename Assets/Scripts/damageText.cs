using TMPro;
using UnityEngine;

public class damageText : MonoBehaviour
{
    public float floatSpeed = 50f;
    public float fadeDuration = 1.5f;

    private TextMeshProUGUI textMesh;
    private Color originalColor;
    private float timeElapsed = 0f;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        originalColor = textMesh.color;
    }

    private void Update()
    {
        // Move upwards
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Fade out
        timeElapsed += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
        textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

        // Destroy after fade
        if (timeElapsed >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}
