using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    // Prevent non-singleton constructor use.
    protected LoadingManager() { }
    public static LoadingManager Instance;

    #region Initialization
    // Singleton awake
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        EventManager.ContentLoaded += OnContentLoaded;
        EventManager.SceneLoad += OnSceneLoaded;
        EventManager.SceneUnload += OnSceneUnloaded;
        EventManager.MainMenuSceneLoad += OnMainMenuSceneLoad;
        EventManager.EventManagerLoaded += InitializeLoading;
    }

    private void InitializeLoading()
    {
        // Udates screen mode to whichever was set previously locally in this machine.
        if (SafePrefs.HasKey(Constants.Prefs.screenModeKey))
        {
            // get previously set mode
            int selectedValueIndex = SafePrefs.GetInt(Constants.Prefs.screenModeKey, 0);

            // if the mode is different, update
            if (Screen.fullScreenMode != (FullScreenMode)selectedValueIndex)
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, (FullScreenMode)selectedValueIndex);
            }
        }

        LoadingScreen.Instance.Hide();
    }

    private void OnDestroy()
    {
        EventManager.ContentLoaded -= OnContentLoaded;
        EventManager.SceneLoad -= OnSceneLoaded;
        EventManager.SceneUnload -= OnSceneUnloaded;
        EventManager.MainMenuSceneLoad -= OnMainMenuSceneLoad;
        EventManager.EventManagerLoaded -= InitializeLoading;
    }
    #endregion Initialization

    #region Public Methods
    /// <summary>
    /// Loads scene content as Addressables
    /// </summary>
    /// <param name="_scene"></param>
    /// <param name="resourceKey"></param>
    /// <param name="_loadingParameters"></param>
    public void LoadScene(string _scene, LoadingParameters _loadingParameters = null)
    {
        InputManager.Instance.ReadInput = false;

        EventManager.EventFired onLoadingScreenShowListener = null;
        onLoadingScreenShowListener = () =>
        {
            EventManager.LoadingScreenShow -= onLoadingScreenShowListener;
            StartCoroutine(LoadSceneAsync(_scene));
        };

        EventManager.LoadingScreenShow += onLoadingScreenShowListener;

        // Show loading screen
        LoadingScreen.Instance.Show(_loadingParameters);
    }

    private IEnumerator LoadSceneAsync(string _scene)
    {
        // Start loading the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_scene);
        asyncOperation.allowSceneActivation = false;

        // While the scene is loading, update the loading bar
        while (!asyncOperation.isDone)
        {
            // Update the loading bar (progress is between 0 and 0.9 while loading, and 0.9 to 1 when done)
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            LoadingBarManager.Instance.UpdateBarSize(progress);

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                // Optionally wait a few frames to ensure loading screen is fully visible
                yield return new WaitForSeconds(0.5f);

                // Activate the scene
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Remove the event handler to prevent it from being called multiple times
        EventManager.OnSceneLoad();
    }
    #endregion Public Methods

    #region Private Helpers
    /*
    public void UnloadScene(ResourceManager.ResourceKeys resourceKey)
    {
        // TODO: Not working on MAC / OSX?
        //if (sceneInstancePreviouslySet)
        //{
        //    ResourceManager.UnloadScene(oldSceneInstance, resourceKey, (e) =>
        //    {
        //        EventManager.OnSceneUnload();
        //    });
        //}
        //else
        //{
        //    StartCoroutine(UnloadPreviousInstantiatedScene());
        //}
        //EventManager.OnSceneUnload();
        ResourceManager.UnloadSceneWithManager();
    }
    */

    private void OnSceneLoaded()
    {
        // Wait for scene to unload

        //EventManager.OnSceneUnload();

        //if (previousSceneKey != ResourceManager.ResourceKeys.ManagersScene)
        //{
            //UnloadScene(previousSceneKey);
        //}
        //else
        //{
        //    EventManager.OnSceneUnload();
        //}
    }

    private void OnSceneUnloaded()
    {
        //// Hide loading screen
        // Release assets
        LoadingScreen.Instance.Hide();
    }

    private void OnContentLoaded()
    {
        //// Hide loading screen
        //LoadingScreen.Instance.Hide();
        Debug.Log("On content loaded - Unloading prev scene");
        EventManager.OnSceneUnload();
    }

    private void OnMainMenuSceneLoad()
    {
        //// Load Main Menu
        LoadScene("MainMenu", new LoadingParameters() {  });
    }
    #endregion Private Helpers
}
