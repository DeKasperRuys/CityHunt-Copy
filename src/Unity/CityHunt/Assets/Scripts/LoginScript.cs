using Mapbox.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{

    public GameObject email;
    public GameObject password;
    public GameObject login;

    public Button btnLogin;

    public TextMeshProUGUI loadingText;

    public static List<Users> loggedinUserInfo = new List<Users>();

    

    private string strEmail;
    private string strPassword;

    private string strPostEmail;


    public static int loggedInuserId;
    
    public static string loggedInUsernaam;
    public static string loggedInUserachternaam;
    public static string loggedInUserusername;
    public static string loggedInUseremail;
    public static string loggedInUserpasswoord;
    public static string loggedInUserlat;
    public static string loggedInUserlong;
    public static bool loggedInUserisGedetineerde;
    public static int loggedInUserteamId;
    public static string loggedInUserteam;
    public static string UserInfoUserId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set strings
        strEmail = email.GetComponent<InputField>().text;
        strPassword = password.GetComponent<InputField>().text;

        //btnLogin = login.GetComponent<InputField>();


    }
    public void StartLogin()
    {
        if (strEmail != "" && strPassword != "")
        {
            StartCoroutine(CallLogin(strEmail, strPassword));
            
            loadingText.gameObject.SetActive(true);
            //
            // Debug.Log("login success");
            // SceneManager.LoadScene(0);
        }
        else
        {

        }
        //btnLogin.onClick.AddListener(validateLogin);
        
    }
 
    public IEnumerator CallLogin(string strEmail, string strPassword)
    {
        //jsonPostString
        string naam = @"{""email"":""" + strEmail+ @""",""passwoord"":"""+strPassword+@"""}";

        //POST Request + Json to bytes
        var www = new UnityWebRequest("https://webapplication2-vx3.conveyor.cloud/api/users/login", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(naam);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");


        Debug.Log(naam);
        www.certificateHandler = new CertHandler();
        yield return www.SendWebRequest();
        
        if (www.error != null)
        {
            Debug.Log("Error" + www.error);
            loadingText.text = "Foutieve invoer";
        }
        else
        {
            Debug.Log("Response" + www.downloadHandler.text);
            StartCoroutine(CallUserInfo(strEmail));
        }
    }

    public IEnumerator CallUserInfo(string strEmail)
    {
        strPostEmail = "\"" + strEmail + "\"";

        var www2 = new UnityWebRequest("https://webapplication2-vx3.conveyor.cloud/api/users3/email", "POST");

        byte[] jsonToSend2 = new System.Text.UTF8Encoding().GetBytes(strPostEmail);
        www2.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend2);
        www2.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www2.SetRequestHeader("Content-Type", "application/json");

        www2.certificateHandler = new CertHandler();
        yield return www2.SendWebRequest();

        if (www2.error != null)
        {

            Debug.Log("Error" + www2.error);
            loadingText.text = "Foutieve invoer";
        }
        else
        {
            Debug.Log("Response" + www2.downloadHandler.text);
            handleJson(www2.downloadHandler.text);
            UserInfoUserId = (loggedinUserInfo[0].scoreId).ToString();

            loggedInUsernaam = (loggedinUserInfo[0].naam).ToString();
            loggedInUserachternaam = (loggedinUserInfo[0].achternaam).ToString();
            loggedInUserusername = (loggedinUserInfo[0].username).ToString();
            loggedInUseremail = (loggedinUserInfo[0].email).ToString();
            loggedInUserpasswoord = (loggedinUserInfo[0].passwoord).ToString();
            //loggedInUserlat = (loggedinUserInfo[0].lat);
            //loggedInUserlong = (loggedinUserInfo[0].@long);
            loggedInUserisGedetineerde = (loggedinUserInfo[0].isGedetineerde);
            loggedInUserteamId = (loggedinUserInfo[0].teamId);
            //loggedInUserteam = (loggedinUserInfo[0].team).ToString();
            loggedInuserId = (loggedinUserInfo[0].userId);
    //Debug.Log(loggedinUserInfo[0].naam);
    SceneManager.LoadScene(1);
        }
    }

    private void handleJson(string www2)
    {
        string results = www2;
        string jsonResults = fixJson(results);
        loggedinUserInfo = JsonConvert.DeserializeObject<Users[]>(results).ToList<Users>();
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
