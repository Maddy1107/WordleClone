using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordExtractor : MonoBehaviour
{
    public static List<string> GetWords(TextAsset textFile)
    {
        return textFile.text.Split('\n').ToList();
    }
}