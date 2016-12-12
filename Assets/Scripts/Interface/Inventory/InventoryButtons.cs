using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryButtons : MonoBehaviour {

	public bool inventoryState = false;
	
	public enum StateButton
	{
		Normal,
		Highlight
	}

	public enum Buttons
	{
		Inventory,
		Arrow,
		Bomb,
		Prism,
		Cancel
	}

	public Buttons button;
	
	public List<Sprite> states;
	
	public SpriteRenderer spriteRenderer;

	public GameObject veer;

	void Start()
	{
		if(name == "inventoryButton")
		{
			GamePlay.inventoryCollider = GetComponent<SphereCollider> ();
		}
		spriteRenderer = GetComponent<SpriteRenderer> ();
		UpdateState (StateButton.Normal);
	}
	
	void OnMouseUpAsButton() {
		if(!GamePlay.blockPauseButton)
		{
			UpdateState (StateButton.Normal);
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
			OnClick();
		}
	}

	void OnClick()
	{
		switch(button)
		{
			case Buttons.Inventory:
				InventoryClick();
				break;
			case Buttons.Arrow:
				ArrowClick();
				break;
			case Buttons.Bomb:
				BombClick();
				break;
			case Buttons.Prism:
				PrismClick();
				break;
			case Buttons.Cancel:
				CancelClick();
				break;
		}
	}

	void InventoryClick()
	{
		if(inventoryState)
		{
			veer.SetActive(false);
		}
		else
		{
			veer.SetActive(true);
		}
		inventoryState = !inventoryState;
//		GamePlay.blockPauseButton = inventoryState;
		GamePlay.SetInput(!inventoryState);
	}

	void ArrowClick()
	{
		if(GamePlay.GetCountPU(PU.Arrow)>0)
		{
			CancelInventoryButtons();
			veer.GetComponent<InventoryButtons>().InventoryClick();
			GamePlay.puActive = PU.Arrow;
			GamePlay.SwitchPUsUI(true);

		}
		else
		{

		}
	}

	void BombClick()
	{
		if(GamePlay.GetCountPU(PU.Bomb)>0)
		{
			CancelInventoryButtons();
			veer.GetComponent<InventoryButtons>().InventoryClick();
			GamePlay.puActive = PU.Bomb;
			GamePlay.SwitchPUsUI(true);

		}
		else
		{

		}
	}

	void PrismClick()
	{
		if(GamePlay.GetCountPU(PU.Prism)>0)
		{
			CancelInventoryButtons();
			veer.GetComponent<InventoryButtons>().InventoryClick();
			GamePlay.puActive = PU.Prism;
			GamePlay.SwitchPUsUI(true);

		}
		else
		{

		}
	}

	public void CancelClick()
	{
		spriteRenderer.sprite = states[2];
		Sprite[] newSprites = new Sprite[]{states[0],states[1]};
		states[0] = states[2];
		states[1] = states[3];
		states[2] = newSprites[0];
		states[3] = newSprites[1];
		button = Buttons.Inventory;
		transform.localScale = new Vector3(1f,1f,1f);
		GamePlay.SwitchPUsUI(false);
	}

	void CancelInventoryButtons()
	{
		InventoryButtons inventory = veer.GetComponent<InventoryButtons>();
		inventory.spriteRenderer.sprite = inventory.states[2];
		Sprite[] newSprites = new Sprite[]{inventory.states[0],inventory.states[1]};
		inventory.states[0] = inventory.states[2];
		inventory.states[1] = inventory.states[3];
		inventory.states[2] = newSprites[0];
		inventory.states[3] = newSprites[1];
		inventory.button = Buttons.Cancel;
		inventory.transform.localScale = new Vector3(0.6f,0.6f,1f);
	}
	
	void OnMouseDown()
	{
		if(!GamePlay.blockPauseButton)
		{
			UpdateState (StateButton.Highlight);
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush1, false);
		}
	}
	
	void OnMouseUp()
	{
		if(!GamePlay.blockPauseButton)
		{
			UpdateState (StateButton.Normal);
			GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.ButtonPush2, false);
		}
	}
	
	private void UpdateState(StateButton state)
	{
		spriteRenderer.sprite = states [(int)state];
	}
}
