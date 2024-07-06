using UnityEngine;

public class Exam3Manager : MonoBehaviour
{
    [SerializeField] RectTransform helmet;
    [SerializeField] GameObject monitor;
    [SerializeField] RectTransform target;
    [SerializeField] Pathogen pathogen;

    private Vector2 initialPosition;
    private float distance;

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
            helmet.position = target.position;
            EndReached();
        }
    }

    public void ButtonDown()
    {
        InvokeRepeating("MoveDown", 0.1f, 0.1f);
    }

    public void ButtonUp()
    {
        CancelInvoke("MoveDown");
    }

}