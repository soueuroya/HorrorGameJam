using System;
using UnityEngine;

public class GameManager : BaseMenu
{
    private bool sceneLoadListenerAdded = false;
    private int infectedHome = 0;
    private int infectedIsolated = 0;
    private int infectedNotWelcomed = 0;
    private int normalNotWelcomed = 0;
    private int normalIsolated = 0;
    private int normalHome = 0;

    private void OnEnable()
    {
        InstantHide();

        if (LoadingScreen.Instance.IsOn)
        {
            EventManager.SceneLoad += UpdateContent;
            EventManager.EventManagerLoaded += ShowMenu;
            sceneLoadListenerAdded = true;
        }
        else
        {
            EventManager.EventManagerLoaded += UpdateContent;
            sceneLoadListenerAdded = false;
        }

        GameEventManager.NormalHome += IncreaseNormalHome;
        GameEventManager.NormalIsolation += IncreaseNormalIsolated;
        GameEventManager.NormalIgnored += IncreaseNormalNotAnswered;
        GameEventManager.InfectedHome += IncreaseInfectedHome;
        GameEventManager.InfectedIsolation += IncreaseInfectedIsolated;
        GameEventManager.InfectedIgnored += IncreaseInfectedNotAnswered;
    }

    private void OnDisable()
    {
        if (sceneLoadListenerAdded)
        {
            EventManager.SceneLoad -= UpdateContent;
            EventManager.EventManagerLoaded -= ShowMenu;
        }
        else
        {
            EventManager.EventManagerLoaded -= UpdateContent;
        }

        GameEventManager.NormalHome -= IncreaseNormalHome;
        GameEventManager.NormalIsolation -= IncreaseNormalIsolated;
        GameEventManager.NormalIgnored -= IncreaseNormalNotAnswered;
        GameEventManager.InfectedHome -= IncreaseInfectedHome;
        GameEventManager.InfectedIsolation -= IncreaseInfectedIsolated;
        GameEventManager.InfectedIgnored -= IncreaseInfectedNotAnswered;
    }

    private void ShowMenu()
    {
        UpdateContent();
    }

    public override void Show(Action _callback)
    {
        UpdateContent(() =>
        {
            base.Show(_callback);
        });
    }

    private void UpdateContent()
    {
        UpdateContent(() =>
        {
            base.Show(() =>
            {
                EventManager.OnContentLoaded();
            });
        });
    }

    protected override void UpdateContent(Action _callback)
    {
        if (sceneLoadListenerAdded)
        {
            EventManager.SceneLoad -= UpdateContent;
            EventManager.EventManagerLoaded -= ShowMenu;
        }
        else
        {
            EventManager.EventManagerLoaded -= UpdateContent;
        }

        // Update content here

        _callback?.Invoke();
    }

    public void OnMainMenuSelected()
    {
        AudioManager.Instance.PlayCancel();
        LoadingManager.Instance.LoadScene("MainMenu", new LoadingParameters() { });
    }

    public void IncreaseNormalNotAnswered()
    {
        normalNotWelcomed++;
    }

    public void IncreaseNormalHome()
    {
        normalHome++;
    }

    public void IncreaseNormalIsolated()
    {
        normalIsolated++;
    }

    public void IncreaseInfectedNotAnswered()
    {
        infectedNotWelcomed++;
    }

    public void IncreaseInfectedHome()
    {
        infectedHome++;
    }

    public void IncreaseInfectedIsolated()
    {
        infectedNotWelcomed++;
    }    
}
