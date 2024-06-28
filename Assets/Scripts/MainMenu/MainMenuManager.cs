using System;

public class MainMenuManager : BaseMenu
{
    private bool sceneLoadListenerAdded = false;

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

    public void OnStartGameSelected()
    {
        LoadingManager.Instance.LoadScene("Game", new LoadingParameters() { });
    }

    public void OnTutorialSelected()
    {
        LoadingManager.Instance.LoadScene("Tutorial", new LoadingParameters() { });
    }

    public void OnCreditsSelected()
    {
        LoadingManager.Instance.LoadScene("Credits", new LoadingParameters() { });
    }

    public void OnExistGameSelected()
    {
        AppHelper.Quit();
    }
}
