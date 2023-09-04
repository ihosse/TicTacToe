using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string CurrentTurnSymbol { get; private set; }

    [SerializeField]
    private string humanPlayerSymbol = "X";
    [SerializeField]
    private string computerPlayerSymbol = "Y";

    private GameBoard gameboard;
    private HUDManager hudManager;
    private int turnCount;

    private void Start()
    {
        gameboard = GetComponent<GameBoard>();
        hudManager = GetComponent<HUDManager>();

        gameboard.Draw();

        turnCount = 0;

        hudManager.UpdatePlayersInfo(humanPlayerSymbol, computerPlayerSymbol);

        TakeHumanTurn();
    }

    private void TakeHumanTurn()
    {
        turnCount++;

        gameboard.SetInteractable(true);
        CurrentTurnSymbol = humanPlayerSymbol;
        hudManager.UpdatePlayersTurn(CurrentTurnSymbol, humanPlayerSymbol, computerPlayerSymbol);
    }

    private IEnumerator TakeComputerTurn()
    {
        turnCount++;
        gameboard.SetInteractable(false);
        CurrentTurnSymbol = computerPlayerSymbol;
        hudManager.UpdatePlayersTurn(CurrentTurnSymbol, humanPlayerSymbol, computerPlayerSymbol);

        yield return new WaitForSeconds(1);

        if (VerifyDrawGame() == true)
        {
            EndGame("Empatado!");
            yield break;
        }

        DecideComputerMove();

        if (VerifyWinner(out string winnerName) == true)
        {
            EndGame(winnerName + " ganhou!");
            yield break;
        }

        TakeHumanTurn();
    }
    public void HumanClick(Cell cell)
    {
        cell.ChangeSymbol();

        if (VerifyDrawGame() == true)
        {
            EndGame("Empatado!");
            return;
        }

        if (VerifyWinner(out string winnerName) == true)
        {
            EndGame(winnerName + " ganhou!");
            return;
        }

        StartCoroutine(TakeComputerTurn());
    }
    private void DecideComputerMove()
    {
        int x;
        int y;

        do
        {
            x = Random.Range(0, gameboard.Size);
            y = Random.Range(0, gameboard.Size);
        } while (gameboard.Cells[y, x].Symbol != "");

        gameboard.Cells[y, x].ChangeSymbol();
    }

    private bool VerifyDrawGame()
    {
        if (turnCount > Mathf.Pow(gameboard.Size, 2))
            return true;
        else
            return false;
    }

    private bool VerifyWinner(out string winnerName)
    {
        winnerName = null;
        bool isWinner = false;
        int points = 0;

        //verifica as colunas
        for (int x = 0; x < 3; x++)
        {
            points = 0;
            for (int y = 0; y < 3; y++)
            {
                if (gameboard.Cells[x, y].Symbol == CurrentTurnSymbol)
                    points++;
            }
            if (points == 3)
                isWinner = true;
        }

        //verifica as linhas
        for (int y = 0; y < 3; y++)
        {
            points = 0;
            for (int x = 0; x < 3; x++)
            {
                if (gameboard.Cells[x, y].Symbol == CurrentTurnSymbol)
                    points++;
            }
            if (points == 3)
                isWinner = true;
        }

        //verifica diagonal 1
        points = 0;
        for (int i = 0; i < 3; i++)
        {
            if (gameboard.Cells[i, i].Symbol == CurrentTurnSymbol)
                points++;
        }

        if (points == 3)
            isWinner = true;

        //verifica diagonal 2
        points = 0;
        for (int i = 0; i < 3; i++)
        {
            if (gameboard.Cells[2 - i, i].Symbol == CurrentTurnSymbol)
                points++;
        }

        if (points == 3)
            isWinner = true;

        if (isWinner)
        {
            if (CurrentTurnSymbol == computerPlayerSymbol)
                winnerName = "Computador";
            else
                winnerName = "Você";
        }

        return isWinner;
    }

    private void EndGame(string text)
    {
        gameboard.SetInteractable(false);
        hudManager.ShowEndGameResult(text);
    }
}