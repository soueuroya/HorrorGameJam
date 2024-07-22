using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IsolationManager : MonoBehaviour
{
    [SerializeField] Transform arrivedContent;
    [SerializeField] PatientExamLine patientExamLinePrefab;
    SerializableDictionaryBase<PatientSO, PatientExamLine> patientExamLines = new SerializableDictionaryBase<PatientSO, PatientExamLine>() { };
    public static IsolationManager Instance;

    #region Initialization
    // Singleton awake
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
    #endregion

    public void PatientExamChanged(PatientSO patient)
    {
        patientExamLines[patient].UpdatePatientInfo(patient);
    }

    public void PatientArrived(PatientSO patient)
    {
        if (!patientExamLines.ContainsKey(patient))
        {
            patientExamLines.Add(patient, Instantiate(patientExamLinePrefab, arrivedContent));
            patient.onChanged.AddListener((patient) =>
            {
                PatientExamChanged(patient);
            });
        }
        else
        {
            patientExamLines[patient].TurnOnFolder();
            patientExamLines[patient].TurnOnToggle();
        }

        PatientExamChanged(patient);
    }

    public void PatientLeft(PatientSO patient)
    {
        if (patientExamLines.ContainsKey(patient))
        {
            patientExamLines[patient].TurnOffFolder();
            patientExamLines[patient].TurnOffToggle();
            //Destroy(patientExamLines[patient]);
            //patientExamLines.Remove(patient);
        }

        patient.onChanged.RemoveAllListeners();
    }
}

