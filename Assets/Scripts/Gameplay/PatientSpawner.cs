using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class PatientSpawner : MonoBehaviour
{
    [SerializeField] PatientController patientPrefab;
    [SerializeField] Transform container;
    [SerializeField] int waitBeforeStart;
    [SerializeField] int waitBetween;

    private void Update()
    {
        
    }


    public void SpawnNewPatient()
    {
        PatientController pc = Instantiate(patientPrefab, container);
        pc.transform.position = transform.position;
        pc.SetRandomPatientData();
        if (pc.TryGoToWaitingQueue())
        {
        }
    }

    public void SpawnNewPatient(PatientSO patientData)
    {
        PatientController pc = Instantiate(patientPrefab, container);
        pc.transform.position = transform.position;
        pc.SetPatientData(patientData);
        if (pc.TryGoToWaitingQueue())
        {

        }
    }
}
