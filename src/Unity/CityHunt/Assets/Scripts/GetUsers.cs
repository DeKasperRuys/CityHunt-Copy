using Mapbox.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
public class GetUsers : MonoBehaviour
{
    public List<Users> users = new List<Users>();
    public TextMeshProUGUI text;
    GetHighscores highscoreUserIDlist;
    int teller = 0;
    void Start()
    {
        highscoreUserIDlist = this.GetComponent<GetHighscores>();
        StartCoroutine(CallUsers());
    }
    public IEnumerator CallUsers()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://webapplication2-vx3.conveyor.cloud/api/users3");
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
    private void handleJson(string www)
    {
        string results = www;
        string jsonResults = fixJson(results);
        users = JsonConvert.DeserializeObject<Users[]>(results).ToList<Users>();
        users = users.OrderBy(x => x.scoreId).ToList();
        //text.text = users[0].username;



    }
    public void setUsers()
    {
        text.text = "";
        Debug.Log("SETUSERS");
        for (int i = 0; i < highscoreUserIDlist.allScores.Count; i++)
        {
            for (int a = 0; a < users.Count; a++)
            {
                if (highscoreUserIDlist.allScores[i].scoreId == users[a].scoreId)
                {
                    //Debug.Log(users[a].userId);
                    text.text += users[a].naam + "\n";
                }
            }
        }
    }
    string fixJson(string value)
    {
        value = "{\"Users3\":" + value + "}";
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
