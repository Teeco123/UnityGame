using UnityEngine;

[ExecuteInEditMode]
public class LightningManager : MonoBehaviour
{
    [SerializeField]
    private Light directionalLight;

    [SerializeField]
    private LightningPreset preset;

    [SerializeField, Range(0, 24)]
    private float timeOfDay;

    [SerializeField, Range(0, 1)]
    private float speedOfTime = 0.0166666666666667f; //1 minute  = 1 hour in game

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

        if (directionalLight != null)
        {
            //Set color of the sun & rotates sun based on time
            directionalLight.color = preset.directionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(
                new Vector3((timePercent * 360f) - 90f, -170, 0)
            );
        }
    }

    private void OnValidate()
    {
        //Check if directional light is set up
        if (directionalLight != null)
        {
            return;
        }

        //Makes directional light a sun
        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();

            //If no sun is found search for first directional light and set it as a sun
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}