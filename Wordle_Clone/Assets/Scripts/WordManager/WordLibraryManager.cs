using System.Collections.Generic;
using UnityEngine;

public class WordLibraryManager : MonoBehaviour
{
    public static WordLibraryManager instance;

    [SerializeField] private TextAsset allwordsFile;
    [SerializeField] private TextAsset allowedwordsFile;

    [SerializeField] private List<string> allwords;
    [SerializeField] private List<string> allowedwords;

    public void Awake() => instance = this;

    private void Start()
    {
        LoadWords();
    }

    public void LoadWords()
    {
        allwords = WordExtractor.GetWords(allwordsFile);
        allowedwords = WordExtractor.GetWords(allowedwordsFile);
    }

    public string GetRandomWord()
    {
        return allowedwords[Random.Range(0, allowedwords.Count)];
    }

    public bool CheckifValid(string word)
    {
        return allowedwords.Contains(word) || allwords.Contains(word);
    }
}
