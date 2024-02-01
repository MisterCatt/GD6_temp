using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI pauseText;

    [SerializeField]
    Image rightArrow;

    public static TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText, gameOverScoreText;

    

    private void Start()
    {
        EventManager.Instance.OnChangeGameState += togglePauseText;

        timerText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        scoreText.text = "     " + GameManager.Instance.Score;
        gameOverScoreText.text = "     " + GameManager.Instance.Score;
    }

    void togglePauseText(GameManager.GameState s)
    {
        if(s == GameManager.GameState.PAUSED)
            pauseText.transform.gameObject.SetActive(true);
        else
            pauseText.transform.gameObject.SetActive(false);
    }

}
