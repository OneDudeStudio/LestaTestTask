using UnityEngine;

public enum TileType
{
    SakuraTile,
    MaskTile,
    FoodTile,
    BlockTile
}

public class Tile : MonoBehaviour
{
    public TileType ThisTileType;
    public Vector3 TargetPosition;
    public bool IsMovable = true;


    private void OnEnable()
    {
        InitTile();
    }

    private void Update()
    {
        transform.position = TargetPosition;
    }

    private void InitTile()
    {
        if (ThisTileType == TileType.BlockTile)
        {
            IsMovable = false;
        }
        TargetPosition = transform.position;
    }
}
