using UnityEngine;

public class ColorScript : MonoBehaviour
{
    [SerializeField] private Color correctColor;
    [SerializeField] private Color wrongColor;
    [SerializeField] private Color partialColor;

    public Color GetCorrectColor()
    {
        return correctColor;
    }

    public Color GetWrongColor()
    {
        return wrongColor;
    }

    public Color GetPartialColor()
    {
        return partialColor;
    }
}
