using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class QueueSystem : MonoBehaviour
{
    public List<PatientSlot> waitingRoomSlots;
    public List<PatientSlot> exam1Slots;
    public List<PatientSlot> exam2Slots;
    public List<PatientSlot> exam3Slots;
    public List<PatientSlot> exam4Slots;
    public List<PatientSlot> isolationSlots;
    public List<PatientSlot> exitSlots;

    public static QueueSystem Instance;

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

    public PatientSlot GetEmptyWaitingRoomSlot(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (item.taken)
            {
                continue;
            }

            item.patient = _patient;
            item.taken = true;
            return item;
        }

        return null;
    }

    public PatientSlot GetEmptyExam1Slot(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.exam1Slots)
        {
            if (item.taken)
            {
                continue;
            }

            item.patient = _patient;
            item.taken = true;
            return item;
        }

        return null;
    }

    public PatientSlot GetEmptyExam2Slot(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.exam2Slots)
        {
            if (item.taken)
            {
                continue;
            }

            item.patient = _patient;
            item.taken = true;
            return item;
        }

        return null;
    }

    public PatientSlot GetEmptyExam3Slot(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.exam3Slots)
        {
            if (item.taken)
            {
                continue;
            }

            item.patient = _patient;
            item.taken = true;
            return item;
        }

        return null;
    }

    public PatientSlot GetEmptyExam4Slot(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.exam4Slots)
        {
            if (item.taken)
            {
                continue;
            }

            item.patient = _patient;
            item.taken = true;
            return item;
        }

        return null;
    }

    public PatientSlot GetEmptyIsolationSlot(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.isolationSlots)
        {
            //if (item.taken)
            //{
            //    continue;
            //}

            item.patient = _patient;
            //item.taken = true;
            return item;
        }

        return null;
    }

    public PatientSlot GetEmptyExitSlot(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.exitSlots)
        {
            //if (item.taken) // Exit can be used by as many patients
            //{
            //    continue;
            //}

            //item.patient = _patient;
            //item.taken = true;
            return item;
        }

        return null;
    }
    
    /*
    public void SendPatientToExam1(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoExam1())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }
    public void SendPatientToExam2(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoExam2())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }
    public void SendPatientToExam3(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoExam3())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }
    public void SendPatientToExam4(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoExam4())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }
    public void SendPatientToIsolation(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoIsolation())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }
    public void SendPatientToExit(PatientController _patient)
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoExit())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }
    
    
    ////// testing functions
    public void SendPatientToExam1()
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoExam1())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }

    public void SendPatientToExam2()
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoExam2())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }
    public void SendPatientToExam3()
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoExam3())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }
    public void SendPatientToExam4()
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoExam4())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }
    public void SendPatientToIsolation()
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoIsolation())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }
    public void SendPatientToExit()
    {
        foreach (var item in QueueSystem.Instance.waitingRoomSlots)
        {
            if (!item.taken)
            {
                continue;
            }

            if (item.patient.TryToGoExit())
            {
                item.taken = false;
                item.patient = null;
            }

            break;
        }
    }
    */
}
