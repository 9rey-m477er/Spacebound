using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private TextMeshProUGUI npcDialogueText;
    //[SerializeField] private float typeSpeed = 10;

    private SoundManager soundManager;

    public Queue<string> paragraphs = new Queue<string>();
    private Queue<string> names = new Queue<string>();

    public bool conversationEnded;
    //private bool isTyping;
    private string n;
    private string p;

    //private Coroutine typeDialogueCoroutine;

    //private const string htmlAlpha = "<color=#00000000>";
    //private const float maxTypeTime = 0.1f;

    public void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
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

        //if (!isTyping)
        //{
        //    p = paragraphs.Dequeue();
        //    typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        //}


        n = names.Dequeue();
        p = paragraphs.Dequeue();
        soundManager.PlaySoundClip(3);
        npcNameText.text = n;
        npcDialogueText.text = p;

        if (paragraphs.Count == 0)
        {
            conversationEnded = true;
        }
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
    }

    //private IEnumerator TypeDialogueText(string p)
    //{
    //    isTyping = true;

    //    npcDialogueText.text = "";

    //    string originalText = p;
    //    string displayedText = "";

    //    int alphaIndex = 0;

    //    foreach (char c in p.ToCharArray())
    //    {
    //        alphaIndex++;
    //        npcDialogueText.text = originalText;

    //        displayedText = npcDialogueText.text.Insert(alphaIndex, htmlAlpha);
    //        npcDialogueText.text = displayedText;
    //        yield return new WaitForSeconds(maxTypeTime / typeSpeed);
    //    }

    //    isTyping = false;
    //}
}
