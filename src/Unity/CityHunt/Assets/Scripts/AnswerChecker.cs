using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerChecker : MonoBehaviour
{
    
    public SpawnVragen questionInfo;
    public static int score;
    PlayerInQuestionRange inRangeInfo;
    public GameObject Player;
    public GameObject questionCanvas;
    public TextMeshProUGUI textScore;

    public void ClickedCorrectAnswer()
    {
        //Player = GameObject.Find(player)
        inRangeInfo = Player.GetComponent<PlayerInQuestionRange>();
        score += questionInfo.listVragen[inRangeInfo.waypointID].points;
        Debug.Log("You have clicked the correct button and gained points! " + score);
        questionCanvas.SetActive(false);
        textScore.text = "Score: " + score.ToString();
    }

    public void ClickedAnswer2()
    {
        Debug.Log("You have clicked the button!");
        questionCanvas.SetActive(false);
    }

    public void ClickedAnswer3()
    {
        Debug.Log("You have clicked the button!");
        questionCanvas.SetActive(false);
    }

    public void ClickedAnswer4()
    {
        Debug.Log("You have clicked the button!");
        questionCanvas.SetActive(false);
    }
}
