using UnityEngine;

public class GameEventManager : EventManager
{
    // Prevent non-singleton constructor use.
    protected GameEventManager() {}
    public new static GameEventManager Instance;

    public static event EventFired GameEventManagerLoaded;

    // Game State Control
    public static event EventFired<bool> ChampionDefeated;
    public static event EventFired GameStart;
    public static event EventFired GameEnd;
    public static event EventFired NormalHome;
    public static event EventFired InfectedHome;
    public static event EventFired NormalIgnored;
    public static event EventFired InfectedIgnored;
    public static event EventFired NormalIsolation;
    public static event EventFired InfectedIsolation;
    public static event EventFired ConfirmationPressed;
    

    #region Initialization
    //Singleton awake
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this); // Event Manager should stay always loaded
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Debug.Log("calling manager loaded");
        OnGameEventManagerLoaded();
    }
    #endregion Initialization

    #region Public Methods

    public static void OnGameEventManagerLoaded() { FireEvent(GameEventManagerLoaded); }


    // Game State Control
    public static void OnVictory(bool isLocalPlayer)        { FireEvent(ChampionDefeated, isLocalPlayer); }
    public static void OnGameStart()                        { FireEvent(GameStart); }
    public static void OnGameEnd()                          { FireEvent(GameEnd); }
    public static void OnNormalHome()                       { FireEvent(NormalHome); }
    public static void OnInfectedHome()                     { FireEvent(InfectedHome); }
    public static void OnNormalIsolation()                  { FireEvent(NormalIsolation); }
    public static void OnInfectedIsolation()                { FireEvent(InfectedIsolation); }
    public static void OnNormalIgnored()                    { FireEvent(NormalIgnored); }
    public static void OnInfectedIgnored()                  { FireEvent(InfectedIgnored); }
    public static void OnConfirmationPressed()              { FireEvent(ConfirmationPressed); }

    #endregion Public Methods
}