using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    public Text nameText;
    public Text dialogueText;

    private Queue<string> sentences;
    private string stringHolder;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue( Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }
        stringHolder = sentences.Dequeue();

        StopCoroutine(TypeSentence(stringHolder));
        StartCoroutine(TypeSentence(stringHolder));
    }

    IEnumerator TypeSentence( string sentence)
    {
        dialogueText.text = string.Empty;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    private void EndDialogue()
    {
        Debug.Log("End of Conversation");
    }
}
