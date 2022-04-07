using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using SimpleJSON;
using System.Text;
using Mapbox.Directions;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SpawnVragen : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    [SerializeField]
    float _spawnScale = 100f;

    [SerializeField]
    GameObject _markerPrefab;

    Vector2d[] _locations;
    List<GameObject> _spawnedObjects;
    public Vragen[] listVragen;
    bool starter;
    public Button Answer1, Answer2, Answer3, Answer4;
    public TextMeshProUGUI Question;
    public int questionCounter = 0;
    public GameObject playerObject;
    public GameObject EndGameScreen;

    public GameObject userScoreInfo;

    void Start()
    {
        
        StartCoroutine(GetText());
        starter = false;
    }

    private void Update()
    {
        if (starter)
        {
            int count = _spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _spawnedObjects[i];
                var location = _locations[i];
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }
        if (starter == false)
        {
            playerObject.SetActive(true);
        }
    }

    public class CertHandler : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }

    IEnumerator GetText()
    {
        //UnityWebRequest www = UnityWebRequest.Get("https://localhost:44313/api/vragen");
        UnityWebRequest www = UnityWebRequest.Get("https://webapplication2-vx3.conveyor.cloud/api/teams3/vragen/" + newGetTeams.teamid);
    
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
        listVragen = JsonConvert.DeserializeObject<Vragen[]>(results);
        //Debug.Log("Response: " + listVragen[0].title);
       //Debug.Log("Response: " + listVragen[1].title);

        _locations = new Vector2d[listVragen.Length];
        _spawnedObjects = new List<GameObject>();
        //var wp = new Vector2d[2];
        for (int i = 0; i < listVragen.Length; i++)
        {
            string latlong = listVragen[i].lat + ", " + listVragen[i].@long;
            _locations[i] = Conversions.StringToLatLon(latlong);
            var prefab = _markerPrefab.GetComponentInChildren<TextMesh>();
            prefab.text = "Question " + (i+1).ToString();//listVragen[i].title;
            
            var instance = Instantiate(_markerPrefab);
            instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            instance.name = /*"Waypoint" +*/ i.ToString();
            instance.tag = "marker";
            instance.AddComponent<SphereCollider>();
            instance.GetComponent<SphereCollider>().radius = 0.5f;
            instance.GetComponent<SphereCollider>().isTrigger = true;
            instance.SetActive(false);
            _spawnedObjects.Add(instance);
            
            //wp[i] = _locations[i].GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
            //var _directionResource = new DirectionResource(instance, RoutingProfile.Driving);
        }
        starter = true;
        doWayPoint();
    }

    public void doWayPoint()
    {
        if (_spawnedObjects.Count > questionCounter)
        {
            _spawnedObjects[questionCounter].SetActive(true);
            
            GameObject prefabAsset = GameObject.Find("Directions");
            DirectionsFactory directions = prefabAsset.GetComponent<DirectionsFactory>();
            directions._waypoints[1] = _spawnedObjects[questionCounter].transform;

            
        }
        if (questionCounter > 0)
        {
            _spawnedObjects[questionCounter - 1].GetComponent<SphereCollider>().isTrigger = false;
            
        }
        if (_spawnedObjects.Count == questionCounter)
        {
            Debug.Log("END GAME GO BACK TO MENU?");
            Debug.Log(LoginScript.UserInfoUserId);
            if (GetHighscores.totalScore < AnswerChecker.score)
            {
                StartCoroutine(CallPostPoints());
            }
            else
            {
                EndGameScreen.active = true;
            }
            

            
        }
        questionCounter++;
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
            EndGameScreen.active = true;
        }
    }

    string fixJson(string value)
    {
        value = "{\"vragen\":" + value + "}";
        return value;
    }

 
}