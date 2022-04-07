using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mapbox.Examples;
public class PlayerInQuestionRange : MonoBehaviour
{
    public GameObject questionCanvas;
    public TextMeshProUGUI vraagText, hintText, textBomb;
    public GameObject[] buttonArray;
    public int waypointID;
    int[] array = {-139,-179, -219, -259};
    //yposvan buttons
    public SpawnVragen questionInfo;
    public static int bombHits;

    //AnswerChecker answeredScore;

    //string[] _antw;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        textBomb.enabled = false;
        // Code to execute after the delay
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bomb" && LoadingPanelController.isDone)
        {
            textBomb.enabled = true;
            bombHits++;
            AnswerChecker.score -= 3;
            Debug.Log("Dit is een bom hit log");
            other.gameObject.SetActive(false);
            //ExecuteAfterTime(2);

        }

        if (other.gameObject.tag == "marker" && LoadingPanelController.isDone)
        {
            questionCanvas.SetActive(true);
            waypointID = Int16.Parse(other.gameObject.name);
            //Debug.Log(questionInfo.listVragen[waypointID].antwoord1);

            //Randomize button position.
            for (int i = 0; i < array.Length; i++)
            {
                int j = UnityEngine.Random.Range(i, array.Length);
                int t = array[i];
                array[i] = array[j];
                array[j] = t;
            }
            buttonArray[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0,array[0]);
            buttonArray[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, array[1]);
            buttonArray[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, array[2]);
            buttonArray[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, array[3]);
            //Place the question and answers on screen
            buttonArray[0].GetComponentInChildren<TextMeshProUGUI>().text = questionInfo.listVragen[waypointID].juisteAntwoord;
            buttonArray[1].GetComponentInChildren<TextMeshProUGUI>().text = questionInfo.listVragen[waypointID].antwoord1;
            buttonArray[2].GetComponentInChildren<TextMeshProUGUI>().text = questionInfo.listVragen[waypointID].antwoord2;
            buttonArray[3].GetComponentInChildren<TextMeshProUGUI>().text = questionInfo.listVragen[waypointID].antwoord3;
            vraagText.text = questionInfo.listVragen[waypointID].title;

            //interactable
           
                foreach (GameObject button in buttonArray)
                {
                    if (button.GetComponentInChildren<TextMeshProUGUI>().text != null)
                    {
                        button.GetComponent<Button>().interactable = true;
                    }
                }
             
        }
    }


    void OnTriggerExit(Collider other)
    {
        // Disable activity everything that leaves the trigger
        if (other.gameObject.tag == "marker")
        {
            foreach (GameObject button in buttonArray)
            {
                button.GetComponent<Button>().interactable = false;
            }
            questionCanvas.SetActive(false);
           /* for (int i = 0; i < buttonArray.Length; i++)
            {
                    buttonArray[i].interactable = false;
            }*/
            
        }

    }

}
