using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyBoardManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] KeyCode _keyCode;

    Text btnText;

    private void Start()
    {
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
        GameManager.instance.AddNewLetter(_keyCode.ToString());
    }

    public void Submit()
    {
        GameManager.instance.CheckWord();
    }

    public void Delete()
    {
        GameManager.instance.DeleteLastLetter();
    }

    public void SetKeyboardLetterColor()
    {

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
