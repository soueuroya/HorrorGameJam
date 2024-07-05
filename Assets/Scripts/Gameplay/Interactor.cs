using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] bool lockMovement;
    private Interactable currentInteractable;
    private Interactable previousInteractable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentInteractable = collision.gameObject.GetComponent<Interactable>();
        if (currentInteractable == null) { return; } // If its not interactable, just return

        if (previousInteractable != null) // if there is a previous interactable
        {
            if (currentInteractable == previousInteractable) { return; } // If its the same interactable, just ignore
            previousInteractable.HideCanvas(); // Hide previous interactable
        }

        currentInteractable.ShowCanvas(); // Show new interactable
        previousInteractable = currentInteractable; // Update interactables
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentInteractable = collision.gameObject.GetComponent<Interactable>();
        if (currentInteractable == null) { return; } // If its not interactable, just return

        if (previousInteractable != null) // if there is a previous interactable
        {
            if (currentInteractable != previousInteractable) { return; } // If its the same interactable, just ignore
            previousInteractable.HideCanvas(); // Hide previous interactable
        }

        previousInteractable = null; // unset previous interactable
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.E))
        {
            previousInteractable?.Interacted(lockMovement);
        }
    }
}
