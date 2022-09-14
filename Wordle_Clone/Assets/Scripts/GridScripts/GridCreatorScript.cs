using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GridCreatorScript : MonoBehaviour
{
    [SerializeField] private int wordLength;

    private int rows = 6;

    GridLayoutGroup _grid;

    public int WordLength
    { 
        get => wordLength; 
    }

    [SerializeField] private GameObject letterPrefab;

    private float _animGapTime = 0.03f;

    public void GridInit()
    {
        SetGridParameters();
        CreateGrid();
    }

    private void SetGridParameters()
    {
        _grid = GetComponent<GridLayoutGroup>();

        _grid.constraintCount = rows;

        float width = GetComponent<RectTransform>().rect.width;
        float height = GetComponent<RectTransform>().rect.height;

        float newCellWidth = Mathf.FloorToInt(width / wordLength);
        float newCellHeight = Mathf.FloorToInt(height / rows);

        Vector2 newSize = new Vector2(newCellWidth,newCellHeight) - _grid.spacing;

        _grid.cellSize = newSize;
    }

    public void CreateGrid()
    {
        for (int i = 0; i < (wordLength * rows); i++)
        {
            GameObject newLetterPrefab = Instantiate(letterPrefab);
            newLetterPrefab.transform.SetParent(this.transform,false);
            GameManager.instance.lettersPrefab.Add(newLetterPrefab.GetComponent<LetterScript>());
            StartCoroutine(ShowBlock(i * _animGapTime,newLetterPrefab.GetComponent<LetterScript>()));
        }
    }

    IEnumerator ShowBlock(float offset,LetterScript letterBlock)
    {
        yield return new WaitForSeconds(offset);
        letterBlock.SetStateInt((int)LetterState.original);
        if (offset/ _animGapTime >= (wordLength * rows) - 1)
            GameManager.instance.ChangeState(GameState.idle);
    }
}
