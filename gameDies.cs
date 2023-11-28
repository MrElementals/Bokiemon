using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameDies : MonoBehaviour
{
    void Update()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
