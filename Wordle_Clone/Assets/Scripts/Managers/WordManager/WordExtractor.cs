using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordExtractor : MonoBehaviour
{
    public static List<string> words = null;

    public static List<string> GetWords(TextAsset textFile)
    {
        words =  new List<string>(textFile.text.Split('\n'));
        return words;
    }
}
