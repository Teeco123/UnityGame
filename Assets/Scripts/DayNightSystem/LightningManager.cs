using Unity.Mathematics;
using UnityEngine;

[ExecuteInEditMode]
public class LightningManager : MonoBehaviour
{
    [Header("Sun & Moon")]
    public Light sun;

    public Light moon;

    [Header("Skybox Materials")]
    public Material skyboxDay;

    public Material skyboxNight;

    Material currentSkybox;

    [Header("Lightning Preset")]
    public LightningPreset preset;

    [Header("Time Management")]
    public float timeTick;

    [SerializeField, Range(0, 24)]
    public float timeOfDay;

    [SerializeField, Range(0, 24)]
    public float skyboxChangeTime;

    [SerializeField, Range(0, 1)]
    public float speedOfTime = 0.0166666666666667f; //1 minute  = 1 hour in game

    [Header("Fog Density")]
    [SerializeField, Range(0, 1)]
    public float maxFogDensity;

    [SerializeField, Range(0, 1)]
    public float minFogDensity;

    void OnDestroy()
    {
        ES3.Save("timeTick", timeTick);
    }

    void Awake()
    {
        timeTick = ES3.Load("timeTick", 0f);
    }

    void Start()
    {
        if (skyboxDay != null && skyboxNight != null)
        {
            currentSkybox = new Material(skyboxDay);
        }
    }

    void Update()
    {
        if (preset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            //Calculating speed time 1 = 1hour in game
            timeTick += Time.deltaTime * speedOfTime;

            //Update time
            timeOfDay = timeTick;
            timeOfDay %= 24;

            //Update skybox time
            skyboxChangeTime = Mathf.PingPong(timeTick, 24);

            UpdateLightning(timeOfDay / 24f);
            GenerateFog();
        }
        else
        {
            UpdateLightning(timeOfDay / 24f);
        }
    }

    void UpdateLightning(float timePercent)
    {
        //Setting color based on gradients from our preset
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        if (sun != null & moon != null)
        {
            //Set color of the sun & rotates sun based on time
            //sun.color = preset.directionalColor.Evaluate(timePercent);

            sun.transform.localRotation = Quaternion.Euler(
                new Vector3((timePercent * 360f) - 90f, -170, 0)
            );
            Shader.SetGlobalVector("GlobalSunDirection", -sun.transform.forward);

            moon.transform.localRotation = Quaternion.Euler(
                new Vector3((timePercent * 360f) - 270f, -170, 0)
            );
            Shader.SetGlobalVector("GlobalMoonDirection", -moon.transform.forward);
        }

        if (skyboxDay != null & skyboxNight != null)
        {
            //Changing skyboxmaterial based on time
            float materialTimeChange = skyboxChangeTime / 12;
            currentSkybox.Lerp(skyboxNight, skyboxDay, Mathf.PingPong(materialTimeChange, 1));
            RenderSettings.skybox = currentSkybox;
        }
    }

    void GenerateFog()
    {
        //TODO:Randomize fog density that gonna feel smooth
    }

    void OnValidate()
    {
        //Check if directional light is set up
        if (sun != null & moon != null)
        {
            return;
        }

        if (skyboxDay != null & skyboxNight != null)
        {
            currentSkybox = new Material(skyboxDay);
        }
    }
}
