using UnityEngine;

[CreateAssetMenu]
public class PatientSO : ScriptableObject
{
    [Header("PATIENT GENERAL INFO")]
    [Tooltip("These can be generally random")]
    public string patientName;
    public int age;
    public float weight;

    [Header("PATIENT INPORTANT INFO")]
    [Tooltip("These are important info for the game")]
    public string profession;
    public Blood blood;
    public string symptoms;
    public Pathogen pathogen;
    public float stage;
    public Sprite skin;
    public Sprite torso;


}