using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI pauseText;


    public static TextMeshProUGUI timerText;

    private void Start()
    {
        EventManager.Instance.ChangeGameState += togglePauseText;


        timerText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void togglePauseText(GameManager.GameState s)
    {
        if(s == GameManager.GameState.PAUSED)
            pauseText.transform.gameObject.SetActive(true);
        else
            pauseText.transform.gameObject.SetActive(false);
    }
}
