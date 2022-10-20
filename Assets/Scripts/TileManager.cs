using UnityEngine;

public class TileManager : MonoBehaviour
{
    public EmptySpace[] EmptySpaces;

    [SerializeField] private Tile[] _allTiles;
    
    private void Awake()
    {
        _allTiles = FindObjectsOfType<Tile>();
        
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < _allTiles.Length; i++)
        {
            Vector2 lastPosition = _allTiles[i].TargetPosition;
            int randomIndex = Random.Range(0, _allTiles.Length);
            _allTiles[i].TargetPosition = _allTiles[randomIndex].TargetPosition;
            _allTiles[randomIndex].TargetPosition = lastPosition;
            //
            var tile = _allTiles[i];
            _allTiles[i] = _allTiles[randomIndex];
            _allTiles[randomIndex] = tile;
        }
    }


}
