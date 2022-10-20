using System.Collections.Generic;
using UnityEngine;

public class ColumnChecker : MonoBehaviour
{
    [SerializeField] private TileType _columnTileType;
    [SerializeField] private Transform[] _columnSlots;
    [SerializeField] private List<Tile> _columnTiles = new List<Tile>();
    [SerializeField] private bool _isFinished;

    private AudioManager _audioManager;
    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void UpdateTilesInColumn()
    {
        _columnTiles.Clear();
        foreach (var slots in _columnSlots)
        {
            Ray ray = new Ray(slots.transform.position, Vector3.down * 0.5f);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                Tile currentTile = hit.collider.GetComponent<Tile>();
                if (currentTile != null)
                {
                    if (currentTile.ThisTileType == _columnTileType && !_columnTiles.Contains(currentTile))
                    {
                        _columnTiles.Add(currentTile);
                    }
                }
            }
        }
        CheckColumnEnd();
    }
    public bool GetIsFinished()
    {
        return _isFinished;
    }
    public void CheckColumnEnd()
    {
        if (_columnTiles.Count == 5 )
        {
            if (!_isFinished)
            {
                _isFinished = true;
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                _audioManager.PlayTargetSound(_audioManager.ColumnDone);
            }
            
        }
        else
        {
            if (_isFinished)
            {
                _isFinished = false;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                _audioManager.PlayTargetSound(_audioManager.ColumnLose);
            }
            
        }
    }
}
