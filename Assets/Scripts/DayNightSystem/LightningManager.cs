using UnityEngine;

[ExecuteInEditMode]
public class LightningManager : MonoBehaviour
{
    [Header("Sun & Moon")]
    [SerializeField]
    private Light sun;

    [SerializeField]
    private Light moon;

    [Header("Skybox Materials")]
    [SerializeField]
    private Material skyboxDay;

    [SerializeField]
    private Material skyboxNight;

    private Material currentSkybox;

    [Header("Lightning Preset")]
    [SerializeField]
    private LightningPreset preset;

    [Header("Time Management")]
    [SerializeField, Range(0, 24)]
    private float timeOfDay;

    [SerializeField, Range(0, 1)]
    private float speedOfTime = 0.0166666666666667f; //1 minute  = 1 hour in game

    [Header("Fog Density")]
    [SerializeField, Range(0, 1)]
    public float maxFogDensity;

    [SerializeField, Range(0, 1)]
    private float minFogDensity;

    private void Update()
    {
        if (preset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            //Update time and lightning
            timeOfDay += Time.deltaTime * speedOfTime;
            timeOfDay %= 24;
            UpdateLightning(timeOfDay / 24f);
            GenerateFog();
        }
        else
        {
            UpdateLightning(timeOfDay / 24f);
        }
    }

    private void UpdateLightning(float timePercent)
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

            moon.transform.localRotation = Quaternion.Euler(
                new Vector3((timePercent * 360f) - 270f, -170, 0)
            );
        }

        //Changing skyboxmaterial based on time
        //currentSkybox.Lerp(skyboxDay, skyboxNight, timeOfDay / 24f);
        //RenderSettings.skybox = currentSkybox;
    }

    private void GenerateFog()
    {
        //TODO:Randomize fog density that gonna feel smooth
    }

    private void OnValidate()
    {
        //Check if directional light is set up
        if (sun != null & moon != null)
        {
            return;
        }
    }
}
