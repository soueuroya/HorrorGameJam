using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine;

public class WaitingListManager : MonoBehaviour
{
    [SerializeField] Transform arrivedContent;
    [SerializeField] Transform welcomedContent;
    [SerializeField] PatientLine patientLinePrefab;
    [SerializeField] PatientLineSimple patientLineSimplePrefab;

    SerializableDictionaryBase<PatientSO, PatientLine> patientLines = new SerializableDictionaryBase<PatientSO, PatientLine>() {};
    SerializableDictionaryBase<PatientSO, PatientLineSimple> patientSimpleLines = new SerializableDictionaryBase<PatientSO, PatientLineSimple>() { };

    public static WaitingListManager Instance;

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

    public void PatientChanged(PatientSO patient)
    {
        patientLines[patient].UpdatePatientInfo(patient);
    }

    public void PatientSimpleChanged(PatientSO patient)
    {
        patientSimpleLines[patient].UpdatePatientInfo(patient);
    }

    public void PatientArrived(PatientSO patient)
    {
        patientLines.Add(patient, Instantiate(patientLinePrefab, arrivedContent));
        patient.onChanged.AddListener((patient) => {
            PatientChanged(patient); 
        });

        PatientChanged(patient);
    }

    public void PatientWelcomed(PatientSO patient)
    {
        if (patientLines.ContainsKey(patient))
        {
            Destroy(patientLines[patient].gameObject);
            patientLines.Remove(patient);
        }

        //patient.onChanged.RemoveAllListeners();
        patientSimpleLines.Add(patient, Instantiate(patientLineSimplePrefab, welcomedContent));
        patient.onChanged.AddListener((patient) => {
            PatientSimpleChanged(patient);
        });
        PatientSimpleChanged(patient);
    }

    public void PatientLeft(PatientSO patient)
    {
        if(patientLines.ContainsKey(patient))
        {
            Destroy(patientLines[patient]);
            patientLines.Remove(patient);
        }

        if (patientSimpleLines.ContainsKey(patient))
        {
            Destroy(patientSimpleLines[patient]);
            patientSimpleLines.Remove(patient);
        }

        patient.onChanged.RemoveAllListeners();
    }

}