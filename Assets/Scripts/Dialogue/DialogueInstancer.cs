using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInstancer : Interactable
{
    public GameObject DialoguePrefab;
    private GameObject dialogueObj;

    public void InstanceDialogue()
    {
        dialogueObj = Instantiate(DialoguePrefab);
        dialogueObj.transform.SetParent(this.transform, false);
    }

    public void StartDialogue()
    {
        dialogueObj.GetComponent<DialogueInteract>().StartDialogue();
    }


    public void DestroyDialogue()
    {
        Destroy(dialogueObj);
    }

    public void SetDialoguePrefab(GameObject radioPrefab)
    {
        DialoguePrefab = radioPrefab;
    }
}
