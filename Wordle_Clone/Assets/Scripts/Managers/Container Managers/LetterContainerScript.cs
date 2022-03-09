using UnityEngine;
using UnityEngine.UI;

public class LetterContainerScript : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Text letterText;

    public void SetLetterText(string letter) => letterText.text = letter;
    public void SetboxColor(Color color) => background.color = color;

    public void ClearLetterContainer() => letterText.text = "";
}
