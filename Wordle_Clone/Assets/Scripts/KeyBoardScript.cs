using UnityEngine;

public class KeyBoardScript : MonoBehaviour
{
    public void onTyped()
    {
        GameManager.instance.AddNewLetter(gameObject.name);
    }

    public void Submit()
    {
        GameManager.instance.CheckWord();
    }

    public void Delete()
    {
        GameManager.instance.DeleteLastLetter();
    }
}
