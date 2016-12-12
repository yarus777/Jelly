/// <summary>
/// Типы создаваемых объектов.
/// </summary>
public enum ObjectTypes
{
	Jelly,
	Line,
	Jam,
	Diamond,
	Boom,
	Plate,
	BlackJelly,
	Electro,
	Lightning,
	EmptyBlock,
	EmptyCell,
	StoneJelly,
	Puddle,
	Ice,
	Snow,
	ScoreText,
	Bomb,
	BoomBomb,
	Feed2,
	Feed2Save,
	Slime,
	Prism,
	EmptyPlace,
	Empty
}
/// <summary>
/// Colors.
/// </summary>
public enum Colors
{
	Fiolet,
	Yellow,
	Red,
	Blue,
	Green,
	Black,
	Pink,
	Orange,
	Prism,
	Empty
}
/// <summary>
/// Jelly по цвету.
/// </summary>
public enum Jellies
{
	JellyFiolet,
	JellyYellow,
	JellyRed,
	JellyBlue,
	JellyGreen,
	Empty
}
/// <summary>
/// Lines по цвету.
/// </summary>
public enum Lines
{
	LineFiolet,
	LineYellow,
	LineRed,
	LineBlue,
	LineGreen,
	Empty
}
/// <summary>
/// Картинки джема в зависимости от положения в сцене.
/// </summary>
public enum Jams
{
	JamFull,
	JamTop,
	JamLeft,
	JamAngleLeftTop,
	JamBottom,
	JamSideLeftRight,
	JamAngleLeftBottom,
	JamSideLeft,
	JamRight,
	JamAngleRightTop,
	JamSideTopBottom,
	JamSideTop,
	JamAngleRightBottom,
	JamSideRight,
	JamSideBottom,
	JamBlock,
	Empty
}
/// <summary>
/// State objects.
/// </summary>
public enum StateObjects
{
	Normal,
	Selected,
	DeletingElectro
}
/// <summary>
/// Task.
/// </summary>
public enum Task
{
	Empty,
	Points,
	Save,
	ClearJam,
	Diamond,
	Feed1,
	Feed2,
	Dig
}
/// <summary>
/// Limit.
/// </summary>
public enum Limit
{
	Empty,
	NotLimit,
	Time,
	Moves
}
/// <summary>
/// Line scale.
/// </summary>
public enum LineScale
{
	Straight,
	Diagonal
}
/// <summary>
/// Направление смещения/удаления объектов.
/// </summary>
public enum MoveOffset
{
	Down,
	Left,
	Right, 
	Up
}
/// <summary>
/// Removing direction.
/// </summary>
public enum RemovingDirection
{
	Vertical,
	Horizontal
}

/// <summary>
/// State animation.
/// </summary>
public enum StateAnimation
{
	Forward,
	Back
}

public enum StateInterfaceMap
{
	Start,
	Interface1,
	Interface2,
	StartNextLvl
}

public enum StateInterfaceGame
{
	Game,
	Pause,
	Settings,
	NotLife,
	Store
}

/// <summary>
/// Statement shadow.
/// </summary>
public enum StatementShadow
{
	On,
	Off
}

public enum StateButton
{
	Normal,
	Highlight
}

public enum FBCallback
{
	Empty,
	Login,
	Invite,
	Share
}

public enum Words
{
	Cool,
	Super,
	Great,
	Splendid,
	Amazing,
	Brilliant,
	Awesome,
	Empty
}

public enum OrderLayers
{
	Empty,
	TopPowerUp,
	PowerUp,
	BottomPowerUp,
	TopMainObject,
	MainObject,
	BottomMainObject,
	Line,
	TopBackObject,
	BackObject,
	BottomBackObject,
	Grid,
	Background
}

public enum PrismState
{
	MouseDown,
	MouseUp
}

public enum EInApp
{
	Moves,
	Life,
	GOLDS_50,
	GOLDS_100,
	GOLDS_300,
	GOLDS_500,
	GOLDS_1000,
	GOLDS_7000,
}

public enum EUI
{
	Empty,
	Map,
	Game,
	Loadout,
	Settings,
	NotLife,
	Store,
	Inventory
}

public enum PU
{
	Arrow,
	Bomb,
	Prism,
	Empty
}


//Прототип Debug
//#if Debug
//Debug.Log();
//#endif