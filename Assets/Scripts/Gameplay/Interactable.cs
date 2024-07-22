using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] UnityEvent onInteract;
    [SerializeField] UnityEvent onUnteract;
    [SerializeField] bool shouldStopPlayer;
    [SerializeField] bool shouldUnteract = true;

    bool interacted = false;

    public void ShowCanvas()
    {
        canvas.gameObject.SetActive(true);
    }

    public void HideCanvas()
    {
        canvas.gameObject.SetActive(false);
    }

    public void ToggleCanvas()
    {
        canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
    }

    public void Interacted(bool lockMovement)
    {
        if (interacted && shouldUnteract)
        {
            Unteracted(lockMovement);
            return;
        }

        interacted = true;
        onInteract?.Invoke();

        if (shouldStopPlayer && lockMovement)
        {
            GameEventManager.OnMovementLocked();
        }
    }

    public void Unteracted(bool lockMovement)
    {
        interacted = false;
        onUnteract?.Invoke();

        if (shouldStopPlayer && lockMovement)
        {
            GameEventManager.OnMovementUnlocked();
        }
    }
}
