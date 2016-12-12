using UnityEngine;
using System.Collections;

public class ObjectsPool
{
	public GameObject jellyBlue;
	public GameObject jellyRed;
	public GameObject jellyYellow;
	public GameObject jellyGreen;
	public GameObject jellyFiolet;

	public GameObject animateJellyBlue;
	public GameObject animateJellyRed;
	public GameObject animateJellyYellow;
	public GameObject animateJellyGreen;
	public GameObject animateJellyFiolet;

	public GameObject jellyBlack;

	public GameObject line;

	public GameObject jamFull;

	public GameObject diamond;

	public GameObject fioletBoom;
	public GameObject yellowBoom;
	public GameObject redBoom;
	public GameObject blueBoom;
	public GameObject greenBoom;

	public GameObject blackBoom;

	public GameObject plate;

	public GameObject electroFiolet;
	public GameObject electroYellow;
	public GameObject electroRed;
	public GameObject electroBlue;
	public GameObject electroGreen;

	public GameObject lightning;

	public GameObject emptyBlock;
	public GameObject emptyCell;

	public GameObject jellyStone;


	public GameObject puddleBlue;
	public GameObject puddleRed;
	public GameObject puddleYellow;
	public GameObject puddleGreen;
	public GameObject puddleFiolet;

	public GameObject feed2Blue;
	public GameObject feed2Red;
	public GameObject feed2Yellow;
	public GameObject feed2Green;
	public GameObject feed2Fiolet;

	public GameObject iceFiolet;
	public GameObject iceYellow;
	public GameObject iceRed;
	public GameObject iceBlue;
	public GameObject iceGreen;

	public GameObject snow;

	public GameObject scoreText;

	public GameObject bombFiolet;
	public GameObject bombYellow;
	public GameObject bombRed;
	public GameObject bombBlue;
	public GameObject bombGreen;

	public GameObject boomBomb;

	public GameObject feed2SaveFiolet;
	public GameObject feed2SaveYellow;
	public GameObject feed2SaveRed;
	public GameObject feed2SaveBlue;
	public GameObject feed2SaveGreen;

	public GameObject slime;

	public GameObject prism;

	public GameObject GetObject(ObjectTypes type, Colors color)
	{
		switch (type) 
		{
			case ObjectTypes.Jelly:
				return LoadJelly(color);
			case ObjectTypes.Line:
				return LoadLine(color);
			case ObjectTypes.Jam:
				return LoadJam();
			case ObjectTypes.Diamond:
				return LoadDiamond();
			case ObjectTypes.Boom:
				return LoadBoom(color);
			case ObjectTypes.Plate:
				return LoadPlate();
			case ObjectTypes.BlackJelly:
				return LoadBlackHero();
			case ObjectTypes.Electro:
				return LoadElectro(color);
			case ObjectTypes.Lightning:
				return LoadLightning();
			case ObjectTypes.EmptyBlock:
				return LoadEmptyBlock();
			case ObjectTypes.EmptyCell:
				return LoadEmptyCell();
			case ObjectTypes.StoneJelly:
				return LoadStoneJelly();
			case ObjectTypes.Puddle:
				return LoadPuddle(color);
			case ObjectTypes.Ice:
				return LoadIce(color);
			case ObjectTypes.Snow:
				return LoadSnow();
			case ObjectTypes.ScoreText:
				return LoadScoreText();
			case ObjectTypes.Bomb:
				return LoadBomb(color);
			case ObjectTypes.BoomBomb:
				return LoadBoomBomb(color);
			case ObjectTypes.Feed2:
				return LoadFeed2(color);
			case ObjectTypes.Feed2Save:
				return LoadFeed2Save(color);
			case ObjectTypes.Slime:
				return LoadSlime();
			case ObjectTypes.Prism:
				return LoadPrism();
//			case ObjectTypes.EmptyPlace:
//				return LoadEmptyPlace();
			default:
				return null;
		}
	}

	private	GameObject LoadJelly(Colors color)
	{
		string pathToResources = "Prefabs/Jelly/";

		switch (color) 
		{
			case Colors.Fiolet:
				if(jellyFiolet == null)
				{
					jellyFiolet = Resources.Load<GameObject>(pathToResources + Jellies.JellyFiolet);
				}
				return jellyFiolet;
			case Colors.Yellow:
				if(jellyYellow == null)
				{
					jellyYellow = Resources.Load<GameObject>(pathToResources + Jellies.JellyYellow);
				}
				return jellyYellow;
			case Colors.Red:
				if(jellyRed == null)
				{
					jellyRed = Resources.Load<GameObject>(pathToResources + Jellies.JellyRed);
				}
				return jellyRed;
			case Colors.Blue:
				if(jellyBlue == null)
				{
					jellyBlue = Resources.Load<GameObject>(pathToResources + Jellies.JellyBlue);
				}
				return jellyBlue;
			case Colors.Green:
				if(jellyGreen == null)
				{
					jellyGreen = Resources.Load<GameObject>(pathToResources + Jellies.JellyGreen);
				}
				return jellyGreen;
			default:
				return null;
		}
	}

	private	GameObject LoadLine(Colors color)
	{
		string pathToResources = "Prefabs/Line/";
		if(line == null)
		{
			line = Resources.Load<GameObject>(pathToResources + "Line");
		}
		return line;
	}
	
	private GameObject LoadJam()
	{
		string pathToResources = "Prefabs/";
		if(jamFull == null)
		{
			jamFull = Resources.Load<GameObject> (pathToResources + Jams.JamFull);
		}
		return jamFull;
	}

	private GameObject LoadDiamond()
	{
		string pathToResources = "Prefabs/";
		if(diamond == null)
		{
			diamond = Resources.Load<GameObject> (pathToResources + ObjectTypes.Diamond);
		}
		return diamond;
	}

	private GameObject LoadBoom(Colors color)
	{
		string pathToResources = "Prefabs/Jelly/Boom/";
		switch(color)
		{
			case Colors.Fiolet:
				if(fioletBoom == null)
				{
					fioletBoom = Resources.Load<GameObject>(pathToResources + "FioletBoom");
				}
				return fioletBoom;
			case Colors.Yellow:
				if(yellowBoom == null)
				{
					yellowBoom = Resources.Load<GameObject>(pathToResources + "YellowBoom");
				}
				return yellowBoom;
			case Colors.Red:
				if(redBoom == null)
				{
					redBoom = Resources.Load<GameObject>(pathToResources + "RedBoom");
				}
				return redBoom;
			case Colors.Blue:
				if(blueBoom == null)
				{
					blueBoom = Resources.Load<GameObject>(pathToResources + "BlueBoom");
				}
				return blueBoom;
			case Colors.Green:
				if(greenBoom == null)
				{
					greenBoom = Resources.Load<GameObject>(pathToResources + "GreenBoom");
				}
				return greenBoom;
			case Colors.Black:
				{
					blackBoom = Resources.Load<GameObject>(pathToResources + "BlackBoom");
				}
				return blackBoom;
			default:
				return null;
		}

	}

	public GameObject LoadPlate()
	{
		string pathToResources = "Prefabs/";
		if(plate == null)
		{
			plate = Resources.Load<GameObject> (pathToResources + "Plate");
		}
		return plate;
	}

	public GameObject LoadBlackHero()
	{
		string pathToResources = "Prefabs/Jelly/";
		if(jellyBlack == null)
		{
			jellyBlack = Resources.Load<GameObject> (pathToResources + "JellyBlack");
		}
		return jellyBlack;
	}

	public GameObject LoadElectro(Colors color)
	{
		string pathToResources = "Prefabs/Electro/";
		switch(color)
		{
		case Colors.Fiolet:
			if(electroFiolet == null)
			{
				electroFiolet = Resources.Load<GameObject>(pathToResources + "ElectroFiolet");
			}
			return electroFiolet;
		case Colors.Yellow:
			if(electroYellow == null)
			{
				electroYellow = Resources.Load<GameObject>(pathToResources + "ElectroYellow");
			}
			return electroYellow;
		case Colors.Red:
			if(electroRed == null)
			{
				electroRed = Resources.Load<GameObject>(pathToResources + "ElectroRed");
			}
			return electroRed;
		case Colors.Blue:
			if(electroBlue == null)
			{
				electroBlue = Resources.Load<GameObject>(pathToResources + "ElectroBlue");
			}
			return electroBlue;
		case Colors.Green:
			if(electroGreen == null)
			{
				electroGreen = Resources.Load<GameObject>(pathToResources + "ElectroGreen");
			}
			return electroGreen;
		default: 
			return null;
		}
	}

	public GameObject LoadLightning()
	{
		string pathToResources = "Prefabs/Lightning/";
		if(lightning == null)
		{
			lightning = Resources.Load<GameObject>(pathToResources+"ArrowBoom");
		}
		return lightning;
	}

	public GameObject LoadEmptyBlock()
	{
		string pathToResources = "Prefabs/";
		if(emptyBlock == null)
		{
			emptyBlock = Resources.Load<GameObject>(pathToResources+"EmptyBlock");
		}
		return emptyBlock;
	}

	public GameObject LoadEmptyCell()
	{
		string pathToResources = "Prefabs/";
		if(emptyCell == null)
		{
			emptyCell = Resources.Load<GameObject>(pathToResources+"EmptyCell");
		}
		return emptyCell;
	}

	public GameObject LoadStoneJelly()
	{
		string pathToResources = "Prefabs/Jelly/";
		if(jellyStone == null)
		{
			jellyStone = Resources.Load<GameObject>(pathToResources+"JellyStone");
		}
		return jellyStone;
	}

	public GameObject LoadIce(Colors color)
	{
		string pathToResources = "Prefabs/Ice/";
		switch(color)
		{
			case Colors.Fiolet:
				if(iceFiolet == null)
				{
					iceFiolet = Resources.Load<GameObject>(pathToResources+"IceFiolet");
				}
				return iceFiolet;
			case Colors.Yellow:
				if(iceYellow == null)
				{
					iceYellow = Resources.Load<GameObject>(pathToResources+"IceYellow");
				}
				return iceYellow;
			case Colors.Red:
				if(iceRed == null)
				{
					iceRed = Resources.Load<GameObject>(pathToResources+"IceRed");
				}
				return iceRed;
			case Colors.Blue:
				if(iceBlue == null)
				{
					iceBlue = Resources.Load<GameObject>(pathToResources+"IceBlue");
				}
				return iceBlue;
			case Colors.Green:
				if(iceGreen == null)
				{
					iceGreen = Resources.Load<GameObject>(pathToResources+"IceGreen");
				}
				return iceGreen;
		}
		return null;
	}

	public GameObject LoadSnow()
	{
		string pathToResources = "Prefabs/";
		if(snow == null)
		{
			snow = Resources.Load<GameObject>(pathToResources+"Snow");
		}
		return snow;
	}

	public GameObject LoadScoreText()
	{
		string pathToResources = "Prefabs/";
		if(scoreText == null)
		{
			scoreText = Resources.Load<GameObject>(pathToResources+"ScoreText");
		}
		return scoreText;
	}

	public GameObject LoadPuddle(Colors color)
	{
		string pathToResources = "Prefabs/Puddle/";
		switch (color) 
		{
		case Colors.Fiolet:
			if(puddleFiolet == null)
			{
				puddleFiolet = Resources.Load<GameObject>(pathToResources + "PuddleFiolet");
			}
			return puddleFiolet;
		case Colors.Yellow:
			if(puddleYellow == null)
			{
				puddleYellow = Resources.Load<GameObject>(pathToResources + "PuddleYellow");
			}
			return puddleYellow;
		case Colors.Red:
			if(puddleRed == null)
			{
				puddleRed = Resources.Load<GameObject>(pathToResources + "PuddleRed");
			}
			return puddleRed;
		case Colors.Blue:
			if(puddleBlue == null)
			{
				puddleBlue = Resources.Load<GameObject>(pathToResources + "PuddleBlue");
			}
			return puddleBlue;
		case Colors.Green:
			if(puddleGreen == null)
			{
				puddleGreen = Resources.Load<GameObject>(pathToResources + "PuddleGreen");
			}
			return puddleGreen;
		default:
			return null;
		}
	}

	public GameObject LoadFeed2(Colors color)
	{
		string pathToResources = "Prefabs/Feed2/";
		switch (color) 
		{
		case Colors.Fiolet:
			if(feed2Fiolet == null)
			{
				feed2Fiolet = Resources.Load<GameObject>(pathToResources + "Feed2Fiolet");
			}
			return feed2Fiolet;
		case Colors.Yellow:
			if(feed2Yellow == null)
			{
				feed2Yellow = Resources.Load<GameObject>(pathToResources + "Feed2Yellow");
			}
			return feed2Yellow;
		case Colors.Red:
			if(feed2Red == null)
			{
				feed2Red = Resources.Load<GameObject>(pathToResources + "Feed2Red");
			}
			return feed2Red;
		case Colors.Blue:
			if(feed2Blue == null)
			{
				feed2Blue = Resources.Load<GameObject>(pathToResources + "Feed2Blue");
			}
			return feed2Blue;
		case Colors.Green:
			if(feed2Green == null)
			{
				feed2Green = Resources.Load<GameObject>(pathToResources + "Feed2Green");
			}
			return feed2Green;
		default:
			return null;
		}
	}

	public GameObject LoadBomb(Colors color)
	{
		string pathToResources = "Prefabs/Bombs/";
		switch(color)
		{
			case Colors.Fiolet:
				if(bombFiolet == null)
				{
					bombFiolet = Resources.Load<GameObject>(pathToResources + "BombPurple");
				}
				return bombFiolet;
			case Colors.Yellow:
				if(bombYellow == null)
				{
					bombYellow = Resources.Load<GameObject>(pathToResources + "BombYellow");
				}
				return bombYellow;
			case Colors.Red:
				if(bombRed == null)
				{
					bombRed = Resources.Load<GameObject>(pathToResources + "BombRed");
				}
				return bombRed;
			case Colors.Blue:
				if(bombBlue == null)
				{
					bombBlue = Resources.Load<GameObject>(pathToResources + "BombBlue");
				}
				return bombBlue;
			case Colors.Green:
				if(bombGreen == null)
				{
					bombGreen = Resources.Load<GameObject>(pathToResources + "BombGreen");
				}
				return bombGreen;
			default: 
				return null;
		}
	}

	public GameObject LoadBoomBomb(Colors color)
	{
		string pathToResources = "Prefabs/Bombs/Boom/";
		//color = Colors.Green;
		if(boomBomb == null)
		{
			boomBomb = Resources.Load<GameObject>(pathToResources + "BoomBomb"+color);
		}
		return boomBomb;
	}

	public GameObject LoadFeed2Save(Colors color)
	{
		string pathToResources = "Prefabs/Feed2/Save/";
		switch(color)
		{
		case Colors.Fiolet:
			if(feed2SaveFiolet== null)
			{
				feed2SaveFiolet = Resources.Load<GameObject>(pathToResources + "Feed2SavePurple");
			}
			return feed2SaveFiolet;
		case Colors.Yellow:
			if(feed2SaveYellow == null)
			{
				feed2SaveYellow = Resources.Load<GameObject>(pathToResources + "Feed2SaveYellow");
			}
			return feed2SaveYellow;
		case Colors.Red:
			if(feed2SaveRed == null)
			{
				feed2SaveRed = Resources.Load<GameObject>(pathToResources + "Feed2SaveRed");
			}
			return feed2SaveRed;
		case Colors.Blue:
			if(feed2SaveBlue == null)
			{
				feed2SaveBlue = Resources.Load<GameObject>(pathToResources + "Feed2SaveBlue");
			}
			return feed2SaveBlue;
		case Colors.Green:
			if(feed2SaveGreen == null)
			{
				feed2SaveGreen = Resources.Load<GameObject>(pathToResources + "Feed2SaveGreen");
			}
			return feed2SaveGreen;
		default: 
			return null;
		}
	}

	public GameObject LoadSlime()
	{
		string pathToResources = "Prefabs/Slime/";
		if(slime == null)
		{
			slime = Resources.Load<GameObject> (pathToResources + "Slime");
		}
		return slime;
	}

	public GameObject LoadPrism()
	{
		string pathToResources = "Prefabs/Prism/";
		if(prism == null)
		{
			prism = Resources.Load<GameObject> (pathToResources + "Prism");
		}
		return prism;
	}

//	public GameObject LoadEmptyPlace()
//	{
//		return null;
//	}

	public void ResetData()
	{
		jellyBlue = null;
		jellyRed = null;
		jellyYellow = null;
		jellyGreen = null;
		jellyFiolet = null;
	
		jellyBlack = null;

		animateJellyBlue = null;
		animateJellyRed = null;
		animateJellyYellow = null;
		animateJellyGreen = null;
		animateJellyFiolet = null;

		line = null;

		jamFull = null;

		diamond = null;

		fioletBoom = null;
		yellowBoom = null;
		redBoom = null;
		blueBoom = null;
		greenBoom = null;

		blackBoom = null;

		plate = null;

		electroFiolet = null;
		electroYellow = null;
		electroRed = null;
		electroBlue = null;
		electroGreen = null;

		lightning = null;
		emptyBlock = null;
		emptyCell = null;
		jellyStone = null;

		puddleBlue = null;
		puddleRed = null;
		puddleYellow = null;
		puddleGreen = null;
		puddleFiolet = null;

		feed2Blue = null;
		feed2Red = null;
		feed2Yellow = null;
		feed2Green = null;
		feed2Fiolet = null;

		feed2SaveFiolet = null;
		feed2SaveYellow = null;
		feed2SaveRed = null;
		feed2SaveBlue = null;
		feed2SaveGreen = null;

		iceFiolet = null;
		iceYellow = null;
		iceRed = null;
		iceBlue = null;
		iceGreen = null;

		snow = null;

		scoreText = null;

		bombFiolet = null;
		bombYellow = null;
		bombRed = null;
		bombBlue = null;
		bombGreen = null;

		boomBomb = null;

		slime = null;

		prism = null;
	}
}