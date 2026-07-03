using NewHorizons.Utility;
using UnityEngine;

namespace PowerFail;

public class BrotherLights : MonoBehaviour
{
    private GameObject[] objectsToDisable = new GameObject[2];

    public void Start()
    {
        objectsToDisable[0] = SearchUtilities.Find("BruisedBrother_Body/Sector").transform.GetChild(0).gameObject;
        objectsToDisable[1] = SearchUtilities.Find("BruisedBrother_Body/Sector/Atmosphere/Atmosphere");
    }
    public void ToggleLights()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            if (obj.activeSelf)
            {
                obj.SetActive(false);
            } else
            {
                obj.SetActive(true);
            }
            
        }
    }

    public virtual void OnTriggerEnter(Collider hitCollider)
    {
        //checks if player collides with the trigger volume
        if (hitCollider.CompareTag("PlayerDetector") && enabled)
        {
            ToggleLights();
        }
    }
}
