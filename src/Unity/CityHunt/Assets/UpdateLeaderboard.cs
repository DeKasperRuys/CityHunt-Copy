using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateLeaderboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void updateLeaderboard()
    {

        StartCoroutine(CallPostPoints());
    }

    public IEnumerator CallPostPoints()
    {
        //jsonPostString

        string postScore = @"{""totalPoints"":" + GetHighscores.totalScore + @",""currentPoints"":" + AnswerChecker.score + @"}";

        //POST Request + Json to bytes
        var www = new UnityWebRequest("https://webapplication2-vx3.conveyor.cloud/api/highscore3/users/" + GetHighscores.userID, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(postScore);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");


        Debug.Log(postScore);
        www.certificateHandler = new CertHandler();
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log("Error" + www.error);

        }
        else
        {
            Debug.Log("Response" + www.downloadHandler.text);
            
        }
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
