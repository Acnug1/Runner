using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Heart _heartTemplate;

    private List<Heart> _hearts = new List<Heart>();

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int value)
    {
        if (_hearts.Count < value) // Если отображаемое количество сердец меньше количества передаваемых жизней игрока
        {
            int createHealth = value - _hearts.Count; // вычитаем из количества жизней игрока количество отображаемых сердец 
            for (int i = 0; i < createHealth; i++) // (т.е. находим количество сердец, которые нужно отрисовать)
            {
                CreateHeart(); // Создаем сердце
            }
        }
        else if (_hearts.Count > value && _hearts.Count != 0) // Если сердец отображено больше, чем жизней игрока
        {
            int deleteHealth = _hearts.Count - value; // Вычитаем из количества сердец количество жизней игрока
            for (int i = 0; i < deleteHealth; i++) // (т.е. находим количество сердец, которые нужно уничтожить)
            {
                DestroyHeart(_hearts[_hearts.Count - 1]); // Уничтожаем последнее сердце в списке
            }
        }    
    }

    private void CreateHeart()
    {
        Heart newHeart = Instantiate(_heartTemplate, transform);
        _hearts.Add(newHeart.GetComponent<Heart>()); // добавляем в список сердец новое сердце и получаем его компонент
        newHeart.ToFill(); // заполняем новое сердце
    }

    private void DestroyHeart(Heart heart) // Передаем последнее сердце в метод DestroyHeart
    {
        _hearts.Remove(heart); // Удаляем сердце из списка
        heart.ToEmpty(); // опустошаем сердце
    }
}
