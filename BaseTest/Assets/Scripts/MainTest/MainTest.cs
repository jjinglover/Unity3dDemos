using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Text _testInputText = null; 
    void Start()
    {
        
    }

    void Update()
    {
        this.checkKeyBoardINput();
    }

    private void checkKeyBoardINput() {
        string inputStr = "";
        if (Input.GetKeyDown(KeyCode.Q)) {
            inputStr = "Q";
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            inputStr = "W";
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            inputStr = "E";
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            inputStr = "R";
        }

        if (inputStr.Length > 0) {
            if (_testInputText) {
                _testInputText.text = "ÄúÊäÈëÁË:" + inputStr;
            }
        }
    }
}
