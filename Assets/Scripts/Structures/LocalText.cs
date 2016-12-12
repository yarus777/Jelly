using UnityEngine;
using System.Collections;

public static class LocalText{
	public enum Language
	{
		Russian,
		English
	}

	private static string[] startGame = {"Начало игры","New game"};

	public static string GetStartGame(Language language)
	{
		return startGame [(int)language];
	}
}
