using UnityEngine;
using System.Collections;
using Assets.Scripts.Sounds;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour {

    [SerializeField]
    private Slider _musicSlider;
    [SerializeField]
    private Slider _soundsSlider;

    private float _musicRange = 0.2f;

    private StateInterfaceGame _initialState;

    void OnDisable()
    {
        MapController2._isInputEnabled = true;
        GamePlay.enableButtonInterface = true;
        GamePlay.SetInput(true);
       // GamePlay.interfaceGame = _initialState;
        Debug.Log("GamePlay.enableButtonInterface dis" + GamePlay.interfaceGame + " " + GamePlay.interfaceMap);

        if (GamePlay.interfaceMap != StateInterfaceMap.Start)
        {
            GamePlay.stateUI = EUI.Map;
            GamePlay.interfaceMap = StateInterfaceMap.Start;          
        }
        if (GamePlay.interfaceGame == StateInterfaceGame.Settings)
        {
            GamePlay.interfaceGame = StateInterfaceGame.Game;
        }
    }

    void OnEnable()
    {
        MapController2._isInputEnabled = false;
        //_initialState = GamePlay.interfaceGame;
        GamePlay.enableButtonInterface = false;
        GamePlay.SetInput(false);
        Debug.Log("GamePlay.enableButtonInterface en" + GamePlay.interfaceGame);
        _musicSlider.value = MusicManager.Instance.MusicVolume / _musicRange;
        _soundsSlider.value = MusicManager.Instance.SoundVolume;
    }

    public void SetMusicVolume(float volume)
    {
        MusicManager.Instance.MusicVolume = volume * _musicRange;
    }

    public void SetSoundsVolume(float volume)
    {
        MusicManager.Instance.SoundVolume = volume;
    }

    public void Close()
    {
        GamePlay.soundManager.CreateSoundTypeUI(SoundsManager.UISoundType.WindowClose, false);
        Destroy(this.gameObject);
    }
}
