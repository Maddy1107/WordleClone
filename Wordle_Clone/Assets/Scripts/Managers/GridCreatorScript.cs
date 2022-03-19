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

    [SerializeField] LetterScript[] letterObjs;

    public void GridInit()
    {
        SetGridParameters();
        CreateGrid();
    }

    private void SetGridParameters()
    {
        _grid = GetComponent<GridLayoutGroup>();

        _grid.constraintCount = rows;
    }

    public void CreateGrid()
    {
        for (int i = 0; i < (wordLength * rows); i++)
        {
            GameObject newLetterPrefab = Instantiate(letterPrefab);
            newLetterPrefab.transform.SetParent(this.transform);
            GameManager.instance.lettersPrefab.Add(newLetterPrefab.GetComponent<LetterScript>());
            StartCoroutine(ShowBlock(i * 0.03f,newLetterPrefab.GetComponent<LetterScript>()));
        }
    }

    IEnumerator ShowBlock(float offset,LetterScript letterBlock)
    {
        yield return new WaitForSeconds(offset);
        letterBlock.SetState("idle");
        GameManager.instance.isGridGenerating = false;
    }
}
