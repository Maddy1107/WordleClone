using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;

    [SerializeField] private GridCreatorScript _grid;

    [SerializeField] private GameObject keyboard;
    [SerializeField] private GameObject Canvas;

    [SerializeField] private GameObject gameOverCanvas;

    [SerializeField] Text status;

    private string _currentword;
    private string _enteredWord;

    private List<string> _currentwordLetters = new List<string>();
    private List<string> _enteredWordletters = new List<string>();

    public List<LetterScript> lettersPrefab;

    public List<KeyBoardManager> keys = new List<KeyBoardManager>();

    private int currentLetterIndex = 0;
    private int triesIndex = 0;

    private GameState currentState = GameState.isGridGenerating;

    #endregion

    private void Awake() => instance = this;

    public void Start()
    {
        _grid.GridInit();
        GetNewWord();
        GenerateKeyboard();
    }

    private void GenerateKeyboard()
    {
        GameObject newkeyboard = Instantiate(keyboard);
        newkeyboard.transform.SetParent(Canvas.transform, false);
    }

    private void GetNewWord()
    {
        _currentword = WordLibraryManager.instance.GetRandomWord();
        _currentword = _currentword.Remove(_currentword.Length - 1);

        SplitWord();
    }

    public void ChangeState(GameState state)
    {
        currentState = state;
    }

    #region Split/Join Word To/From List
    public void SplitWord()
    {
        for (int i = 0; i < _currentword.Length; i++)
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
        if (currentState != GameState.idle)
            return;

        if (_enteredWordletters.Count < _grid.WordLength)
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

    #region GameLogic
    public void CheckWord()
    {
        JoinWord();

        if (_enteredWord.Length < _grid.WordLength)
        {
            Shake();
            GameEvents.Toast?.Invoke("NOT ENOUGH LETTERS!!");
        }
        else
        {
            ChangeState(GameState.isChecking);
            if (true)//WordLibraryManager.instance.CheckifValid(_enteredWord))
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
                        StartCoroutine(PlayLetterAnim(index, i * 0.3f, letterinWord ? LetterState.partial : LetterState.wrong));
                    }
                    else
                    {
                        StartCoroutine(PlayLetterAnim(index, i * 0.3f, LetterState.correct));
                    }
                }
                if (_enteredWord == _currentword)
                {
                    status.text = "YOU WIN \nCORRECT WORD:" + _currentword.ToUpper();
                    gameOverCanvas.GetComponent<Animator>().SetTrigger("UP");
                }
                else if(triesIndex >= 5)
                {
                    status.text = "YOU LOOSE \nCORRECT WORD:" + _currentword.ToUpper();
                    gameOverCanvas.GetComponent<Animator>().SetTrigger("UP");
                }
                else
                    ResetForNextTry();
            }
            /*else
            {
                Shake();
                GameEvents.Toast?.Invoke("NOT IN WORD LIST");
                ChangeState(GameState.idle);
            }*/
        }
    }

    private void Shake()
    {
        for (int i = 0; i < _grid.WordLength; i++)
        {
            lettersPrefab[(triesIndex * _grid.WordLength) + i].SetStateTrigger("invalid");
        }
    }

    IEnumerator PlayLetterAnim(int index, float gapTime, LetterState state)
    {
        yield return new WaitForSeconds(gapTime);
        lettersPrefab[index].SetStateInt((int)state);

        KeyCode currKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), lettersPrefab[index].containingCharacter);
        
        foreach (KeyBoardManager key in keys)
        {
            if (key._keyCode == currKey)
            {
                key.SetKeyBoardColor(state);
                break;
            }
        }
        if((index + 1) % _grid.WordLength == 0)
            ChangeState(GameState.idle);
    }

    public void ResetForNextTry()
    {
        currentLetterIndex = 0;
        triesIndex += 1;
        _enteredWord = "";
        _enteredWordletters.Clear();
    }

    public void Restart()
    {
        ChangeState(GameState.idle);

        GetNewWord();

        foreach (KeyBoardManager key in keys)
        {
            key.SetKeyBoardColor(LetterState.original);
        }

        foreach (LetterScript letters in lettersPrefab)
        {
            Destroy(letters.gameObject);
        }

        lettersPrefab.Clear();

        _grid.GridInit();

        status.text = "";

        triesIndex = 0;
        currentLetterIndex = 0;
        _enteredWord = "";
        _enteredWordletters.Clear();
        _currentwordLetters.Clear();
    }
    #endregion

}
