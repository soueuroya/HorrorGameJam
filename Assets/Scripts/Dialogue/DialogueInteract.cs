using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueInteract : MonoBehaviour
{
    [SerializeField] CharacterDialogueSO startCharacterDialogueSO;
    [SerializeField] GameObject dialogueButtonParent;
    [SerializeField] GameObject characterDialogueTextbox;
    [SerializeField] GameObject dialogueButtonPrefab;

    bool optionSelected = false;


    private void Awake()
    {
        dialogueButtonParent = GameObject.Find("PlayerResponses");
    }
    private void Start()
    {
        SetCharacterText("");
    }

    public void SetCharacterText(string myText)
    {
        characterDialogueTextbox.GetComponent<TMP_Text>().text = myText;
    }

    //For loopable Dialogue
    public void StartDialogue()
    {
        StartCoroutine(DisplayDialogue(startCharacterDialogueSO));
    }

    public void StartDialogue(CharacterDialogueSO _characterDialogueSO)
    {
        StartCoroutine(DisplayDialogue(_characterDialogueSO));
    }

    public void OptionSelected(CharacterDialogueSO selectedOption)
    {
        optionSelected = true;
        StartDialogue(selectedOption);
    }

    IEnumerator DisplayDialogue(CharacterDialogueSO _characterDialogueSO)
    {
        yield return null;

        List<GameObject> spawnedButtons = new List<GameObject>();

        foreach (var dialogue in _characterDialogueSO.dialogueSegments)
        {
            SetCharacterText(dialogue.dialogueText);


            if (dialogue.dialogueChoices.Length == 0)
            {
                yield return new WaitForSeconds(dialogue.dialogueDisplayTime);
            }
            else
            {
                dialogueButtonParent.SetActive(true);
                foreach (var option in dialogue.dialogueChoices)
                {
                    GameObject newButton = Instantiate(dialogueButtonPrefab, dialogueButtonParent.transform);
                    spawnedButtons.Add(newButton);
                    newButton.GetComponent<UIDialogueOption>().Setup(this, option.followUpDialogue, option.dialogueChoice);
                }

                while (!optionSelected)
                {
                    yield return null;
                }
                break;
            }
        }
        dialogueButtonParent.SetActive(false);
        optionSelected = false;
        spawnedButtons.ForEach(x => Destroy(x));
        GameEventManager.OnMovementUnlocked();
    }
}
