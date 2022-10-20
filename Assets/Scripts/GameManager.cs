using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private ColumnChecker[] _columnCheckers;
    private TileManager _tileManager;
    private UIManager _uiManager;
    private AudioManager _audioManager;
    private int _turnCounter = 0;
    private void Awake()
    {
        _tileManager = FindObjectOfType<TileManager>();
        _columnCheckers = FindObjectsOfType<ColumnChecker>();
        _uiManager = FindObjectOfType<UIManager>();
        _audioManager = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        StartGame();
    }
    private void StartGame()
    {
        ShuffleColumns();
        _tileManager.ShuffleDeck();
        _uiManager.UpdateTurnText(_turnCounter);
    }

    private void ShuffleColumns()
    {
        for (int i = 0; i < _columnCheckers.Length; i++)
        {
            Vector2 lastPosition = _columnCheckers[i].transform.position;
            int randomIndex = Random.Range(0, _columnCheckers.Length);
            _columnCheckers[i].transform.position = _columnCheckers[randomIndex].transform.position;
            _columnCheckers[randomIndex].transform.position = lastPosition;

        }
    }

    
    public void TileSwitched()
    {
        _audioManager.PlayTargetSound(_audioManager.Switch);
    }

    public void DoTurn()
    {
        _turnCounter++;
        _uiManager.UpdateTurnText(_turnCounter);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void CheckGameWin()
    {
        int columnFinishCounter = 0;
        for (int i = 0; i < _columnCheckers.Length; i++)
        {
            _columnCheckers[i].UpdateTilesInColumn();
            if (_columnCheckers[i].GetIsFinished() == true)
            {
                
                columnFinishCounter++;
            }
        }
        if (columnFinishCounter >= _columnCheckers.Length)
        {
            Debug.Log("GAME WIN");
            _uiManager.ShowFinishPanel(_turnCounter);
        }
    }
}
