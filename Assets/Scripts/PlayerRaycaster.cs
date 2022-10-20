using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    public Transform pointer;

    private Camera _mainCamera;
    private Tile _currentTile;
    private TileManager _tileManager;
    private GameManager _gameManager;

    [SerializeField] List<EmptySpace> nearEmptySpaces = new List<EmptySpace>();
    [SerializeField] private EmptySpace _currentEmptySpace;
    [SerializeField] private bool _waitInput = false;

    private void Awake()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _tileManager = FindObjectOfType<TileManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit)
        {
            pointer.position = hit.transform.position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_waitInput == false)
            {
                RaycastTile(hit);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _waitInput = false;
        }
    }
    private void RaycastTile(RaycastHit2D hit)
    {
        if (hit)
        {
            StartCoroutine(SwitchTileCoroutine(hit.transform, hit.collider.GetComponent<Tile>()));
        }
    }
    private IEnumerator SwitchTileCoroutine(Transform hit, Tile tileToMove)
    {
        nearEmptySpaces.Clear();
        int potentialDirectionToMove = 0;

        for (int i = 0; i < _tileManager.EmptySpaces.Length; i++)
        {
            if (Vector2.Distance(_tileManager.EmptySpaces[i].transform.position, hit.position) < 1 && tileToMove.IsMovable == true)
            {
                potentialDirectionToMove++;
                nearEmptySpaces.Add((_tileManager.EmptySpaces[i]));
            }
        }

        if (potentialDirectionToMove > 1)
        {
            _waitInput = true;

            for (int i = 0; i < nearEmptySpaces.Count; i++)
            {
                nearEmptySpaces[i].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                nearEmptySpaces[i].ActivateCollider();
            }

            yield return new WaitWhile(() => _waitInput);

            for (int i = 0; i < nearEmptySpaces.Count; i++)
            {
                if (pointer.transform.position == nearEmptySpaces[i].transform.position && _currentEmptySpace == null)
                {
                    _currentEmptySpace = nearEmptySpaces[i];
                }
            }

            if (_currentEmptySpace != null)
            {
                SwitchTile(_currentEmptySpace, tileToMove);
                _gameManager.DoTurn();
                _gameManager.TileSwitched();
                _currentEmptySpace = null;
            }
            for (int i = 0; i < nearEmptySpaces.Count; i++)
            {
                nearEmptySpaces[i].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                nearEmptySpaces[i].DeactivateCollider();
            }
        }

        else if (potentialDirectionToMove != 0)
        {
            SwitchTile(nearEmptySpaces[0], tileToMove);
            _gameManager.DoTurn();
            _gameManager.TileSwitched();
        }

        yield return new WaitForSecondsRealtime(0.2f);
        _gameManager.CheckGameWin();
    }
    private void SwitchTile(EmptySpace emptySpace, Tile tileToMove)
    {
        Vector2 lastEmptySpacePosition = emptySpace.transform.position;
        emptySpace.transform.position = tileToMove.transform.position;
        tileToMove.TargetPosition = lastEmptySpacePosition;
    }
}
