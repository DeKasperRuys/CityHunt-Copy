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

public class SpawnAbility : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    [SerializeField]
    float _spawnScale = 100f;

    [SerializeField]
    GameObject _abilityPrefab;

    public GameObject playerObject;
    Vector2d[] _abiltyLocations;
    List<GameObject> _spawnedObjects;
    public Ability[] listAbilities;
    bool starter;


    void Start()
    {

        StartCoroutine(GetAbility());
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
                var location = _abiltyLocations[i];
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

    IEnumerator GetAbility()
    {
        //UnityWebRequest www = UnityWebRequest.Get("https://localhost:44313/api/vragen");
        UnityWebRequest www = UnityWebRequest.Get("https://webapplication2-vx3.conveyor.cloud/api/teams3/ability/" + newGetTeams.teamid);

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
        listAbilities = JsonConvert.DeserializeObject<Ability[]>(results);
        //Debug.Log("Response: " + listVragen[0].title);
        //Debug.Log("Response: " + listVragen[1].title);

        _abiltyLocations = new Vector2d[listAbilities.Length];
        _spawnedObjects = new List<GameObject>();
        //var wp = new Vector2d[2];
        for (int i = 0; i < listAbilities.Length; i++)
        {
            //Spawn de bommen hier, abilitytype 1 is een bom
            if (listAbilities[i].latitude != null && listAbilities[i].abilityType == 1)
            {
                string latlong = listAbilities[i].latitude + ", " + listAbilities[i].longitude;
                Debug.Log("Dit is de latlong voor spawnability " + latlong);
                _abiltyLocations[i] = Conversions.StringToLatLon(latlong);
                //var prefab = _markerPrefab.GetComponentInChildren<TextMesh>();
                //prefab.text = "Bomb " + (i + 1).ToString();//listVragen[i].title;
                var instance = Instantiate(_abilityPrefab);
                instance.transform.localPosition = _map.GeoToWorldPosition(_abiltyLocations[i], true);
                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                instance.name = "Bomb " + i.ToString();
                instance.tag = "bomb";
                instance.AddComponent<SphereCollider>();
                instance.GetComponent<SphereCollider>().radius = 0.5f;
                instance.GetComponent<SphereCollider>().isTrigger = true;
                _spawnedObjects.Add(instance);

                //wp[i] = _locations[i].GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
                //var _directionResource = new DirectionResource(instance, RoutingProfile.Driving);
            }

        }
        starter = true;
        
    }


    string fixJson(string value)
    {
        value = "{\"vragen\":" + value + "}";
        return value;
    }


}