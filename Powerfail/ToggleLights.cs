using NewHorizons.Utility;
using UnityEngine;
using System.Collections;

namespace PowerFail;

public class ToggleLights : MonoBehaviour
{
    [SerializeField]
    private NomaiInterfaceSlot onSlot;
    [SerializeField]
    private NomaiInterfaceSlot offSlot;
    [SerializeField]
    public float lerpDuration = 0.25f;

    private Material lightBulbMaterial;
    private GameObject star;
    private GameObject forcefield;
    private bool hasStartRan = false;
    private Coroutine lerpRoutine;

    public void Awake()
    {
        onSlot.OnSlotActivated += OnSlot_OnSlotActivated;
        offSlot.OnSlotActivated += OffSlot_OnSlotActivated;
    }

    public void Start()
    {
        lightBulbMaterial = SearchUtilities.Find("LoftedLightbulb_Body/Sector/Lightbulb_OWLK/Lightbulb_OWLK/Sphere").GetComponent<MeshRenderer>().material;
        star = SearchUtilities.Find("LoftedLightbulb_Body/Sector/Star");
        forcefield = SearchUtilities.Find("Maze_Body/Sector/Maze/Forcefields");
        hasStartRan = true;
    }

    private void OnSlot_OnSlotActivated(NomaiInterfaceSlot slot)
    {
        if (!hasStartRan)
        {
            return;
        }
        star.SetActive(true);
        forcefield.SetActive(true);
        StartLerp(true);
    }

    private void OffSlot_OnSlotActivated(NomaiInterfaceSlot slot)
    {
        if (!hasStartRan)
        {
            return;
        }
        star.SetActive(false);
        forcefield.SetActive(false);
        //Locator.GetShipLogManager().RevealFact("POWERFAIL_SWITCH"); //Replace with your own shiplog reveal!
        StartLerp(false);
    }

    private void StartLerp(bool toOn)
    {
        if (lerpRoutine != null) StopCoroutine(lerpRoutine);

        Color fromColor = lightBulbMaterial.GetColor("_ParticlesColor");

        Color toColor = toOn ? Color.white : Color.black;

        lerpRoutine = StartCoroutine(LerpLight(fromColor, toColor));
    }

    private IEnumerator LerpLight(Color fromColor, Color toColor)
    {
        float t = 0f;

        while (t < lerpDuration)
        {
            t += Time.deltaTime;
            float lerpT = Mathf.Clamp01(t / lerpDuration);
            lightBulbMaterial.SetColor("_ParticlesColor", Color.Lerp(fromColor, toColor, lerpT));

            yield return null;
        }

        lightBulbMaterial.SetColor("_ParticlesColor", toColor);
        lerpRoutine = null;
    }
}
