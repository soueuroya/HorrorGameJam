using UnityEngine;

[CreateAssetMenu]
public class PatientSO : ScriptableObject
{
    [Header("PATIENT GENERAL INFO")]
    [Tooltip("These can be generally random")]
    public string name;
    public int age;
    public float weight;
    public string sex;
    public string reason;

    [Header("PATIENT INPORTANT INFO")]
    [Tooltip("These are important info for the game")]
    public string profession;
    public string blood;
    public string pathogen;
    public string symptom;
    public float stage;
    public Sprite picture;


}