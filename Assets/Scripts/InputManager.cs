using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public bool mouseUp;

    private void Update()
    {
        
    }
    private void OnMouseUp()
    {
        if (!mouseUp&&enabled)
        {
            mouseUp = true;
        }
    }
}
