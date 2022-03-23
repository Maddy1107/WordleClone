using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static Action<string> Toast;
    public static Action<int> onReadytoLoadScene;
}
