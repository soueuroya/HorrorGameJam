using UnityEngine;

public class Exam4Manager : MonoBehaviour
{
    [SerializeField] RectTransform helmet;
    [SerializeField] GameObject monitor;
    [SerializeField] RectTransform target;

    private float distance;

    public static Exam4Manager Instance;

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
    }

    public void EndReached()
    {
    }


    public void MoveDown()
    {
    }

}