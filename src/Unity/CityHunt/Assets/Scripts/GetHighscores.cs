using Mapbox.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GetHighscores : MonoBehaviour
{
    // public Highscore[] scores;
    public List<Highscore> allScores = new List<Highscore>();
    public List<Highscore> playerScore = new List<Highscore>();
    public TextMeshProUGUI HighScoreList, scoreNames;
    public GameObject leaderBoardUI, mainScreen;

    public static int  totalScore;
    public static int currentPoints;
    public static int userID;

    List<Highscore> Highscorelistorder = new List<Highscore>();
    public Users[] listUsers;
    GetUsers getUsers;
    void Start()
    {

        StartCoroutine(CallHighscore());
        StartCoroutine(CallUserHighScore());
    }

    public void startHighscoresCoroutine()
    {
        
    }
    public IEnumerator CallHighscore()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://webapplication2-vx3.conveyor.cloud/api/highscore3");
        www.certificateHandler = new CertHandler();
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
        }

        else
        {
            handleJson(www.downloadHandler.text);
        }
    }

    public IEnumerator CallUserHighScore()
    {
        UnityWebRequest www2 = UnityWebRequest.Get("https://webapplication2-vx3.conveyor.cloud/api/highscore3/users/" + LoginScript.UserInfoUserId);
        www2.certificateHandler = new CertHandler();
        yield return www2.SendWebRequest();
        if (www2.isNetworkError || www2.isHttpError)
        {
            Debug.Log("Error: " + www2.error);
        }

        else
        {
            handleJson2(www2.downloadHandler.text);
        }
    }
    private void handleJson(string www)
    {
        string results = www;
        string jsonResults = fixJson(results);
        allScores = JsonConvert.DeserializeObject<Highscore[]>(results).ToList<Highscore>();
        //Debug.Log(scores[0].currentPoints);
        //Debug.Log(scores[0].totalPoints);
        //scoreNames.text = "";
        //scores.OrderByDescending(x => x.totalPoints).ToList();
        allScores = allScores.OrderByDescending(x => x.currentPoints).ToList();
        
        
    }

    private void handleJson2(string www)
    {
        string results = www;
        string jsonResults = fixJson(results);
        playerScore = JsonConvert.DeserializeObject<Highscore[]>(results).ToList<Highscore>();
        currentPoints = playerScore[0].currentPoints;
        totalScore = playerScore[0].totalPoints;
        userID = playerScore[0].scoreId;

        //Debug.Log("SWAGGGG" + playerScore[0].currentPoints);
        //Debug.Log("SWAGGGG123" + LoginScript.UserInfoUserId);
       
    }
    public void setScores()
    {
        HighScoreList.text = "";
        for (int a = 0; a < allScores.Count; a++)
        {

            //Highscorelistorder = scores[i].totalPoints.to

            //HighScoreList.text += scores[i].totalPoints + "\n";

            HighScoreList.text += allScores[a].currentPoints + "\n";
            

        }
    }

    string fixJson(string value)
    {
        value = "{\"Highscores\":" + value + "}";
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
