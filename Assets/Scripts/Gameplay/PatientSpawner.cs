using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class PatientSpawner : MonoBehaviour
{
    [SerializeField] PatientController patientPrefab;
    [SerializeField] Transform container;


    public void SpawnNewPatient()
    {
        PatientController pc = Instantiate(patientPrefab, container);
        pc.SetRandomPatientData();
    }

    public void SpawnNewPatient(PatientSO patientData)
    {
        PatientController pc = Instantiate(patientPrefab, container);
        pc.SetPatientData(patientData);
    }
}
