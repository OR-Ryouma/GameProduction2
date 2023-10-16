using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScript : MonoBehaviour
{
    public int _defaultWidth = 1080;
    public int _defaultHeight = 1920;


    // Start is called before the first frame update
    void Awake()
    {
        Screen.SetResolution(_defaultWidth, _defaultHeight, FullScreenMode.Windowed, 60);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
