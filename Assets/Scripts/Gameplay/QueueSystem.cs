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
    public PatientSlot outsideSlot;

}
