using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class KeyBoardManager : MonoBehaviour, IPointerClickHandler
{
    [Serializable]
    public struct BtnColor
    {
        public LetterState letterstate;
        public Color color;
    }

    public KeyCode _keyCode;

    Text btnText;

    Image btnImage;

    public BtnColor[] btnColor;

    private void Start()
    {
        btnImage = GetComponent<Image>();
        btnText = GetComponentInChildren<Text>();
        SetKeyboardButtonText();
    }

    private void SetKeyboardButtonText()
    {
        if (_keyCode == KeyCode.KeypadEnter)
            btnText.text = "ENTER";
        else if (_keyCode == KeyCode.Backspace)
            btnText.text = "X";
        else
            btnText.text = _keyCode.ToString();
    }

    public void onTyped()
    {
        Debug.Log("Enter");
        GameManager.instance.AddNewLetter(_keyCode.ToString());
        GameManager.instance.keys.Add(this);
    }

    public void Submit()
    {
        GameManager.instance.CheckWord();
    }

    public void Delete()
    {
        GameManager.instance.DeleteLastLetter();
    }

    public void SetKeyBoardColor(LetterState currState)
    {
        foreach (BtnColor state in btnColor)
        {
            if(state.letterstate == currState)
            {
                btnImage.color = state.color;
                btnText.color = Color.white;
                break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_keyCode == KeyCode.KeypadEnter)
            Submit();
        else if (_keyCode == KeyCode.Backspace)
            Delete();
        else
            onTyped();
    }
}
