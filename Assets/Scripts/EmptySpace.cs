using UnityEngine;

public class EmptySpace : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _emptyTileBoxCollider;

    private void Awake()
    {
        DeactivateCollider();
    }

    public void ActivateCollider()
    {
        _emptyTileBoxCollider.enabled = true;
    }
    public void DeactivateCollider()
    {
        _emptyTileBoxCollider.enabled = false;
    }
}
