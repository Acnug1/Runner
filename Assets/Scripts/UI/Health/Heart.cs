using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class Heart : MonoBehaviour
{
    [SerializeField] private float _lerpDuration;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
     //   _image.fillAmount = 1;
    }

    public void ToFill()
    {
        StartCoroutine(Filling(0, 1, _lerpDuration, Fill)); // начинаем корутину и подписываемся на событие Fill
    }

    public void ToEmpty()
    {
        StartCoroutine(Filling(1, 0, _lerpDuration, Destroy)); // начинаем корутину и подписываемся на событие Destroy
    }

    private IEnumerator Filling(float startValue, float endValue, float duration, UnityAction<float> lerpingEnd) // Создаем систему событий в корутине
    {
        float elapsed = 0;
        float nextValue;

        while (elapsed < duration)
        {
            nextValue = Mathf.Lerp(startValue, endValue, elapsed / duration);
            _image.fillAmount = nextValue;
            elapsed += Time.deltaTime;
            yield return null;
        }

        lerpingEnd?.Invoke(endValue); // Передаем конечное значение корутины в обработчики событий Fill или Destroy
    }

    private void Destroy(float value) // Обработчик события Destroy
    {
        _image.fillAmount = value; // Доводим fillAmount ровно до нуля и уничтожаем объект
        Destroy(gameObject);
    }

    private void Fill(float value) // Обработчик события Fill
    {
        _image.fillAmount = value; // Доводим fillAmount ровно до единицы при заполнении
    }
}
