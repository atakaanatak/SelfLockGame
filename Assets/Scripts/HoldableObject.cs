using UnityEngine;

// Dikkat: MonoBehaviour'dan değil, BaseInteractable'dan miras alıyor!
public abstract class HoldableObject : BaseInteractable
{
    [Header("Elde Tutma Ayarları")]
    public Vector3 holdScale = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 holdRotation = Vector3.zero;
    public Vector3 holdPositionOffset = Vector3.zero;

    protected bool isHeld = false; // Obje elde mi diye kontrol

    protected override void Interact()
    {
        if (!isHeld)
        {
            GrabObject();
        }
    }

    // Elde tutma işleminin ana mekaniği
    protected virtual void GrabObject()
    {
        Transform playerHand = GameObject.Find("HoldPoint").transform;

        transform.SetParent(playerHand);


        transform.localPosition = holdPositionOffset;
        transform.localEulerAngles = holdRotation;
        transform.localScale = holdScale;

        Collider col = GetComponent<Collider>();
        if(col != null) col.enabled = false;

        isHeld = true;
        OnGrabbed(); 
    }
    protected virtual void OnGrabbed() { }
}