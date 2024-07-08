using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    public static ScreensManager Instance;

    [SerializeField] Exam1Manager exam1;
    [SerializeField] Exam2Manager exam2;
    [SerializeField] Exam3Manager exam3;
    [SerializeField] Exam3Manager exam4;
    [SerializeField] WaitingListManager waitingList;
    [SerializeField] StaffManagementManager staffManagement;
    [SerializeField] PatientInfoManager patientInfo;


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

    public void TurnOnExam1()
    {
        exam1.gameObject.SetActive(true);
    }

    public void TurnOffExam1()
    {
        exam1.gameObject.SetActive(false);
        PatientInfoManager.Instance.Hide();
    }

    public void TurnOnExam2()
    {
        exam2.gameObject.SetActive(true);
    }

    public void TurnOffExam2()
    {
        exam2.gameObject.SetActive(false);
        PatientInfoManager.Instance.Hide();
    }

    public void TurnOnExam3()
    {
        exam3.gameObject.SetActive(true);
        exam3.ResetExam();
    }

    public void TurnOffExam3()
    {
        exam3.gameObject.SetActive(false);
        PatientInfoManager.Instance.Hide();
    }

    public void TurnOnExam4()
    {
        exam4.gameObject.SetActive(true);
    }

    public void TurnOffExam4()
    {
        exam4.gameObject.SetActive(false);
        PatientInfoManager.Instance.Hide();
    }

    public void TurnOnWaitingList()
    {
        waitingList.gameObject.SetActive(true);
    }

    public void TurnOffWaitingList()
    {
        waitingList.gameObject.SetActive(false);
        PatientInfoManager.Instance.Hide();
    }

    public void TurnOnStaffManagement()
    {
        staffManagement.gameObject.SetActive(true);
    }

    public void TurnOffStaffManagement()
    {
        staffManagement.gameObject.SetActive(false);
        PatientInfoManager.Instance.Hide();
    }

    public void TurnOnPatienfInfo()
    {
        patientInfo.gameObject.SetActive(true);
    }

    public void TurnOffPatientInfo()
    {
        patientInfo.gameObject.SetActive(false);
        PatientInfoManager.Instance.Hide();
    }
}
