using System;
using UnityEngine;

public class PauseManager : BaseMenu
{
    private bool sceneLoadListenerAdded = false;

    private void OnEnable()
    {
        InstantHide();
    }

    public override void Show(Action _callback)
    {
        UpdateContent(() =>
        {
            base.Show(_callback);
        });
    }

    public override void Hide(Action _callback)
    {
        // Hide only if is visible but callback should be called anyway
        if (!isVisible)
        {
            _callback?.Invoke();
            return;
        }

        canvasGroup.interactable = false;

        LeanTween.value(canvasGroup.alpha, to: 0, time: AnimationTimers.TABCONTENT_HIDE)
        .setEaseInOutSine()
        .setOnUpdate((float val) =>
        {
            canvasGroup.alpha = val;
        }).setOnComplete(() => {
            isVisible = false;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            _callback?.Invoke();
            //gameObject.SetActive(false); // Don't desactivate pause screen
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

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Toggle(() => {
                AudioManager.Instance.PlayPopup();
            });
        }
    }

    protected override void UpdateContent(Action _callback)
    {
        // Update content here

        _callback?.Invoke();
    }

    public void OnMainMenuSelected()
    {
        AudioManager.Instance.PlayCancel();
        LoadingManager.Instance.LoadScene("MainMenu", new LoadingParameters() { });
    }

    private void PauseGame()
    {
        UpdateContent();
    }

    public void OnResumeSelected()
    {
        Hide(() => {
            AudioManager.Instance.PlayClick();
            // TODO: turn on animations and unpause game
        });
    }
}
