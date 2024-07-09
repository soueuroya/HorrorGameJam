using UnityEngine;

[CreateAssetMenu]
public class CharacterDialogueSO : ScriptableObject
{
    [Header("CharacterID")]
    [Tooltip("tbd")]
    public string characterID;

    /*
    // This is to give more characterization. 
    public int[] pauseBetweenLines;

    public string[] lines;

    */
    public DialogueSegments[] dialogueSegments;
    public CharacterDialogueSO[] characterDialogueSO;  //Optional

    [System.Serializable]
    public struct DialogueSegments
    {
        public string dialogueText;
        public float dialogueDisplayTime;
        public DialogueChoice[] dialogueChoices;
    }

    [System.Serializable]
    public struct DialogueChoice
    {
        public string dialogueChoice;
        public CharacterDialogueSO followUpDialogue;

    }

}
