using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ContainerManagers container;
    public ColorScript colors;

    public string currentword;
    public string enteredWord;

    public List<string> currentwordLetters;
    public List<string> enteredWordletters;

    private void Awake() => instance = this;

    private void Start()
    {
        colors = GetComponent<ColorScript>();
        currentword = WordLibraryManager.instance.GetRandomWord();
        SplitWord();
    }

    public void SplitWord()
    {
        for (int i = 0; i < currentword.Length - 1; i++)
        {
            currentwordLetters.Add(System.Convert.ToString(currentword[i]));
        }
    }

    public void JoinWord()
    {
        enteredWord = "";
        for (int i = 0; i < enteredWordletters.Count; i++)
        {
            enteredWord += enteredWordletters[i].ToLower();
        }
    }

    public void AddNewLetter(string letter)
    {
        if (enteredWordletters.Count < 5)
        {
            container.AddLetter(letter);
            enteredWordletters.Add(letter);
        }
    }

    public void DeleteLastLetter()
    {
        if (enteredWordletters.Count > 0)
        {
            container.DeleteLetter();
            enteredWordletters.RemoveAt(enteredWordletters.Count - 1);
        }
    }

    public void CheckWord()
    {
        JoinWord();
        Debug.Log(WordLibraryManager.instance.CheckifValid(enteredWord + " "));
        Debug.Log(WordLibraryManager.instance.CheckifValid(enteredWord));

        if (true)
        {
            for (int i = 0; i < 5; i++)
            {
                if(enteredWord[i] == currentword[i])
                {
                    container.SetColor(i, colors.GetCorrectColor());
                }
                else
                {
                    container.SetColor(i, colors.GetWrongColor());
                }
            }
            ResetForNextTry();
        }
    }

    public void ResetForNextTry()
    {
        container.MovetoNextWord();
        enteredWord = "";
        enteredWordletters.Clear();
    }

}
