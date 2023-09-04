using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Cell : MonoBehaviour
{
    public string Symbol 
    {
        get 
        {
            return label.text;
        }
    }

    [SerializeField]
    private TextMeshProUGUI label;

    private GameManager gameManager;
    private Button button;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        button = GetComponent<Button>();
        
        label.text = "";
    }

    public void Click() 
    {
        gameManager.HumanClick(this);
    }

    public void ChangeSymbol() 
    {
        label.text = gameManager.CurrentTurnSymbol;
    }

    internal void SetInteractable(bool value)
    {
        button.interactable = value;
    }
}
