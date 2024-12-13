using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;  // Звук при наведении
    public AudioClip clickSound;  // Звук при нажатии
    private AudioSource audioSource;

    void Start()
    {
        // Добавляем компонент AudioSource, если его нет
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Метод, который вызывается при наведении
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound); 
            Debug.Log("www"); // Воспроизводим звук при наведении
        }
    }

    // Метод, который вызывается при нажатии
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);  // Воспроизводим звук при нажатии
            Debug.Log("lll");
        }
    }
}
