using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterScript : MonoBehaviour
{
    Animator letterContainerAnim;

    [SerializeField] private Image background;
    [SerializeField] private Text letterText;

    private void Start()
    {
        letterContainerAnim = GetComponent<Animator>();
    }

    public void SetLetterText(string letter) => letterText.text = letter;
    public void SetboxColor(Color color) => background.color = color;

    public void SetState(string trigger)
    {
        letterContainerAnim.SetTrigger(trigger);
    }

    public void ClearLetterContainer() => letterText.text = "";
}
