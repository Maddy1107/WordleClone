using System.Collections.Generic;
using UnityEngine;

public class ContainerManagers : MonoBehaviour
{
    [SerializeField] private List<WordContainerScript> wordContainers;

    int currentContainerIndex = 0;

    int currentletterIndex = 0;
    
    public void MovetoNextLetter()
    {
        currentletterIndex++;
    }

    public void MovetoPreviousLetter()
    {
        currentletterIndex--;
    }

    public void MovetoNextWord()
    {
        currentContainerIndex++;
        currentletterIndex = 0;
    }

    public void AddLetter(string letter)
    {
        wordContainers[currentContainerIndex].AddLettertoLetterContainer(currentletterIndex, letter);
        MovetoNextLetter();
    }

    public void DeleteLetter()
    {
        MovetoPreviousLetter();
        wordContainers[currentContainerIndex].DeleteLetterfromLastContainer(currentletterIndex);
    }

    public void ClearContainer()
    {
        foreach (var item in wordContainers)
        {
            item.ClearWordContainer();
        }
    }

    public void SetColor(int index,Color color)
    {
        wordContainers[currentContainerIndex].SetLetterContainerColor(index,color);
    }
}
