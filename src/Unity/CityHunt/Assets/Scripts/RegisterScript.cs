using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterScript : MonoBehaviour
{

    public GameObject name;
    public GameObject lastName;
    public GameObject userName;
    public GameObject email;
    public GameObject password;
    public GameObject passwordver;


    public Button btnRegister;

    public TextMeshProUGUI txtError;

    private string strname;
    private string strlastName;
    private string struserName;
    private string stremail;
    private string strpassword;
    private string strpasswordver;

    public const string MatchEmailPattern =
           @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
           + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        strname = name.GetComponent<InputField>().text;
        strlastName = lastName.GetComponent<InputField>().text; ;
        struserName = userName.GetComponent<InputField>().text; ;
        stremail = email.GetComponent<InputField>().text; ;
        strpassword = password.GetComponent<InputField>().text; ;
        strpasswordver = passwordver.GetComponent<InputField>().text;

      
    }

    public void StartRegister()
    {
        btnRegister.GetComponentInChildren<TextMeshProUGUI>().text = "Loading...";
        if (strname != "" && strlastName != "" && struserName != "" && stremail != "" && strpassword != "" && strpasswordver != "")
        {
            if (Regex.IsMatch(stremail, MatchEmailPattern) == true)
            {
                if (strpassword == strpasswordver)
                {
                    StartCoroutine(CallRegister(strname, strlastName, struserName, stremail, strpassword));
                }
                else
                {
                    txtError.text = "Fill in the same password";
                    btnRegister.GetComponentInChildren<TextMeshProUGUI>().text = "Register";
                }
                
            }
            else
            {
                txtError.text = "Fill in a correct email";
                btnRegister.GetComponentInChildren<TextMeshProUGUI>().text = "Register";
            }
            
            
        }
        else
        {
            txtError.text = "Fill in all inputs";
            btnRegister.GetComponentInChildren<TextMeshProUGUI>().text = "Register";
        }
    }

    private void validateRegister()
    {
        if (strname != "" && strlastName != "" && struserName != "" && stremail != "" && strpassword != "" && strpasswordver != "" && strpassword == strpasswordver)
        {
            StartCoroutine(CallRegister(strname, strlastName, struserName, stremail, strpassword));
        }

    }

    public IEnumerator CallRegister(string strname, string strlastName, string struserName, string stremail, string strpassword)
    {
        //Debug.Log(strname + "  " + struserName);
        string naam = @"{""naam"":""" + strname + @""",""achternaam"":""" + strlastName + @""",""username"":""" + struserName + @""",""email"":""" + stremail + @""",""passwoord"":""" + strpassword + @""",""teamId"":" + 1 + "}";

        //POST Request + Json to bytes
        var www = new UnityWebRequest("https://webapplication2-vx3.conveyor.cloud/api/users3", "POST");
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
            txtError.text = www.error;
            btnRegister.GetComponentInChildren<TextMeshProUGUI>().text = "Register";

        }
        else
        {
            Debug.Log("Response" + www.downloadHandler.text);
            btnRegister.enabled = false;
            btnRegister.GetComponentInChildren<TextMeshProUGUI>().text = "Register success";
            txtError.text = "";
        }
    }
    public class CertHandler : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
