using Mapbox.Examples;
using Mapbox.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ShowQuestions : MonoBehaviour
{
    public Button Answer1, Answer2, Answer3, Answer4;
    public Button callQuestions;
    public TextMeshProUGUI Question;
    //public int choiceMade;
    //public int teamid;
    //public long myLocationx, myLocationy;
    public int personalScore;
    //public Text currentLocation;
    //public GameObject canvas;
    //double currentLong, currentLat;
    //GroupShower chosenId;
    public Vragen[] vragen;

    // Start is called before the first frame update

    //dit moet veranderen naar If in range...
    void Start()
    {
        //change to chosen team
        StartCoroutine(CallQuestions());
        
    }



    public void StartQuestionSearch()
    {
        callQuestions.onClick.AddListener(searchQuestions);
    }
    private void searchQuestions()
    {
        StartCoroutine(CallQuestions());
    }
    public IEnumerator CallQuestions()
    {
       // UnityWebRequest www = UnityWebRequest.Get("https://localhost:44313/api/team/vragen/1"/* + GroupShower.teamid*/);
        UnityWebRequest www = UnityWebRequest.Get("https://webapplication2-vx3.conveyor.cloud/api/teams3/vragen/1"/* + GroupShower.teamid*/);
    
        www.certificateHandler = new CertHandler();
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
        }

        else
        {
            handleJson(www.downloadHandler.text);
            // Response can be accessed through: request.downloadHandler.text
            // Debug.Log(request.downloadHandler.text);
            // responseText.text = request.downloadHandler.text;
        }
    }

    private void handleJson(string www)
    {
        //var dropdown = transform.GetComponent<Dropdown>();

        string results = www;
        string jsonResults = fixJson(results);
        vragen = JsonConvert.DeserializeObject<Vragen[]>(results);
        //Debug.Log("Voor de vragen");
        //Debug.Log("Response: " + vragen[0].teamId);
        // Debug.Log("Response: " + teams[1].teamId);

        //temporary to show question with button
       /* Answer1.GetComponentInChildren<Text>().text = vragen[1].juisteAntwoord;
        Answer2.GetComponentInChildren<Text>().text = vragen[1].antwoord1;
        Answer3.GetComponentInChildren<Text>().text = vragen[1].antwoord2;
        Answer4.GetComponentInChildren<Text>().text = vragen[1].antwoord3;
        Question.text = vragen[1].title;*/

        //Debug.Log("na showing questions teamid is: " + GroupShower.teamid);
    }
   

    string fixJson(string value)
    {
        value = "{\"vragen\":" + value + "}";
        return value;
    }
    public class CertHandler : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
