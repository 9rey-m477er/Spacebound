using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI npcNameText;
    [SerializeField] private TextMeshProUGUI npcDialogueText;
    //[SerializeField] private float typeSpeed = 10;

    private Queue<string> paragraphs = new Queue<string>();

    private bool conversationEnded;
    //private bool isTyping;
    private string p;

    //private Coroutine typeDialogueCoroutine;

    //private const string htmlAlpha = "<color=#00000000>";
    //private const float maxTypeTime = 0.1f;

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


        p = paragraphs.Dequeue();
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

        npcNameText.text = dialogueText.name;

        for (int i = 0; i < dialogueText.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialogueText.paragraphs[i]);
        }
    }

    private void EndConversation()
    {
        paragraphs.Clear();
        conversationEnded = false;
        if (gameObject.activeSelf)
        {
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
