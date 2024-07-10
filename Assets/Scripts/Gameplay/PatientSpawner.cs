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

    public int missedPatients = 0;
    public int missedInfected = 0;

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
        else
        {
            missedPatients++;
            if (pc.patientData.pathogen == Pathogen.Cerebrognatha || pc.patientData.pathogen == Pathogen.CerebrognathaAS ||
                pc.patientData.pathogen == Pathogen.Pulmospora || pc.patientData.pathogen == Pathogen.PulmosporaAS ||
                pc.patientData.pathogen == Pathogen.Xenostroma || pc.patientData.pathogen == Pathogen.XenostromaAS)
            {
                missedInfected++;
            }
            Destroy(pc.gameObject);
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
            missedPatients++;
            if (pc.patientData.pathogen == Pathogen.Cerebrognatha || pc.patientData.pathogen == Pathogen.CerebrognathaAS ||
                pc.patientData.pathogen == Pathogen.Pulmospora || pc.patientData.pathogen == Pathogen.PulmosporaAS ||
                pc.patientData.pathogen == Pathogen.Xenostroma || pc.patientData.pathogen == Pathogen.XenostromaAS)
            {
                missedInfected++;
            }
            Destroy(pc.gameObject);
        }
    }
}
