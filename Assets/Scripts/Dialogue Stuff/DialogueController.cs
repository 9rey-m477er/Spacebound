using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private TextMeshProUGUI npcDialogueText;
    //[SerializeField] private float typeSpeed = 10;

    public SoundManager soundManager;

    public Queue<string> paragraphs = new Queue<string>();
    private Queue<string> names = new Queue<string>();

    public bool conversationEnded;
    public bool isTyping;
    private string n;
    private string p;
    private bool isInCutscene = false;
    private DialogueText currentCutscene;


    public void Awake()
    {

    }

    public void Update()
    {
        if ((isInCutscene) && Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                // Instantly complete the text
                isTyping = false;
                npcDialogueText.text = p; // Display full text immediately
            }
            else
            {
                displayNextParagraph(currentCutscene);
            }
        }
    }

    public void displayNextParagraph(DialogueText dialogueText)
    {

        if (paragraphs.Count == 0)
        {
            if (!conversationEnded)
            {
                StartConversation(dialogueText);
            }
            else
            {
                EndConversation();
                return;
            }
        }

        n = names.Dequeue();
        p = paragraphs.Dequeue();
        soundManager.PlaySoundClip(3);
        npcNameText.text = n;
        npcDialogueText.text = p;

        StopAllCoroutines(); // Ensure no overlapping coroutines
        StartCoroutine(TypeText(p)); // Start the typewriter effect

        if (paragraphs.Count == 0)
        {
            conversationEnded = true;
        }
    }

    public IEnumerator TypeText(string fullText)
    {
        isTyping = true;

        npcDialogueText.text = ""; // Clear the current text
        foreach (char letter in fullText.ToCharArray())
        {
            if (!isTyping)
            {
                npcDialogueText.text = fullText; // If interrupted, display full text
                break;
            }

            npcDialogueText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(0.035f); // Wait between letters (adjust for speed)
        }
        isTyping = false;
    }

    private void StartConversation(DialogueText dialogueText)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        for (int i = 0; i < dialogueText.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialogueText.paragraphs[i]);
        }
        for (int i = 0; i < dialogueText.speakerNames.Length; i++)
        {
            names.Enqueue(dialogueText.speakerNames[i]);
        }
    }

    public void EndConversation()
    {
        paragraphs.Clear();
        names.Clear();
        conversationEnded = false;
        if (gameObject.activeSelf)
        {
            soundManager.PlaySoundClip(4);
            gameObject.SetActive(false);
        }
        if (isInCutscene)
        {
            isInCutscene = false;
        }
    }

    public void startCutscene(DialogueText text)
    {
        currentCutscene = text;
        displayNextParagraph(currentCutscene);
        isInCutscene = true;
    }
}
