using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class showHint : MonoBehaviour
{
    public SpawnVragen questionInfo;
    public TextMeshProUGUI hintText;

    public void hintPressed()
    {
        if (questionInfo.questionCounter - 1 >= 0)
        {
            hintText.text = questionInfo.listVragen[questionInfo.questionCounter - 1].hint;
        }
        
    }
}
