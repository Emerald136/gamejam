using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonImageChange : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public GameObject normalImage;  
    public GameObject hoverImage;   
    public AudioClip hoverSound;  
    public AudioClip clickSound;  
    private AudioSource audioSource;

    void Start()
    {
        
        audioSource = gameObject.AddComponent<AudioSource>();

        
        if (normalImage == null || hoverImage == null)
        {
            Debug.LogError("Не все объекты с картинками заданы!");
        }

        
        normalImage.SetActive(true);
        hoverImage.SetActive(false);
    }

   
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound); 
        }

        
        if (normalImage != null && hoverImage != null)
        {
            normalImage.SetActive(false);   // 
            hoverImage.SetActive(true);     
        }

        Debug.Log("Наведение на кнопку");
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);  
        }

        Debug.Log("Кнопка нажата");
    }

    
    public void OnPointerExit(PointerEventData eventData)
    {
        
        if (normalImage != null && hoverImage != null)
        {
            normalImage.SetActive(true);    
            hoverImage.SetActive(false);    
        }

        Debug.Log("Курсор ушел с кнопки");
    }
}
