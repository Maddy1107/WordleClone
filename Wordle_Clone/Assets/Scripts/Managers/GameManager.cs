using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GridCreatorScript _grid;

    [Header ("Words")]
    public string _currentword;
    public string _enteredWord;

    [Header("Letters")]
    public List<string> _currentwordLetters;
    public List<string> _enteredWordletters;

    [Header("Letter Slots")]
    public List<LetterScript> lettersPrefab;

    [Header("Current Letter Pos & Tries")]
    public int currentLetterIndex = 0;
    public int triesIndex = 1;

    public bool isGridGenerating = true;
    public bool isChecking = false;

    private void Awake() => instance = this;

    private void Start()
    {
        _grid.GridInit();
        GetNewWord();
    }

    private void GetNewWord()
    {
        _currentword = WordLibraryManager.instance.GetRandomWord();
        SplitWord();
    }

    #region Split/Join Word To/From List
    public void SplitWord()
    {
        for (int i = 0; i < _currentword.Length - 1; i++)
        {
            _currentwordLetters.Add(System.Convert.ToString(_currentword[i]));
        }
    }

    public void JoinWord()
    {
        _enteredWord = "";
        for (int i = 0; i < _enteredWordletters.Count; i++)
        {
            _enteredWord += _enteredWordletters[i].ToLower();
        }
    }

    #endregion

    #region Add/Delete Letter
    public void AddNewLetter(string letter)
    {
        if (_enteredWordletters.Count < _grid.WordLength && !isGridGenerating)
        {
            lettersPrefab[(triesIndex * _grid.WordLength) + currentLetterIndex].SetLetterText(letter);
            currentLetterIndex += 1;
            _enteredWordletters.Add(letter);
        }
    }

    public void DeleteLastLetter()
    {
        if (_enteredWordletters.Count > 0)
        {
            currentLetterIndex -= 1;
            lettersPrefab[(triesIndex * _grid.WordLength) + currentLetterIndex].ClearLetterContainer();
            _enteredWordletters.RemoveAt(_enteredWordletters.Count - 1);
        }
    }

    #endregion

    #region MainLogic
    public void CheckWord()
    {
        JoinWord();
        Debug.Log(WordLibraryManager.instance.CheckifValid(_enteredWord + " "));
        Debug.Log(WordLibraryManager.instance.CheckifValid(_enteredWord));

        if (_enteredWord.Length < _grid.WordLength)
            Shake();
        else
        {
            if (true)
            {
                for (int i = 0; i < _grid.WordLength; i++)
                {
                    int index = (triesIndex * _grid.WordLength) + i;

                    bool correct = _enteredWord[i] == _currentword[i];

                    if (!correct)
                    {
                        bool letterinWord = false;
                        for (int j = 0; j < _grid.WordLength; j++)
                        {
                            letterinWord = _enteredWord[i] == _currentword[j];
                            if (letterinWord)
                                break;
                        }
                        StartCoroutine(PlayLetterAnim(index, i * 0.5f, letterinWord ? "partial" : "wrong"));
                    }
                    else
                    {
                        StartCoroutine(PlayLetterAnim(index, i * 0.5f, "correct"));
                    }
                }
                ResetForNextTry();
            }
        }
    }
    #endregion

    private void Shake()
    {
        for (int i = 0; i < _grid.WordLength; i++)
        {
            lettersPrefab[(triesIndex * _grid.WordLength) + i].SetState("invalid");
        }
        GameEvents.Toast?.Invoke("NOT ENOUGH LETTERS!!");
    }

    IEnumerator PlayLetterAnim(int index, float gapTime, string trigger)
    {
        yield return new WaitForSeconds(gapTime);
        lettersPrefab[index].SetState(trigger);
    }

    public void ResetForNextTry()
    {
        if (triesIndex >= 5)
            GameEvents.Toast?.Invoke("GAME COMPLETE");
        else
        {
            isChecking = false;
            currentLetterIndex = 0;
            triesIndex += 1;
            _enteredWord = "";
            _enteredWordletters.Clear();
        }
    }

}
