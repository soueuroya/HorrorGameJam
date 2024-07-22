using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class PatientSpawner : MonoBehaviour
{
    [SerializeField] PatientController patientPrefab;
    [SerializeField] Transform container;
    [SerializeField] int waitBetween;
    [SerializeField] bool repeating = false;

    public void StartSpawning()
    {
        repeating = true;
        SpawnNewPatient();
    }

    public void StopSpawning()
    {
        repeating = false;
    }


    public void SpawnNewPatient()
    {
        PatientController pc = Instantiate(patientPrefab, container);
        pc.transform.position = transform.position;
        pc.SetRandomPatientData();
        if (pc.TryGoToWaitingQueue())
        {

        }
        else
        {
            if (pc.patientData.pathogen == Pathogen.Cerebrognatha || pc.patientData.pathogen == Pathogen.CerebrognathaAS ||
                pc.patientData.pathogen == Pathogen.Pulmospora || pc.patientData.pathogen == Pathogen.PulmosporaAS ||
                pc.patientData.pathogen == Pathogen.Xenostroma || pc.patientData.pathogen == Pathogen.XenostromaAS)
            {
                GameEventManager.OnInfectedIgnored();
            }
            else
            {
                GameEventManager.OnNormalIgnored();
            }
            Destroy(pc.gameObject);
        }

        if (repeating)
        {
            Invoke("SpawnNewPatient", waitBetween);
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
        else
        {
            if (pc.patientData.pathogen == Pathogen.Cerebrognatha || pc.patientData.pathogen == Pathogen.CerebrognathaAS ||
                pc.patientData.pathogen == Pathogen.Pulmospora || pc.patientData.pathogen == Pathogen.PulmosporaAS ||
                pc.patientData.pathogen == Pathogen.Xenostroma || pc.patientData.pathogen == Pathogen.XenostromaAS)
            {
                GameEventManager.OnInfectedIgnored();
            }
            else
            {
                GameEventManager.OnNormalIgnored();
            }
            Destroy(pc.gameObject);
        }
    }
}
