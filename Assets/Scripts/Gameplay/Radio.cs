using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] List<CharacterDialogueSO> dialogues;
    [SerializeField] int currentDialogue = 0;
    [SerializeField] int currentMessage = 0;
    [SerializeField] TextMeshProUGUI label;

    public void OpenRadio()
    {
        EventManager.OnGamePause();
        
    }

    public void CloseRadio()
    {
        EventManager.OnGamePause();

    }

    public void NextMessage()
    {
        if (dialogues.Count > currentDialogue)
        {
            currentMessage++;
            if (currentMessage >= dialogues[currentDialogue].dialogueSegments.Length)
            {
                currentMessage = 0;
            }

            label.gameObject.SetActive(true);
            label.text = dialogues[currentDialogue].dialogueSegments[currentMessage].dialogueText;
            CancelInvoke("HideLabel");
            Invoke("HideLabel", 3);
        }
    }

    public void HideLabel()
    {
        label.gameObject.SetActive(false);
    }

    public void SetCurrentDialogue(int _dialogue)
    {
        if (dialogues.Count > _dialogue)
        {
            currentDialogue = _dialogue;
        }
    }
}
