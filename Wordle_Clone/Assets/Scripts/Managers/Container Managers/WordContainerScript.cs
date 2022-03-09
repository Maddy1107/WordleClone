using System.Collections.Generic;
using UnityEngine;

public class WordContainerScript : MonoBehaviour
{
    [SerializeField] private List<LetterContainerScript> letterContainers;

    public void AddLettertoLetterContainer(int letterIndex, string letter)
    {
        letterContainers[letterIndex].SetLetterText(letter);
    }

    public void DeleteLetterfromLastContainer(int letterIndex)
    {
        letterContainers[letterIndex].ClearLetterContainer();
    }

    public void ClearWordContainer()
    {
        foreach (var item in letterContainers)
        {
            item.ClearLetterContainer();
        }
    }

    public void SetLetterContainerColor(int index,Color color)
    {
        letterContainers[index].SetboxColor(color);
    }

}
