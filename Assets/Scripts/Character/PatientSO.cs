using TarodevController;
using UnityEngine;
using UnityEngine.Events;

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
    public Sprite side;
    public Sprite skin;
    public Sprite blury;
    public Sprite visible;
    public Sprite infected;
    public Sprite torso;
    public bool inIso = false;
    public bool isMale = false;
    public bool isOld = false;
    public PatientController controller;
    public int currentState = 0;
    public Animator animator;

    public UnityEvent<PatientSO> onChanged = new UnityEvent<PatientSO>();

    public void Change(PatientSO _newData)
    {
        blood = _newData.blood;
        pathogen = _newData.pathogen;
        stage = _newData.stage;

        if (_newData.controller != null)
        {
            controller = _newData.controller;
        }

        if (!string.IsNullOrEmpty(_newData.patientName))
        {
            patientName = _newData.patientName;
        }

        if (_newData.age > 0)
        {
            age = _newData.age;
        }

        if (_newData.weight > 0)
        {
            weight = _newData.weight;
        }

        if (!string.IsNullOrEmpty(_newData.profession))
        {
            profession = _newData.profession;
        }

        if (!string.IsNullOrEmpty(_newData.symptoms))
        {
            symptoms = _newData.symptoms;
        }

        if (_newData.skin != null)
        {
            skin = _newData.skin;
        }

        if (_newData.torso != null)
        { 
            torso = _newData.torso;
        }

        onChanged?.Invoke(this);
    }
}