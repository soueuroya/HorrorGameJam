using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatientLineSimple : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameLabel;
    [SerializeField] Toggle toggle;
    [SerializeField] Button folder;

    public void UpdatePatientInfo(PatientSO patient)
    {
        nameLabel.text = patient.patientName;
        if (toggle != null)
        {
            toggle.isOn = patient.inIso;
        }
        if (folder != null)
        {
            folder.onClick.RemoveAllListeners();
            folder.onClick.AddListener(() =>
            {
                PatientInfoManager.Instance.Show(patient);
            });
        }
    }
}
