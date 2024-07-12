using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioInstancer : Interactable
{
    public GameObject RadioPrefab;
    private GameObject radioObj;

    public void InstanceRadio()
    {
        radioObj = Instantiate(RadioPrefab);
        radioObj.transform.SetParent(GameObject.Find("GameCanvas").transform, false);
    }

    public void StartRadio()
    {
        radioObj.GetComponent<DialogueInteract>().StartDialogue();
    }


    public void DestroyRadio()
    {
        Destroy(radioObj);
    }

    public void SetRadioPrefab(GameObject radioPrefab)
    {
        RadioPrefab = radioPrefab;
    }
}
