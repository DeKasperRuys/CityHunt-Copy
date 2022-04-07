using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using SimpleJSON;
using System.Text;
using Mapbox.Directions;
using TMPro;

public class newGetTeams : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    //public Button btnTeam;
    public TextMeshProUGUI responseText;
    public static int teamid;
    Teams[] teams;
    public Button btnGoMap;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(CallTeams());
        //StartCoroutine(PutInTeam());
    }

    public void Putinteamenzo()
    {
        StartCoroutine(PutInTeam());
    }
    public IEnumerator CallTeams()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://webapplication2-vx3.conveyor.cloud/api/teams3");
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
        dropdown.options.Clear();

        string results = www;
        string jsonResults = fixJson(results);
        teams = JsonConvert.DeserializeObject<Teams[]>(results);
        //Debug.Log("Response: " + teams[0].teamId);
        // Debug.Log("Response: " + teams[1].teamId);
        for (int i = 0; i < teams.Length; i++)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = (teams[i].teamNaam).ToString() });
        }
        dropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData() { text = "Choose Teams" });
        // Select it
        dropdown.GetComponent<TMP_Dropdown>().value = dropdown.GetComponent<TMP_Dropdown>().options.Count - 1;
        // Remove it
        dropdown.GetComponent<TMP_Dropdown>().options.RemoveAt(dropdown.GetComponent<TMP_Dropdown>().options.Count - 1);
        dropdown.onValueChanged.AddListener(delegate { DropDownItemSelected(dropdown); });

        // Debug.Log("Response: " + vraagjes[1].title);

        //_locations = new Vector2d[teams.Length];
        // _spawnedObjects = new List<GameObject>();
        //var wp = new Vector2d[2];
        /*for (int i = 0; i < teams.Length; i++)
        {
            string latlong = vraagjes[i].lat + ", " + vraagjes[i].@long;
            _locations[i] = Conversions.StringToLatLon(latlong);
            var prefab = _markerPrefab.GetComponentInChildren<TextMesh>();
            prefab.text = vraagjes[i].title;
            var instance = Instantiate(_markerPrefab);
            instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            instance.name = "Waypoint" + i;
            _spawnedObjects.Add(instance);

            //wp[i] = _locations[i].GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
            //var _directionResource = new DirectionResource(instance, RoutingProfile.Driving);
        }
        starter = true;*/
    }

    void DropDownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        responseText.text = "Your team: " + dropdown.options[index].text;
        teamid = (dropdown.value) + 1;
        //Debug.Log("teamid is na groushower: " + teamid);
        StartCoroutine(PutInTeam());
        btnGoMap.interactable = true;

    }

    public IEnumerator PutInTeam()
    {
        Debug.Log("AAYYY LMAaaO");
        string naam = @"{
       ""naam"":""" + LoginScript.loggedInUsernaam + @""",
       ""achternaam"":""" + LoginScript.loggedInUserachternaam + @""",
       ""username"":""" + LoginScript.loggedInUsernaam + @""",
       ""email"":""" + LoginScript.loggedInUseremail + @""",
       ""passwoord"":""" + LoginScript.loggedInUserpasswoord + @""",
       ""teamId"": " + teamid + @"
    }";
        Debug.Log("call logininfo " + LoginScript.loggedInUsernaam);
        //responseText.text= (LoginScript.UserInfoUserId).ToString();
        //VERANDER NAAR ONLINE
        var www = new UnityWebRequest("https://webapplication2-vx3.conveyor.cloud/api/users3/updateuser", "PUT");
        //var www = new UnityWebRequest("https://localhost:44313/api/users3/updateuser", "PUT");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(naam);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        www.certificateHandler = new CertHandler();
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Debug.Log("AAYYY LMAaaO");
            Debug.Log("Error: " + www.error);
        }

        else
        {
            //Debug.Log("AAYYY LMAaaO");
            Debug.Log(www.downloadHandler.text);
            // Response can be accessed through: request.downloadHandler.text
            // Debug.Log(request.downloadHandler.text);
            // responseText.text = request.downloadHandler.text;
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
