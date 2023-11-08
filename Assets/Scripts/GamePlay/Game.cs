using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private bool _timerIsOn;
    [SerializeField] private float _timerValue;
    [SerializeField] private Text _timerView;

    [Header("Objects")]
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _continueButton, _nextlevelbutton, _restartbutton;
    [SerializeField] private Text Status;
    [SerializeField] private Exit _exitFromLevel;
    
    private float _timer = 0;
    private bool _gameIsEnded = false;

    private void Awake()
    {
        Time.timeScale = 1f;
        _timer = _timerValue;
    }

    private void Start()
    {
        Debug.Log(_exitFromLevel.IsNeedKey);
        if (_exitFromLevel.IsNeedKey)
        {
            _exitFromLevel.Close();
        }
        else
        {
            _exitFromLevel.Open();
        }
    }

    private void Update()
    {
        if(_gameIsEnded)
            return;
        
        TimerTick();
        LookAtPlayerHealth();
        Debug.Log(_exitFromLevel.IsNeedKey);
        if (_exitFromLevel.IsNeedKey)
        {
            LookAtPlayerInventory();
            
        }
        TryCompleteLevel();
    }

    private void TimerTick()
    {
        if(_timerIsOn == false)
            return;
        
        _timer -= Time.deltaTime;
        _timerView.text = $"{_timer:F1}";
        
        if(_timer <= 0)
            Lose();
    }

    private void TryCompleteLevel()
    {
        Debug.Log("open " + _exitFromLevel.IsOpen);
        if(_exitFromLevel.IsOpen == false)
            return;

        //var flatExitPosition = new Vector2(_exitFromLevel.transform.position.x, _exitFromLevel.transform.position.z);
        //var flatPlayerPosition = new Vector2(_player.transform.position.x, _player.transform.position.z);

        Debug.Log("exit " + _exitFromLevel.IsPlayerOnExit);
        if (_exitFromLevel.IsPlayerOnExit)
            Victory();
    }

    private void LookAtPlayerHealth()
    {
        if(_player.IsAlive)
            return;

        Lose();
        Destroy(_player.gameObject);
    }

    private void LookAtPlayerInventory()
    {
        if(_player.HasKey)
            _exitFromLevel.Open();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        _panel.SetActive(true);
        _continueButton.SetActive(true);
        Status.text = "Пауза";
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
        _panel.SetActive(false);
        Status.text = "Пауза";
    }

    public void Victory()
    {
        _gameIsEnded = true;
        _player.Disable();
        _nextlevelbutton.SetActive(true);
        Time.timeScale = 0f;
        _panel.SetActive(true);
        Status.text = "Вы Выйграли";
    }

    public void Lose()
    {
        _gameIsEnded = true;
        _player.Disable();
        _restartbutton.SetActive(true);
        Time.timeScale = 0f;
        _panel.SetActive(true);
        Status.text = "Вы проиграли";
    }
}
