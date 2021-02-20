using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // расширение, необходимое для управления сценой

[RequireComponent(typeof(CanvasGroup))]

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Player _player;

    private CanvasGroup _gameOverGroup;

    private void OnEnable()
    {
        _player.Died += OnDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnDied;
    }

    private void Start()
    {
        _gameOverGroup = GetComponent<CanvasGroup>();
        _gameOverGroup.alpha = 0;
    }

    private void OnDied()
    {
        _gameOverGroup.alpha = 1;
        Time.timeScale = 0; // остановка времени. Ставим игру на паузу после смерти игрока

        _restartButton.onClick.AddListener(OnRestartButtonClick); // Подписываемся и вызываем обработчик события OnRestartButtonClick при нажатии на кнопку _restartButton
        _exitButton.onClick.AddListener(OnExitButtonClick); // Аналогично делаем для выхода из игры
    }

    private void OnRestartButtonClick()
    {
        Time.timeScale = 1; // Возвращаем время в норму
        SceneManager.LoadScene(0); // Загружаем нашу сцену заново (перезапускаем игру)

        _restartButton.onClick.RemoveListener(OnRestartButtonClick); // Отписываемся от обработчика событий
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
