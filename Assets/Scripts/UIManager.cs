using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _turnCounter;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TextMeshProUGUI _totalTurnCounter;
    public void ShowFinishPanel(int turnIndex)
    {
        _winPanel.SetActive(true);
        _totalTurnCounter.text = turnIndex.ToString();
    }
    public void UpdateTurnText(int turnIndex)
    {
        _turnCounter.text = turnIndex.ToString();
    }
}
