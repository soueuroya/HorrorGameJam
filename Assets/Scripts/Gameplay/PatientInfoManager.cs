using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatientInfoManager : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI nameLabel;
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI ageLabel;
    [SerializeField] TextMeshProUGUI weightLabel;
    [SerializeField] TextMeshProUGUI occupationLabel;
    [SerializeField] TextMeshProUGUI bloodLabel;
    [SerializeField] TextMeshProUGUI symptomsLabel;
    [SerializeField] TMP_Dropdown dropdown;
    int currentLocation;

    public static PatientInfoManager Instance;

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

    public void Show(PatientSO patientData)
    {
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        img.sprite = patientData.torso;
        nameLabel.text = patientData.patientName;
        weightLabel.text = patientData.weight.ToString();
        ageLabel.text = patientData.age.ToString();
        occupationLabel.text = patientData.profession;
        bloodLabel.text = patientData.blood.ToString().Replace("n", "-").Replace("p", "+");
        symptomsLabel.text = patientData.symptoms;
        dropdown.SetValueWithoutNotify(patientData.currentState);
        dropdown.onValueChanged.RemoveAllListeners();
        currentLocation = dropdown.value;
        dropdown.onValueChanged.AddListener((value) => {
            PatientSO patientToSend = patientData;
            if (value != currentLocation)
            {
                SendPatientTo(value, patientToSend);
            }
        });
    }

    public void Hide()
    {
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    private void SendPatientTo(int location, PatientSO patient)
    {
        switch (dropdown.options[location].text)
        {
            case "Waiting Room":
                if (patient.controller.TryGoToWaitingQueue())
                {
                    currentLocation = location;
                    Hide();
                }
                else
                {
                    dropdown.SetValueWithoutNotify(location);
                }
                break;

            case "NeuroScan":
                if (patient.controller.TryToGoExam3())
                {
                    {
                        currentLocation = location;
                        Hide();
                    }
                }
                else
                {
                    dropdown.SetValueWithoutNotify(location);
                }
                break;

            case "OsteoScan":
                if (patient.controller.TryToGoExam1())
                {
                    {
                        currentLocation = location;
                        Hide();
                    }
                }
                else
                {
                    dropdown.SetValueWithoutNotify(location);
                }
                break;

            case "HoloScan":
                if (patient.controller.TryToGoExam2())
                {
                    {
                        currentLocation = location;
                        Hide();
                    }
                }
                else
                {
                    dropdown.SetValueWithoutNotify(location);
                }
                break;

            case "Isolation":
                if (patient.controller.TryToGoIsolation())
                {
                    {
                        currentLocation = location;
                        Hide();
                    }
                }
                else
                {
                    dropdown.SetValueWithoutNotify(location);
                }
                break;

            case "Home":
                if (patient.controller.TryToGoExit())
                {
                    {
                        currentLocation = location;
                        Hide();
                    }
                }
                else
                {
                    dropdown.SetValueWithoutNotify(location);
                }

                break;

            default:
                break;
        }
    }
}