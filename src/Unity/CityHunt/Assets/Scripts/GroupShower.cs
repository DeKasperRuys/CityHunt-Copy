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

public class TeamShower : MonoBehaviour
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
    }


    public void StartTeamSearch()
    {
        //btnTeam.onClick.AddListener(searchTeams);
    }
    private void searchTeams()
    {
       
            



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
        teamid = (dropdown.value) + 1;
        responseText.text = "Chosen team: " + dropdown.options[index].text;
        Debug.Log("teamid is na groushower: " + teamid);

        
        

        btnGoMap.interactable = true;
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
