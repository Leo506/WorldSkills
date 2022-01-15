using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Перечисление всех игровых событий
/// </summary>
public enum Events
{
    LOADING_END,
    AUTHORIZE_SUCCESFULL
}


/// <summary>
/// Интерфейс "наблюдателя"
/// </summary>
public interface IObserver
{
    void OnNotify(Events eventValue);
}

public class Subject : MonoBehaviour
{
    public static Subject instance;

    List<IObserver> observers = new List<IObserver>();


    private void Awake()
    {
        // Обеспечиваем наличие только одного экземпляра данного класса на сцене
        // используем паттерн синглтон

        if (instance != this && instance != null)
            Destroy(instance.gameObject);

        instance = this;
    }


    /// <summary>
    /// Регистрация нового наблюдателя
    /// </summary>
    /// <param name="observer">Объект, реализующий интерфейс IObserver</param>
    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }


    /// <summary>
    /// Удаление наблюдателя
    /// </summary>
    /// <param name="observer">Объект, реализующий интерфейс IObserver</param>
    public void RemoveObserver(IObserver observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }


    /// <summary>
    /// Уведомление о событии
    /// </summary>
    /// <param name="eventValue">Событие</param>
    public void Notify(Events eventValue)
    {
        foreach (var observer in observers)
            observer.OnNotify(eventValue);
    }
}
