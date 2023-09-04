using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public int Size 
    {
        get
        {
            return size;
        }
    }

    [SerializeField]
    private Cell buttonPrefab;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private float initialPosition = -90;

    [SerializeField]
    private float distance = 90;

    private const int size = 3;
    public Cell[,] Cells { get; private set; }

    public void Draw()
    {
        Cells = new Cell[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cells[y, x] = Instantiate(buttonPrefab.gameObject, canvas.transform).GetComponent<Cell>();
                float positionX = initialPosition + x * distance;
                float positionY = initialPosition + y * distance;
                Cells[y, x].GetComponent<RectTransform>().localPosition = new Vector2(positionX, positionY);
            }
        }
    }

    public void SetInteractable(bool value)
    {
        foreach (var cell in Cells)
        {
            cell.SetInteractable(value);
        }
    }
}
