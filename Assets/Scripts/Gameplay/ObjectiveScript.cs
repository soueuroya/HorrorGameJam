using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Objective
{
    [SerializeField] public string objectiveDescription;
    [SerializeField] public UnityEvent onComplete;
}

public class ObjectiveScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objectiveLabel;
    [SerializeField] bool shown = true;

    [SerializeField] List<Objective> objectives;
    [SerializeField] int currentObjective = 0;

    public void NextObjective(int compare)
    {
        if (compare != currentObjective && compare != 7)
        {
            return;
        }
        currentObjective = compare;
        if (currentObjective >= objectives.Count)
        {
            return;
        }
        objectives[currentObjective].onComplete?.Invoke();
        currentObjective++;
        if (currentObjective >= objectives.Count)
        {
            currentObjective--;
            HideObjective();
            return;
        }

        UpdateObjective(objectives[currentObjective].objectiveDescription);
    }

    public void NextObjective()
    {
        if (currentObjective >= objectives.Count)
        {
            return;
        }
        objectives[currentObjective].onComplete?.Invoke();
        currentObjective++;
        if (currentObjective >= objectives.Count)
        {
            return;
        }
        UpdateObjective(objectives[currentObjective].objectiveDescription);
    }

    public void UpdateObjective(string newObjective)
    {
        objectiveLabel.text = newObjective;
    }

    public void HideObjective()
    {
        objectiveLabel.gameObject.SetActive(false);
    }

    public void ShowObjective()
    {
        objectiveLabel.gameObject.SetActive(true);
    }
}
