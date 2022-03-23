using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    void Start()
    {
        Button newButton = GetComponent<Button>();
        newButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        PlayMusic();
    }

    void PlayMusic()
    {
        GameObject.FindObjectOfType<AudioManager>().Play("BtnClick");
    }

    
}
