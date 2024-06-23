using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(
    fileName = "Lightning Preset",
    menuName = "ScriptableObjects/NewLightningPreset",
    order = 1
)]
public class LightningPreset : ScriptableObject
{
    public Gradient ambientColor;
    public Gradient directionalColor;
    public Gradient fogColor;
}
