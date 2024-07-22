using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Exam2Manager : MonoBehaviour
{
    [SerializeField] Transform arrivedContent;
    [SerializeField] PatientExamLine patientExamLinePrefab;
    SerializableDictionaryBase<PatientSO, PatientExamLine> patientExamLines = new SerializableDictionaryBase<PatientSO, PatientExamLine>() { };

    [SerializeField] RectTransform bluryMask;
    [SerializeField] RectTransform skinMask;
    [SerializeField] Transform gameLine;
    //[SerializeField] Vector2 gameLineInitial;
    [SerializeField] float limit;
    [SerializeField] Pathogen pathogen;
    [SerializeField] Pathogen pathogen2;
    [SerializeField] TextMeshProUGUI patientName;
    [SerializeField] Slider lineSlider;

    [SerializeField] Image skin;
    [SerializeField] Image blury;
    [SerializeField] Image visible;
    [SerializeField] Image infected;

    [SerializeField] List<GameObject> infections;
    private float initialPosition;
    private float previousValue = 0;
    private bool blur = false;

    public static Exam2Manager Instance;

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

    private void Start()
    {
        initialPosition = lineSlider.value;
        //gameLineInitial = gameLine.transform.localPosition;
    }
    public void ResetExam()
    {
        lineSlider.value = initialPosition;
        //gameLine.transform.localPosition = gameLineInitial;
    }

    public void PatientExamChanged(PatientSO patient)
    {
        patientExamLines[patient].UpdatePatientInfo(patient);
    }

    public void PatientArrived(PatientSO patient)
    {
        if (!patientExamLines.ContainsKey(patient))
        {
            patientExamLines.Add(patient, Instantiate(patientExamLinePrefab, arrivedContent));
            patient.onChanged.AddListener((patient) =>
            {
                PatientExamChanged(patient);
            });
        }
        else
        {
            patientExamLines[patient].TurnOnFolder();
            patientExamLines[patient].TurnOnToggle();
        }

        patientName.text = patient.patientName;
        skin.gameObject.SetActive(true);
        skin.sprite = patient.skin;
        blury.gameObject.SetActive(true);
        blury.sprite = patient.blury;

        TurnInfectionsOff();
        if (pathogen == patient.pathogen || pathogen2 == patient.pathogen) // if patient is infected show the infected images
        {
            infected.sprite = patient.infected;
            visible.gameObject.SetActive(false);
            infected.gameObject.SetActive(true);
            TurnRandomInfectionsOn(Mathf.RoundToInt(patient.stage * 10));
        }
        else
        {
            visible.sprite = patient.visible;
            visible.gameObject.SetActive(true);
            infected.gameObject.SetActive(false);
        }

        PatientExamChanged(patient);
    }

    public void TurnInfectionsOff()
    {
        for (int i = 0; i < infections.Count; i++)
        {
            infections[i].SetActive(false);
        }
    }

    public void PatientLeft(PatientSO patient)
    {
        if (patientExamLines.ContainsKey(patient))
        {
            patientExamLines[patient].TurnOffFolder();
            patientExamLines[patient].TurnOffToggle();
            //Destroy(patientExamLines[patient]);
            //patientExamLines.Remove(patient);
        }

        TurnOffEverything();

        patient.onChanged.RemoveAllListeners();
    }

    public void OnSliderChange(float value)
    {
        if (value <= 0.1f)
        {
            blur = false;
            bluryMask.localScale = Vector2.right + Vector2.up * 0;
        }
        else
        if (value - previousValue > limit)
        {
            blur = true;
        }

        skinMask.localScale = Vector2.right + Vector2.up * value;
        //gameLine.localPosition = gameLineInitial + Vector2.up * value;

        if (blur)
        {
            bluryMask.localScale = Vector2.right + Vector2.up * value;
        }

        previousValue = value;
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
        blury.gameObject.SetActive(false);
        visible.gameObject.SetActive(false);
        infected.gameObject.SetActive(false);
        patientName.text = "";
    }
}

