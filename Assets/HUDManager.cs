using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playersInfo, gameInfo, buttonLabel;

    [SerializeField]
    private Button button;

    private void Start()
    {
        button.gameObject.SetActive(false);
    }

    public void UpdatePlayersInfo(string humanSymbol, string computerSymbol)
    {
        string humanInfo = "Você: " + humanSymbol;
        string computerInfo = "Computador: " + computerSymbol;

        playersInfo.text = humanInfo + "\n" + computerInfo;
    }

    public void UpdatePlayersTurn(string currentTurnSymbol, string humanSymbol, string computerSymbol) 
    {
        if (currentTurnSymbol == humanSymbol)
            gameInfo.text = "Sua vez";
        else
            gameInfo.text = "Vez do computador";
    }

    public void ShowEndGameResult(string message) 
    {
        gameInfo.text = message;
        button.gameObject.SetActive(true);
    }
}
