using UnityEngine;
using UnityEngine.UI;

public class LetterScript : MonoBehaviour
{
    public string containingCharacter;

    Animator letterContainerAnim;

    readonly int letterContainerAnimStateParameter = Animator.StringToHash("State");
    public Image image;
    [SerializeField] private Text letterText;

    private void Start()
    {
        image = GetComponent<Image>();
        letterText = GetComponentInChildren<Text>();
        letterContainerAnim = GetComponent<Animator>();
    }

    public void SetLetterText(string letter)
    {
        containingCharacter = letter;
        letterText.text = letter.ToString();
        SetStateInt((int)LetterState.letter_enter);
    }

    public void SetStateInt(int state) => letterContainerAnim.SetInteger(letterContainerAnimStateParameter, state);
    public void SetStateTrigger(string trigger) => letterContainerAnim.SetTrigger(trigger);

    public void ClearLetterContainer()
    {
        letterText.text = "";
        SetStateInt((int)LetterState.original);
    }
}
