using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDialogueOption : MonoBehaviour
{
    DialogueInteract dialogueInteract;
    CharacterDialogueSO characterDialogueSO;

    //[SerializeField] Text dialogueText;

    public void Setup(DialogueInteract _dialogueInteract, CharacterDialogueSO _characterDialogueSO, string _dialogueText)
    {
        dialogueInteract = _dialogueInteract;
        characterDialogueSO = _characterDialogueSO;
        this.GetComponent<Transform>().GetChild(0).GetComponent<TMP_Text>().text= _dialogueText;
    }

    public void SelectOption()
    {
        dialogueInteract.OptionSelected(characterDialogueSO);
    }
}
