using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void BackToMenu()
    {
        Application.LoadLevel("MainScene");
        
    }
    public void ToPlay()
    {
        Application.LoadLevel("SomeGame");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
