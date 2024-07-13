using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public void ActivateRadio()
    {
        EventManager.OnGamePause();
        Debug.Log("Radio Triggered");

    }
}
