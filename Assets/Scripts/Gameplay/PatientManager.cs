using RotaryHeart.Lib.SerializableDictionary;
using System.Collections.Generic;
using UnityEngine;
public enum Pathogen { Normal, Xenostroma, Cerebrognatha, Pulmospora, XenostromaFP, CerebrognathaFP, PulmosporaFP, XenostromaAS, CerebrognathaAS, PulmosporaAS }
public enum Blood { Ap, Bp, An, Bn, ABp, ABn, Op, On  }
public class PatientManager : MonoBehaviour
{
    [SerializeField] public List<PatientImages> malePatientImages;
    [SerializeField] public List<PatientImages> femalePatientImages;
    [SerializeField] public List<PatientInfections> patientInfections;
    [SerializeField] SerializableDictionaryBase<Pathogen, string> symptoms = new SerializableDictionaryBase<Pathogen, string>() {
        // Normal patients
        { Pathogen.Normal, "Normal" },

        // Sick patients with symptoms
        { Pathogen.Xenostroma, "Shaking, twitching." },
        { Pathogen.Pulmospora, "Chronic cough." },
        { Pathogen.Cerebrognatha, "Severe headaches, and nausea" },

        // Non-sick patients with symptoms
        { Pathogen.XenostromaFP, "Shaking, twitching." },
        { Pathogen.PulmosporaFP, "Chronic cough." },
        { Pathogen.CerebrognathaFP, "Severe headaches, and nausea" },

        // Sick patients without symptoms
        { Pathogen.XenostromaAS, "Normal" },
        { Pathogen.PulmosporaAS, "Normal" },
        { Pathogen.CerebrognathaAS, "Normal" }
    };

    public static PatientManager Instance;
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

    public string GetSymptoms(Pathogen pathogen)
    {
        return symptoms[pathogen];
    }

    public PatientImages GetPatientImages(bool isMale)
    {
        if (isMale)
        {
            return malePatientImages[Random.Range(0, malePatientImages.Count)];
        }
        else
        {
            return malePatientImages[Random.Range(0, femalePatientImages.Count)];
        }
    }
}

public class PatientImages
{
    [SerializeField] public Sprite skin;
    [SerializeField] public Sprite torso;
}

public class PatientInfections
{
    [SerializeField] public Pathogen pathogen;
    [SerializeField] public string symptoms;
    [SerializeField] public int skeletonInfection;
    [SerializeField] public int organInfection;
}