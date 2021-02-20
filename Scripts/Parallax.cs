using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _speed;

    private RawImage _image;
    private float _imageUVPositionX;

    private void Start()
    {
        _image = GetComponent<RawImage>();
        _imageUVPositionX = _image.uvRect.x; // _imageUVPositionX изначально равен значению X, заданному в инспекторе
    }

    private void Update()
    {
        _imageUVPositionX += _speed * Time.deltaTime; // задаем движение бэкграунда на величину _speed по оси X синхронизируя с каждым кадром

        if (_imageUVPositionX > 1) // сбрасывает значение прокрутки в ноль, когда значение X достигает единицы (1 - означает, что закончился один проход текстуры)
            _imageUVPositionX = 0;

        _image.uvRect = new Rect(_imageUVPositionX, 0, _image.uvRect.width, _image.uvRect.height); // Подставляем значение X. Ширину и высоту берем из инспектора
    }
}
