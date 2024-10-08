using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/NewDialogueContainer")]
public class DialogueText : ScriptableObject
{
    public string[] speakerNames;

    [TextArea(5, 10)]
    public string[] paragraphs;
}
