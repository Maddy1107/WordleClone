using UnityEngine;
using UnityEngine.UI;

public class LetterScript : MonoBehaviour
{
    public string containingCharacter;

    Animator letterContainerAnim;

    readonly int letterContainerAnimStateParameter = Animator.StringToHash("State");

    [SerializeField] private Image background;
    [SerializeField] private Text letterText;

    private void Start()
    {
        letterText = GetComponentInChildren<Text>();
        letterContainerAnim = GetComponent<Animator>();
    }

    public void SetLetterText(string letter)
    {
        containingCharacter = letter;
        letterText.text = letter.ToString();
    }

    public void SetboxColor(Color color) => background.color = color;

    public void SetStateInt(int state) => letterContainerAnim.SetInteger(letterContainerAnimStateParameter, state);
    public void SetStateTrigger(string trigger) => letterContainerAnim.SetTrigger(trigger);

    public void ClearLetterContainer() => letterText.text = "";
}
