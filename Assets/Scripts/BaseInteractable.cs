using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    [Header("UI ve Ses")]
    public string promptMessage = "Etkileşime Geç";
    public AudioClip interactSound;
    public void BaseInteract()
    {
        if (interactSound != null)
        {
            AudioSource.PlayClipAtPoint(interactSound, transform.position);
        }
        Interact(); 
    }
    protected abstract void Interact();
}