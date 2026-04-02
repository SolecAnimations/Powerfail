using UnityEngine;

namespace PowerFail;

public class WarpHandler : MonoBehaviour
{
	private NomaiWarpTransmitter transmitter;
    private string itemName;

    public void Init(NomaiWarpTransmitter transmitter, string itemName)
    {
        this.itemName = itemName;
        this.transmitter = transmitter;
    }

    public void Start() 
	{
        transmitter._alignmentWindow = 0;
	}

	public void Update()
	{
		if (Locator.GetToolModeSwapper().GetItemCarryTool().GetHeldItem() != null)
		{
            if (Locator.GetToolModeSwapper().GetItemCarryTool().GetHeldItem().name == itemName)
            {
                transmitter._alignmentWindow = 360;
            }
            else
            {
                transmitter._alignmentWindow = 0;
                transmitter.CloseBlackHole();
            }
        } else
        {
            transmitter._alignmentWindow = 0;
            transmitter.CloseBlackHole();
        }
	}
}
