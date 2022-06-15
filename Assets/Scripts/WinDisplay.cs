using UnityEngine;
using UnityEngine.UI;

public class WinDisplay : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject[] _blackObjects;
    [SerializeField] private GameObject[] _whiteObjects;
    [Header("Buttons")]
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _quitButton;

    private ChessBoard _chessBoard;
    public void Initialize(ChessBoard chessBoard)
    {
        _chessBoard = chessBoard;
        
        _resetButton.onClick.AddListener(OnReset);
        _quitButton.onClick.AddListener(OnQuit);
    }

    private void OnReset()
    {
        _chessBoard.ResetChessBoard();
        gameObject.SetActive(false);
    }

    private void OnQuit() => 
        Application.Quit();

    public void ShowWinTeam(int team)
    {
        gameObject.SetActive(true);
        
        if (team == 0)
        {
            for (int i = 0; i < _whiteObjects.Length; i++) 
                _whiteObjects[i].SetActive(true);
            
            for (int i = 0; i < _blackObjects.Length; i++) 
                _blackObjects[i].SetActive(false);
        }
        else
        {
            for (int i = 0; i < _blackObjects.Length; i++) 
                _blackObjects[i].SetActive(true);
            
            for (int i = 0; i < _whiteObjects.Length; i++) 
                _whiteObjects[i].SetActive(false);
        }
    }
}