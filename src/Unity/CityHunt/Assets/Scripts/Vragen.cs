using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;

[SerializeField]
public class Vragen
{

    public int vraagId;
    public string title;
    public string juisteAntwoord;
    public string antwoord1;
    public string antwoord2;
    public string antwoord3;
    public string lat;
    public string @long;
    public string hint;
    public int points;
    public int teamId;
    public object team;

    // Start is called before the first frame update
    void Start()
    {
        string latlong = lat + ", " + @long;
        Vector2d worldLatLong = Conversions.StringToLatLon(latlong);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


