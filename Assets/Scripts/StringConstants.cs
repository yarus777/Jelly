using UnityEngine;
using System.Collections;

public static class StringConstants {
	public enum TextType
	{
		Record,
		NoRecord,
		NewRecord,
		Level,
		ReachPoints,
		ReachSaveJelly,
		ReachCleanChocolate,
		ReachLowerPot,
		ReachFillBags,
		ReachFillBucket,
		ReachCleanCoockie,
		Moves,
		Points,
		Save,
		Chocolate,
		Pot,
		Bags,
		Bucket,
		Cookie,
		Score,
		Settings,
		Sounds,
		Music,
		Credits,
		Play,
		Count,
		NotMoves,
		NotMovesText,
		Buy,
		GiveUp,
		NotLife,
		NotLifeText,
		Refill,
		Exit,
		Yes,
		No,
		LoseText,
		WinText,
		Scored,
		ContinueTap,
		ShareFB,
		FBLogin,
		FBLogout,
		RateUs,
		Later,
		Again,
		Store,
		Inventory,
		GetOneLife,
		WinTextCool,
		WinTextSuper,
		WinTextGreat,
		WinTextSplwndid,
		WinTextAmaziing,
		WinTextBrilliant,
		WinTextAwesome,
		Pause,
		NoVideo,
		NoNetwork,
        UnlockName,
        UnlockText,
        UnlockText2
	}

	public enum Language
	{
		English,
		Russian
	}

	public enum Level
	{
		First,
		Second,
		Fourth,
		Five,
		Six,
		Seven,
		Eight,
		Teen,
		Fifteen,
		TwentyOne,
		ThirtyOne,
		FortyOne,
		FortyFive,
		FiftiOne,
        Achivements
	}

	public static string[,] textResult = 
	{
		{"Record", "Рекорд"},
		{"No record", "Нет рекорда"},
		{"New record", "Новый рекорд"},
		{"Level","Уровень"},
		{"Get points","Наберите очки"},
		{"Save jellies","Спасите желе"},
		{"Get rid of water","Уберите воду"},
		{"Get pots","Опустите пончики"},
		{"Fulfil bags","Наполните сумки"},
		{"Fulfil cups","Наполните кружки"},
		{"Destroy ice ","Уничтожьте лёд"},
		{"Moves","Ходы"},
		{"Score", "Очки"}, // очки замена
		{"Saved","Спасено"},
		{"Pools","Вода"},
		{"Donuts","Пончики"},
		{"Bags","Сумки"},
		{"Сups","Кружки"},
		{"Ice", "Лёд"},
		{"Points","Очки"},
		{"Settings", "Настройки"},
		{"Sounds", "Звуки"},
		{"Music", "Музыка"},
		{"Credits", "О нас"},
		{"Play", "Начать"},
		{"Count", "Количество"},
		{"No moves left", "У вас не осталось \nходов"},
		{"No moves?\nWatch the video to continue playing!", "Закончились ходы?\nПосмотри видео и получи еще!"},
		{"Buy", "Купить"},
		{"Give up", "Сдаться"},
		{"No energy left","У вас нет энергии"},
		{"Next energy \nin","Следующая энергия \nчерез"},
		{"Refill","Пополнить"},
		{"Do you want to quit?", "Вы хотите выйти?"},
		{"Yes", "Да"},
		{"No", "Нет"},
		{"You lost","Вы проиграли"},
		{"You won!", "Вы победили!"},
		{"Scored", "Набрано"},
		{"Tap the screen to continue","Нажмите на экране для продолжения"},
		{"Jelly - like us!","Желе - лайкни нас!"},
		{"Login","Войти"},
		{"Logout","Выйти"},
		{"Rate us!", "Оцените нас!"},
		{"Later", "Позже"},
		{"Try again!","Попробуйте еще раз!"},
		{"Store","Магазин"},
		{"Inventory","Инвентарь"},
		{"Get \none energy","Получить \nодну энергию"},
		{"Cool!", "Круто!"},
		{"Super!", "Классно!"},
		{"Great!", "Вот это да!"},
		{"Splendid!", "Идеально!"},
		{"Amazing!", "Восхитительно!"},
		{"Brilliant!", "Великолепно!"},
		{"Awesome!", "Потрясающе!"},
		{"Pause","Пауза"},
		{"No available videos","Нет доступных видео"},
		{"Check your internet\n connection","Проверьте подключение\n к интернету"},
        {"Gate is closed","Ворота закрыты"},
        {"The gate will be opened\nthrough","Ворота откроются через"},
        {"Collect more stars\nconnecting jellies\nto open it now!","Заработай больше звёзд\nсобирая желе!"}
	};

//	public static string scored = "Scored ";
	static string[,] updateApp = {
		{"New version is available!\n Download it here!","Доступна новая версия\nОбновитесь здесь!"},
		{"Later","Позже"},
		{"Update","Обновить"}
	};
	public static string[,] ShareText = {
		{"Dive into exciting Sweet adventures in Jelly Monsters app!","Окунись в невероятные приключения по сладкой стране с игрой Jelly Monsters!"},
	};
	public static string GetMenuShareText () {
		switch(Application.systemLanguage)
		{
		case SystemLanguage.Russian:
			return ShareText[0,(int)(Language.Russian)];
		default:
			return ShareText[0,(int)(Language.English)];
		}
	}
	public static string ShareTextToWin(int level, int coins){
		switch(Application.systemLanguage)
		{
		case SystemLanguage.Russian:
			return "Я набрал "+coins+" очков в "+ level + " уровне приложения Jelly Monsters! А у тебя так получится?";
		default:
			return "Look! I've "+coins+" points in level "+ level + " of Jelly Monsters game! Can you beat my record?";
		}
	}
	public static string GetUpdateAppText (int id) {
		switch(Application.systemLanguage)
		{
		case SystemLanguage.Russian:
			return updateApp[id,(int)(Language.Russian)];
		default:
			return updateApp[id,(int)(Language.English)];
		}
	}

	#region Tutorials
	public static string[,] level1 = {
		{
			"Link 3 jelly monsters \ntogether!",
			"More monsters – more \npoints!",
			"Your goal is scores.",
			"Here are your stars. \nGet stars by getting \nmore points!",
			"Here are your points. \nAchieve higher results at one \nlevel to get more of them!",
			"Gain 10000 points to win. \nGood luck!"
		},
		{
			"Соедините 3 желейных \nмонстра!",
			"Чем больше монстров - \nтем больше очков!",
			"Ваша цель - набрать \n10000 очков!",
			"Здесь ваши звезды. \nКоличество звезд \nзависит от очков!",
			"Здесь отображаются \nваши очки. \nУстанавливайте \nрекорды в уровне!",
			"Наберите 10000 очков \nдля победы. \nУдачи!"
		}
	};
	
	public static string[,] level2 = {
		{
			"Here are your moves. \nYou’ll lose if there are \nno more moves!",
			"Gain 15000 points in \n5 moves to win. \nGood luck!"
		},
		{
			"Здесь ваши ходы. \nВы проиграете, если \nони закончатся!",
			"Наберите 15000 очков за \n5 ходов для победы в этом уровне. \nУдачи!"
		}
	};
	
	public static string[,] level4 = {
		{
			"Link 5 jellies together \nto get the bomb!",
			"Blast the bomb by \nlinking it with jellies \nof the same color!",
			"You’ll get 1 bomb \nfor 5-6 jellies!",
			"Bombs blow up jellies \naround them.",
			"Bombs are for you to \nhelp to pass a level. \nGood luck!"
		},
		{
			"Соедините 5 желе, и \nполучите бомбу!",
			"Взорвите бомбу, \nсоеденив с желе \nтакого же цвета!",
			"Вы будете получать \nбомбу за 5 или 6 \nмонстров одного цвета!",
			"Бомбы уничтожают \nжеле вокруг \nсебя.",
			"Бомбы помогут \nвам проходить уровни. \nУдачи!"
		}
	};
	public static string[,] level5 = {
		{
			"Link 7 jellies together \nto get 1 jewel!",
			"Blow up jewel by \nlinking jellies of \nthe same color!",
			"You’ll get 1 jewel \nfor 7 or 8 jellies!",
			"Jewel wipes away \nall jellies from above, \nbelow, left and right.",
			"Jewels will help \nyou to pass a level. \nGood luck!",
		},
		{
			"Соедините 7 желе, и \nполучите алмаз!",
			"Взорвите алмаз, \nсоеденив с желе \nтакого же цвета!",
			"Вы получите алмаз \nсоединив 7 или 8 желе!",
			"Алмаз уничтожает \nвсех монстров сверху, \nснизу, слева и справа.",
			"Алмазы помогут \nвам набрать больше очков. \nУдачи!"
		}
	};
	
	public static string[,] level6 = {
		{
			"You’ve got more moves. \nWait for Bonus Time!",
			"Each left move will give \nyou jewel which will \nbe blown out. \nMore moves - more points!"
		},
		{
			"У вас еще остались \nходы. \nБудет Bonus time!",
			"На каждый оставшийся \nход будет создан и \nвзорван алмаз! \nЧем больше ходов, тем больше очков!"
		}
	};
	
	public static string[,] level7 = {
		{
			"There is the flood \nin the Jelly country! \nLink monsters to remove \nwater under them!",
			"Keep removing \nwater to win. \nGood luck!"
		},
		{
			"В желейной стране потоп! \nЧтобы монстры убрали \nлужи, соедините их",
			"Уберите всю \nводу для победы. \nУдачи!",
		}
	};
	
	public static string[,] level8 = {
		{
			"Attention! Obstacles. \nJellies should \novercome them!"
		},
		{
			"Это препятствия. \n Желе их \nобходят!",
		}
	};
	
	public static string[,] level10 = {
		{
			"Link 9 or more jellies \nto get colored jewel!",
			"This is a colored jewel. \nIt removes all jellies of \nchosen color!",
			"Colored jewel color will be \nchanged and reflected here \nand at top!",
			"Color is saved and blinks \nwhile chosing next \nelement!",
			"Colored jewel may be linked \nwith jellies of any color!",
			"Colored jewel destroys \njellies of chosen \ncolor!",
			"Colored jewels will help \nyou to pass a level. \nGo ahead!"
		},
		{
			"Соедините 9 или более \nжеле и получите \nсамоцвет!",
			"Это самоцвет. \nОн удаляет всех монстров \nвыбранного цвета!",
			"Цвет самоцвета будет \nменяться и отображаться \nздесь и вверху!",
			"При выборе следующего \nэлемента цвет \nсохраняется и моргает!",
			"Самоцвет можно \nсоединить с любым \nцветом!",
			"Самоцвет \nуничтожает желе \nвыбранного цвета!",
			"Самоцветы помогут \nвам набрать как \nможно больше очков. \nВперед!"
		}
	};
	
	public static string[,] level15 = {
		{
			"Jelly Monsters \nwere locked in \nthe bubble. \nRescue them!",
			"Single out \nneighbor monsters \nto help jellies in bubbles!",
			"Rescue all other \njellies out of bubble!"
		},
		{
			"Желе в пузыре. \nСпасите их!",
			"Выделите соседних монстров, \nчтобы помочь желейкам в пузыре!",
			"Спасите остальных \nжеле!"
		}
	};
	
	public static string[,] level21 = {
		{
			"Get the donut down to the \nlower line and it will \nget wiped out!",
			"You goal is donuts. \nThey will appear on the \ntop themselves!",
			"Get all donuts down. \nGood luck!"
		},
		{
			"Опустите пончик в \nнижнюю строчку и \nон удалиться!",
			"Цель уровня - опустить\n все пончики. \nОни сами будут \nпоявляться сверху!",
			"Опустите все пончики \nвниз. \nУдачи!"
		}
	};
	
	public static string[,] level31 = {
		{
			"Highlight jellies to make \nthe ice crack!",
			"Hit the ice for 5 times \nto straight away. \nRemove them off!",
			"Destroy all ice cubes to win \nthe level. \nGo ahead!"
		},
		{
			"На поле лед! \nПомогите монстрам его убрать \nсоедините цепочку с монстрами, \nстоящими на льду",
			"Лед пропадает не сразу! \nУбрать с поля лед \nможно ударив его 5 раз.!",
			"Уберите весь лед \nдля победы в уровне. \nУдачи!"
		}
	};
	
	public static string[,] level41 = {
		{
			"This is a bag. It is \nfor jellies!",
			"Link 2 or more jellies \ntogether with a bag of \nthe same color!",
			"Bag is being wiped away \nthanks to linking \nenough of jellies!",
			"Destroy all the bags \nto win. \nGood luck!"
		},
		{
			"Это сумка. В неё \nнужно собирать \nжеле!",
			"Соедините с ней 3 и \nболее желе \nтакого же цвета!",
			"Соедини указанное на сумке \nколичество желе и она пропадет!",
			"Наполните все \nсумки для победы. \nУдачи!"
		}
	};
	
	public static string[,] level45 = {
		{
			"They are Angry jellies. If \nyou don’t remove them, \nthey will spread out!",
			"Destroy the ice!",
			"You see? Monster made \nthis jelly angry!",
			"Demolish him!",
			"Don’t let monsters spread \naround the field!",
			"Achieve level’s aim. \nGood luck!"
		},
		{
			"Это Злые желейки. Если их \nне усмирить они \nзлят соседние желе!",
			"Уничтожьте \nльдинки!",
			"О нет! \nОн разозлил желе!",
			"Уберите злого желе \nс поля!",
			"Не давайте монстрам \nзаполнять поле!",
			"Выполните цель \nуровня. \nУдачи!"
		}
	};
	
	public static string[,] level51 = {
		{
			"These are cups. \nYou gotta put jellies of the \nsame color into them.",
			"Drop shown amount of jellies \ninto the cup.",
			"Get rid of jellies \nto do so.",
			"Jellies are pulled down when \nthey are above the cup \nof the same color.",
			"Keep dropping jellies \ninto cup. \nGood luck!"
		},
		{
			"Это кружки. В них нужно \nопускать желе \nих цвета.",
			"Опустите указанное \nколичество желе \nв кружку.",
			"Для этого уберите \nжеле.",
			"Желе попадают в кружку \nсвоего цвета, \nкогда находятся над ней.",
			"Наполните все кружки! \nУдачи!"
		}
	};

    public static string[,] achivements = {
		{
			"These are cups. \nYou gotta put jellies of the \nsame color into them.",
			"Keep dropping jellies \ninto cup. \nGood luck!",
            "These are cups. \nYou gotta put jellies of the \nsame color into them.",
			"Keep dropping jellies \ninto cup. \nGood luck!"
		},
		{
			"Это кружки. В них нужно \nопускать желе \nих цвета.",
			"Наполните все кружки! \nУдачи!",
            "Это кружки. В них нужно \nопускать желе \nих цвета.",
			"Наполните все кружки! \nУдачи!"
		}
	};
	
	#endregion


	public static string[,] winLevel = {
		{
			"Sweetacular!",
			"Delicandible!",
			"Lollipopping!",
			"Greatastiful!",
			"Jellicious!",
			"When you better jellify – to \nnew level quicker fly!",
			"Jellific!"
		},
		{
			"Круто!",
			"Круто!",
			"Круто!",
			"Круто!",
			"Круто!",
			"Круто!",
			"Круто!"
		}
	};

	public static string[] startLevel = {
		"Just Jelly it!",
		"Jellify!",
		"Reduce it to jelly!"
	};

	public static string GetText(TextType type)
	{
		switch(Application.systemLanguage)
		{
			case SystemLanguage.Russian:
				return textResult[(int)type,(int)(Language.Russian)];
			default:
				return textResult[(int)type,(int)(Language.English)];
		}
	}

	public static string GetTextTutorial(Level numberLevel, int numberString)
	{
		int language = 0;
		switch(Application.systemLanguage)
		{
			case SystemLanguage.Russian:
				language = (int)(Language.Russian);
//				language = (int)(Language.English);
				break;
			default:
				language = (int)(Language.English);
				break;
		}

		switch(numberLevel)
		{
			case Level.First:
				return level1[language, numberString];
			case Level.Second:
				return level2[language, numberString];
			case Level.Fourth:
				return level4[language, numberString];	
			case Level.Five:
				return level5[language, numberString];	
			case Level.Six:
				return level6[language, numberString];
			case Level.Seven:
				return level7[language, numberString];
			case Level.Eight:
				return level8[language, numberString];
			case Level.Teen:
				return level10[language, numberString];
			case Level.Fifteen:
				return level15[language, numberString];
			case Level.TwentyOne:
				return level21[language, numberString];
			case Level.ThirtyOne:
				return level31[language, numberString];
			case Level.FortyOne:
				return level41[language, numberString];
			case Level.FortyFive:
				return level45[language, numberString];
			case Level.FiftiOne:
				return level51[language, numberString];
            case Level.Achivements:
                return achivements[language, numberString];
			default:
				return "";
		}
	}

	public static string GetWinText(int str)
	{
		int language = 0;
		switch(Application.systemLanguage)
		{
			case SystemLanguage.Russian:
				language = (int)(Language.Russian);
//				language = (int)(Language.English);
				break;
			default:
				language = (int)(Language.English);
				break;
		}

		return winLevel [language, str];
	}

	public static int GetCountWinText()
	{
		return 7;
	}


	public static string GetStartText(int str)
	{
		return startLevel [str];
	}
	
	public static int GetCountStartText()
	{
		return startLevel.Length;
	}

}
