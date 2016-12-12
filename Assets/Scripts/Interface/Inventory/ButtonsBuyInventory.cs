using UnityEngine;
using System.Collections;

public class ButtonsBuyInventory : MonoBehaviour {

	public enum Buttons
	{
		Coins_Slot_1,
		Coins_Slot_2,
		Coins_Slot_3
	}
	
	public Buttons button;
	
	public Sprite[] states;
	
	private SpriteRenderer spriteRenderer;
	
	void Awake()
	{
//		GamePlay.buyInventoryUI = this;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		UpdateState (StateButton.Normal);
		spriteRenderer.sharedMaterial.color = new Color(1f,1f,1f,1f);
	}
	
	public void UpdateState(StateButton state)
	{
		spriteRenderer.sprite = states [(int)state];
	}
	
	void OnMouseDown()
	{
		UpdateState (StateButton.Highlight);
		GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
	}
	
	void OnMouseUp()
	{
		UpdateState (StateButton.Normal);
		GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
	}
	
	void OnMouseUpAsButton() {
		if(GamePlay.enableButtonInterface)
		{
			UpdateState (StateButton.Normal);
//			switch(button)
//			{
//				case Buttons.Coins_Slot_1:
//					InAppManager.Buy(EInApp.Bombs);
//					break;
//				case Buttons.Coins_Slot_2:
//					InAppManager.Buy(EInApp.Arrows);
//					break;
//				case Buttons.Coins_Slot_3:
//					InAppManager.Buy(EInApp.Prisms);
//					break;
//			}

		}
	}
}
