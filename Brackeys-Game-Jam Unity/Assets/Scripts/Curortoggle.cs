using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curortoggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

    }


    public void activateCursor()
    {
        Cursor.visible = true;
    }

    public void deactivateCursor()
    {
        Cursor.visible = false;
    }
}
