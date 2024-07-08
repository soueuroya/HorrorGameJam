using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.Events;

public class PatientSlot : MonoBehaviour
{
    public PatientController patient;
    public bool taken = false;

    [SerializeField] UnityEvent<PatientSO> onPatientReached;
    [SerializeField] UnityEvent<PatientSO> onPatientLeft;

    public void Reached(PatientSO patientData)
    {
        onPatientReached?.Invoke(patientData);
    }

    public void Left(PatientSO patientData)
    {
        onPatientLeft?.Invoke(patientData);
    }
}
