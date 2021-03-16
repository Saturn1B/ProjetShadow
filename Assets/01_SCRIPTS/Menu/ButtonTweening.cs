using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTweening : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
        {
            LeanTween.scale(gameObject, new Vector3(1.15f, 1.15f, 1.15f), 0.1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.1f);
    }
}
