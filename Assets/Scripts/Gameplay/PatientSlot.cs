using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.Events;

public class PatientSlot : MonoBehaviour
{
    public PatientController patient;
    public GameObject target;
    public bool taken = false;

    [SerializeField] UnityEvent<PatientSO> onPatientReached;

    public void Reached(PatientSO patientData)
    {
        onPatientReached?.Invoke(patientData);
    }
}
