using UnityEngine;

public class Exam1Manager : MonoBehaviour
{
    [SerializeField] RectTransform xray;
    [SerializeField] Animator anim;
    [SerializeField] GameObject masks;

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
    #endregion


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