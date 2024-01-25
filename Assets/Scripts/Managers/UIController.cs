using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI pauseText;

    [SerializeField]
    Image rightArrow;


    public static TextMeshProUGUI timerText;

    private void Start()
    {
        EventManager.Instance.OnChangeGameState += togglePauseText;
        EventManager.Instance.OnChangeScreen += toggleArrow;


        timerText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void togglePauseText(GameManager.GameState s)
    {
        if(s == GameManager.GameState.PAUSED)
            pauseText.transform.gameObject.SetActive(true);
        else
            pauseText.transform.gameObject.SetActive(false);
    }

    void toggleArrow()
    {
        if(!rightArrow.IsActive())
            rightArrow.gameObject.SetActive(true);
        else 
            rightArrow.gameObject.SetActive(false);
    }


}
