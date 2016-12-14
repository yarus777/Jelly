namespace Assets.Scripts {
    using UnityEngine;

    public enum WhatText
    {
        Settings,
        Music,
        Sounds,
        TutorialText1,
        TutorialText2,
        ExitText,
        YesTxt,
        NoTxt,
        PlayTxt,
        RateUsTxt,
        NoEnergyTxt,
        NextEnergyTxt,
        GatesTitleTxt,
        GatesTxt1,
        GatesTxt2,
        min30Txt,
        ShareTxt,
        LevelTxt,
        RecordTxt,
        NoRecordTxt,
        ReachGetPoints,
        ReachSaveJelly,
        ReachWaterOut,
        ReachPotsOut,
        ReachFillBags,
        ReachFillCups,
        ReachDestroyIce,
        Count,
        Moves,
        NoNetwork,
        NoVideos,
        PauseTxt,
        MovesTxt,
        ScoreTxt,
        LoseTxt1,
        LoseTxt2,
        GiveUpTxt,
        NoMovesTitleTxt,
        NoMovesTxt,
        ScoredTxt,
        NewRecordTxt
    }

    public enum Language {
        English,
        Russian
    }

    public static class Texts
    {
        private static string[,] value = {
            {"Settings","Настройки"},
            {"Music","Музыка"},
            {"Sounds","Звуки"},
            {"Some text, Some text, Some text, Some text, Some text","Какой-то текст, Какой-то текст, Какой-то текст, Какой-то текст, Какой-то текст"},
            {"Some text1, Some text1, Some text1, Some text1, Some text1","Какой-то текст1, Какой-то текст1, Какой-то текст1, Какой-то текст1, Какой-то текст1"},
            {"Do you want to quit?", "Вы хотите выйти?"},
            {"Yes", "Да"},
		    {"No", "Нет"},
            {"Play", "Начать"}, 
            {"Rate us!", "Оцените нас!"},
            {"No energy left","У вас нет энергии"},
            {"Next energy in","Следующая энергия через"},
            {"Gate is closed","Ворота закрыты"},
            {"The gate will be opened through","Ворота откроются через"},
            {"Collect more stars connecting jellies to open it now!","Заработай больше звёзд собирая желе!"},
            {"- 30 min","- 30 мин"},
            {"Dive into exciting Sweet adventures in Jelly Monsters app!","Окунись в невероятные приключения по сладкой стране с игрой Jelly Monsters!"},
            {"Level","Уровень"},
            {"Record", "Рекорд"},
		    {"No record", "Нет рекорда"},
            {"Get points","Наберите очки"},
		    {"Save jellies","Спасите желе"},
		    {"Get rid of water","Уберите воду"},
		    {"Get pots","Опустите пончики"},
		    {"Fulfil bags","Наполните сумки"},
		    {"Fulfil cups","Наполните кружки"},
		    {"Destroy ice ","Уничтожьте лёд"},
            {"Count", "Количество"},
            {"Moves","Ходы"},
            {"Check your internet connection","Проверьте подключение к интернету"},
            {"No available videos","Нет доступных видео"},
            {"Pause","Пауза"},
            {"Moves","Ходы"},
		    {"score", "очки"},
            {"You lost","Вы проиграли"},
            {"Try again!","Попробуйте еще раз!"},
            {"Give up", "Сдаться"},
            {"No moves left", "У вас не осталось ходов"},
            {"No moves? Watch the video to continue playing!", "Закончились ходы? Посмотри видео и получи еще!"},
            {"Scored", "Набрано"},
            {"New record", "Новый рекорд"}
        };

        public static string GetText (WhatText what)
        {
            return value [(int)what, (int)LangItem.language];
        }
    }

    public class LangItem : MonoBehaviour {
        public WhatText whatIsThis;
        public string textBefore, textAfter;
        public UnityEngine.UI.Text textUI;
        public static Language language {
            get {
                switch (Application.systemLanguage) {
                    case SystemLanguage.Russian:
                        return Language.Russian;
                    default:
                        return Language.English;
                }
            }
        }
        private static System.Collections.Generic.List<LangItem> all;
        public static void UpdateAll () {
            if (all != null) {
                foreach (LangItem item in all) {
                    item.Change ();
                }
            }
        }
        private static void AddToAll (LangItem item) {
            if (all == null) {
                all = new System.Collections.Generic.List<LangItem> ();
            }
            all.Add (item);
        }
        private static void RemoveFromAll (LangItem item) {
            if (all != null) {
                all.Remove (item);
            }
        }

        public void SetText (WhatText what) {
            whatIsThis = what;
            Change ();
        }

        void Change () {
            if (textUI == null)
            {
                return;
                //textUI = this.GetComponent<UnityEngine.UI.Text> ();
            }
            textUI.text = textBefore+Texts.GetText(whatIsThis)+textAfter;
        }

        void OnEnable ()
        {
            AddToAll (this);
            Change ();
        }
        void OnDisable () {
            RemoveFromAll (this);
        }

    }
}