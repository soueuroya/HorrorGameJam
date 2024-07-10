using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Exam3Manager : MonoBehaviour
{
    [SerializeField] Transform arrivedContent;
    [SerializeField] PatientExamLine patientExamLinePrefab;
    SerializableDictionaryBase<PatientSO, PatientExamLine> patientExamLines = new SerializableDictionaryBase<PatientSO, PatientExamLine>() { };

    [SerializeField] RectTransform helmet;
    [SerializeField] GameObject monitor;
    [SerializeField] RectTransform target;
    [SerializeField] Pathogen pathogen;
    [SerializeField] Pathogen pathogen2;
    [SerializeField] TextMeshProUGUI patientName;

    [SerializeField] Image torso;
    [SerializeField] Image visible;
    [SerializeField] Image infected;

    [SerializeField] List<GameObject> infections;

    private Vector2 initialPosition;
    private float distance;
    private bool hasPatient = false;

    public static Exam3Manager Instance;

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
        distance = (target.position - helmet.position).y / 50;
        initialPosition = helmet.localPosition;
    }

    public void ResetExam()
    {
        helmet.localPosition = initialPosition;
        monitor.SetActive(false);
    }


    public void EndReached()
    {
        monitor.SetActive(true);
    }


    public void MoveDown()
    {
        monitor.SetActive(false);
        helmet.Translate(Vector2.up * distance);

        if (helmet.position.y <= target.position.y)
        {
            helmet.position = Vector2.right * helmet.position.x + Vector2.up * target.position.y;
            EndReached();
        }
    }

    public void ButtonDown()
    {
        if (hasPatient)
        {
            InvokeRepeating("MoveDown", 0.1f, 0.1f);
        }
    }

    public void ButtonUp()
    {
        CancelInvoke("MoveDown");
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
        hasPatient = true;
        torso.gameObject.SetActive(true);
        torso.sprite = patient.torso;

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

    public void TurnInfectionsOff()
    {
        for (int i = 0; i < infections.Count; i++)
        {
            infections[i].SetActive(false);
        }
    }

    public void PatientLeft(PatientSO patient)
    {
        hasPatient = false;

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

    private void TurnOffEverything()
    {
        TurnInfectionsOff();

        torso.gameObject.SetActive(false);
        visible.gameObject.SetActive(false);
        infected.gameObject.SetActive(false);
        patientName.text = "";
    }
}