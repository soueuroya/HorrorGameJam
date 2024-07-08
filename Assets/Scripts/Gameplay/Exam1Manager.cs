using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Exam1Manager : MonoBehaviour
{
    [SerializeField] Transform arrivedContent;
    [SerializeField] PatientExamLine patientExamLinePrefab;
    SerializableDictionaryBase<PatientSO, PatientExamLine> patientExamLines = new SerializableDictionaryBase<PatientSO, PatientExamLine>() { };

    [SerializeField] RectTransform xray;
    [SerializeField] Animator anim;
    [SerializeField] GameObject masks;
    [SerializeField] Pathogen pathogen;
    [SerializeField] Pathogen pathogen2;
    [SerializeField] TextMeshProUGUI patientName;

    [SerializeField] Image skin;
    [SerializeField] Image visible;
    [SerializeField] Image infected;

    [SerializeField] List<GameObject> infections;
    private Vector2 initialPosition;
    public static Exam1Manager Instance;
    
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
    private void Start()
    {
        initialPosition = xray.position;
    }

    #endregion
    public void ResetExam()
    {
        xray.position = initialPosition;
        masks.SetActive(false);
    }

    public void PatientExamChanged(PatientSO patient)
    {
        patientExamLines[patient].UpdatePatientInfo(patient);
    }

    public void TurnInfectionsOff()
    {
        for (int i = 0; i < infections.Count; i++)
        {
            infections[i].SetActive(false);
        }
    }

    private void TurnRandomInfectionsOn(int quantity)
    {
        if (infections == null || infections.Count == 0)
        {
            Debug.LogWarning("No infections available.");
            return;
        }

        if (quantity > infections.Count)
        {
            Debug.LogWarning("Requested quantity exceeds the number of available infections.");
            quantity = infections.Count;
        }

        // Shuffle the list
        infections.Shuffle();

        // Turn on the specified quantity of infections
        for (int i = 0; i < quantity; i++)
        {
            infections[i].SetActive(true);
        }
    }

    private void TurnOffEverything()
    {
        TurnInfectionsOff();

        skin.gameObject.SetActive(false);
        visible.gameObject.SetActive(false);
        infected.gameObject.SetActive(false);
        patientName.text = "";
    }

public void PatientArrived(PatientSO patient)
    {
        patientExamLines.Add(patient, Instantiate(patientExamLinePrefab, arrivedContent));
        patient.onChanged.AddListener((patient) => {
            PatientExamChanged(patient);
        });

        patientName.text = patient.patientName;
        skin.gameObject.SetActive(true);
        skin.sprite = patient.skin;

        TurnInfectionsOff();
        if (pathogen == patient.pathogen || pathogen2 == patient.pathogen) // if patient is infected show the infected images
        {
            visible.gameObject.SetActive(false);
            infected.gameObject.SetActive(true);
            TurnRandomInfectionsOn(10);
        }
        else
        {
            visible.gameObject.SetActive(true);
            infected.gameObject.SetActive(false);
        }

        PatientExamChanged(patient);
    }

    public void PatientLeft(PatientSO patient)
    {
        if (patientExamLines.ContainsKey(patient))
        {
            Destroy(patientExamLines[patient]);
            patientExamLines.Remove(patient);
        }

        TurnOffEverything();

        patient.onChanged.RemoveAllListeners();
    }

    public void OnSliderXChanged(float value)
    {
        masks.SetActive(false);
        xray.localPosition = xray.localPosition.y * Vector2.up + (((value - 0.5f) * 110 ) * Vector2.right);
    }

    public void OnSliderYChanged(float value)
    {
        masks.SetActive(false);
        xray.localPosition = xray.localPosition.x * Vector2.right + (((value - 0.5f) * -450 ) * Vector2.up);
    }

    public void ButtonPressed()
    {
        masks.SetActive(true);
        anim.SetTrigger("Flash");
    }
}