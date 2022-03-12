using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiController : MonoBehaviour
{
    [SerializeField] GameObject victoryText;
    [SerializeField] GameObject defeatText;
    [SerializeField] TextMeshProUGUI messageText;
    //Activates Defeat Text
    public void ShowDefeat()
    {
        defeatText.SetActive(true);
    }
    //Activated Victory Text
    public void ShowVictory(string msg)
    {
        messageText.text = msg;
        messageText.gameObject.SetActive(true);
        victoryText.SetActive(true);
    }
    //hides All text
    public void HideAll()
    {
        messageText.gameObject.SetActive(false);
        victoryText.SetActive(false);
        defeatText.SetActive(false);
    }
}
