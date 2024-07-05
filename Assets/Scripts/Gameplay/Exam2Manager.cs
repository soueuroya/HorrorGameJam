using UnityEngine;

public class Exam2Manager : MonoBehaviour
{
    [SerializeField] RectTransform bluryMask;
    [SerializeField] RectTransform skinMask;
    [SerializeField] float limit;

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

        if (blur)
        {
            bluryMask.localScale = Vector2.right + Vector2.up * value;
        }

        previousValue = value;
    }

}