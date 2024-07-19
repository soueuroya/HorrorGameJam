using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class CinematicsSegment
{
    [SerializeField] public string title;
    [SerializeField] public string subtitle;
    [SerializeField] public string playerDialogue;
    [SerializeField] public bool allowGameplay = false;
    [SerializeField] public bool faded = false;
    [SerializeField] public bool requireInput = false;
    [SerializeField] public float requireDelay = 0.0f;
    [SerializeField] public float nextDelay = 0.0f;
    [SerializeField] public AudioClip audio;
    [SerializeField] public bool hasMusic;
}

public class CinematicsScript : MonoBehaviour
{
    [SerializeField] AudioSource audioS;
    [SerializeField] List<CinematicsSegment> segments = new List<CinematicsSegment>();
    [SerializeField] int currentSegment;
    [SerializeField] CanvasGroup fadeGroup;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI subtitle;
    [SerializeField] bool requiringInput = false;
    [SerializeField] GameObject inputRequirement;
    [SerializeField] GameObject tires;

    private void Awake()
    {
        fadeGroup.alpha = 1;
    }

    private void Start()
    {
        MusicManager.Instance.StopMusic();
        Invoke("NextSegment", 2);
    }

    private void Update()
    {
        if (requiringInput)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                requiringInput = false;
                NextSegment();
            }
        }
    }

    public void NextSegment()
    {
        inputRequirement.SetActive(false);
        if (segments.Count <= currentSegment)
        {
            // stop it here
        }
        else
        {
            ProcessSegment(segments[currentSegment]);
            currentSegment++;
        }
    }

    public void ProcessSegment(CinematicsSegment segment)
    {
        if (!string.IsNullOrEmpty(segment.title))
        {
            title.text = segment.title;
        }
        else
        {
            title.text = "";
        }

        if (!string.IsNullOrEmpty(segment.subtitle))
        {
            subtitle.text = segment.subtitle;
        }
        else
        {
            subtitle.text = "";
        }

        if (!string.IsNullOrEmpty(segment.playerDialogue))
        {

        }

        if (segment.allowGameplay)
        {
            tires.SetActive(false);
            GameEventManager.OnMovementUnlocked();
        }
        else
        {
            tires.SetActive(true);
            GameEventManager.OnMovementLocked();
        }

        if (segment.faded)
        {
            if (fadeGroup.alpha != 1)
            {
                Show();
            }
        }
        else
        {
            if (fadeGroup.alpha != 0)
            {
                Hide();
            }
        }

        if (segment.audio != null)
        {
            audioS.PlayOneShot(segment.audio);
        }

        if (!segment.requireInput)
        {
            inputRequirement.SetActive(false);
            Invoke("NextSegment", segment.nextDelay);
        }
        else
        {
            Invoke("RequireInput", segment.requireDelay);
        }

        if (segment.hasMusic && !MusicManager.Instance.isPlaying)
        {
            MusicManager.Instance.Resume();
            MusicManager.Instance.FadeMusicTo(3, 1);
        }
        //else if (MusicManager.Instance.isPlaying)
        //{
        //    MusicManager.Instance.FadeMusicTo(1, 0, () => {
        //        MusicManager.Instance.StopMusic();
        //    });
        //}
    }

    public void RequireInput()
    {
        inputRequirement.SetActive(true);
        requiringInput = true;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);

        LeanTween.value(fadeGroup.alpha, to: 1, time: 3)
        .setEaseInOutSine()
        .setOnUpdate((float val) =>
        {
            fadeGroup.alpha = val;
        }).setOnComplete(() => {
            fadeGroup.alpha = 1;
        });
    }

    public virtual void Hide()
    {
        LeanTween.value(fadeGroup.alpha, to: 0, time: 3)
        .setEaseInOutSine()
        .setOnUpdate((float val) =>
        {
            fadeGroup.alpha = val;
        }).setOnComplete(() => {
            fadeGroup.alpha = 0;
        });
    }
}
