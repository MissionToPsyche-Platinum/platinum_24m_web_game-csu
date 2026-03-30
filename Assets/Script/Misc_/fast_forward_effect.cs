using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// public class UIManager : MonoBehaviour{
public class ButtonText : MonoBehaviour{

    public TextMeshProUGUI statusText;

    public void ChangeScreenText(string text){
        text = "1x speed";
       statusText.text = text; 
    }
    
}
