using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    // Prevent non-singleton constructor use.
    protected MusicManager() { }
    public static MusicManager Instance;

    // Component references
    [SerializeField] AudioSource audioSource;

    // Addressable Songs in project
    //[SerializeField] SerializableDictionaryBase<Songs, string> songsPaths;

    // Private properties
    float delayUntilNextSong = 0f; // Delay in-between songs
    float maxVolume = 1f; // Max volume

    #region Initialization
    private void OnValidate()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Singleton start
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this); // MusicManager should stay always loaded
        }
        else
        {
            if (Instance != this)
            {
                if (audioSource.clip != null)
                {
                    Instance.audioSource.loop = audioSource.loop;
                    Instance.StartMusic(audioSource.clip);
                }
                else
                {
                    Debug.LogError("Music Manager Audio source clip is null -1-");
                }
                Destroy(gameObject);
                return;
            }
        }

        if (!audioSource.isPlaying)
        {
            if (audioSource.clip != null)
            {
                StartMusic(audioSource.clip);
            }
            else
            {
                Debug.LogError("Audio source clip is null -2-");
            }
        }
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnEnable()
    {
        
    }
    #endregion Initialization

    #region Public Methods
    public void StartAgain()
    {
        StartMusic(audioSource.clip);
    }

    public void StartMusic(AudioClip _nextClip)
    {
        if (_nextClip == null) // If no song received
        {
            //Debug error
            return;   
        }

        if (audioSource.clip != null)
        {
            if (_nextClip.name == audioSource.clip.name && audioSource.isPlaying) // If trying to play same song again
            {
                return;
            }
        }

        CancelInvoke(); // Cancel automatic fade

        if (audioSource.isPlaying)
        {
            FadeMusicTo(AnimationTimers.MUSIC_FADEOUT, 0, () =>
            { // fade OUT current song
                audioSource.clip = _nextClip; // update with new song
                audioSource.Play(); // start playing new song
                FadeMusicTo(AnimationTimers.MUSIC_FADEIN, maxVolume); // fade IN new song
                if (!audioSource.loop)
                {
                    AddFadeToEndOfMusic(); // Add fade automatic OUT
                }
            });
        }
        else
        {
            audioSource.clip = _nextClip; // update with new song
            audioSource.Play(); // start playing new song
            AudioManager.Instance.SetVolume("musicFadeVolume", 1);
            //FadeMusicTo(AnimationTimers.MUSIC_FADEIN, maxVolume); // fade IN new song
            //if (!audioSource.loop)
            //{
                AddFadeToEndOfMusic(); // Add fade automatic OUT
            //}
        }
    }

    public void StopMusic()
    {
        FadeMusicTo(AnimationTimers.MUSIC_FADEOUT, 0);
    }

    public void FadeMusicTo(float _fadeLenght, float _fadeToValue, Action _callback = null)
    {
        AudioManager.Instance?.FadeTo("musicFadeVolume", _fadeLenght, _fadeToValue, () => {
            if (_fadeToValue > 0)
            {

            }
            else
            {
                audioSource.Stop();
            }
            _callback?.Invoke();
        });
    }
    #endregion Public Methods

    #region Private Helpers
    private void AddFadeToEndOfMusic()
    {
        if (audioSource.clip != null)
        {
            Invoke("FadeMusicAtEnd", audioSource.clip.length - AnimationTimers.MUSIC_FADEOUT); // Invoke automatic fade OUT
        }
        else
        {
            Debug.LogError("Audio source clip is null -73-");
        }
    }

    private void FadeMusicAtEnd()
    {
        AudioManager.Instance.FadeTo("musicFadeVolume", AnimationTimers.MUSIC_FADEOUT, 0, () => {
            audioSource.Stop();

            // Invoke everything again from the start once fade OUT ends. Also, waiting for delayUntilNextSong
            //StartAgain();
            Invoke("StartAgain", delayUntilNextSong); 
        });
    }

    
    #endregion Private Helpers
}
