using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI objectiveLabel;
    [SerializeField] bool shown = true;

    [SerializeField] List<string> objectives;
    [SerializeField] int currentObjective = 0;

    public void NextObjective(int compare)
    {
        if (compare != currentObjective)
        {
            return;
        }

        currentObjective++;
        if (currentObjective >= objectives.Count)
        {
            currentObjective--;
            HideObjective();
            return;
        }

        UpdateObjective(objectives[currentObjective]);
    }

    public void NextObjective()
    {
        currentObjective++;
        if (currentObjective >= objectives.Count)
        {
            return;
        }

        UpdateObjective(objectives[currentObjective]);
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
