using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevButton : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] KeyCode keyCode;

    void Update()
    {
        if (btn == null)
        {
            return;
        }

        if (Input.GetKeyDown(keyCode))
        {
            btn.Select();
        }
    }
}
