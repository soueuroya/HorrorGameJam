using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PatientExamLine : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameLabel;

    public void UpdatePatientInfo(PatientSO patient)
    {
        nameLabel.text = patient.patientName;
    }
}
