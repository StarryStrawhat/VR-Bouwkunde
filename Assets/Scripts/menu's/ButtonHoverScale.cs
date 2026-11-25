using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ButtonHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public float hoverScale = 1.2f;
    public float animationTime = 0.12f;

    private RectTransform rectTransform;
    private Vector3 normalScale;
    private Vector3 targetScale;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        normalScale = rectTransform.localScale;
        targetScale = normalScale;
    }

    private void Update()
    {
        rectTransform.localScale = Vector3.Lerp(
            rectTransform.localScale,
            targetScale,
            1f - Mathf.Exp(-Time.unscaledDeltaTime / Mathf.Max(animationTime, 0.01f))
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = normalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = normalScale;
    }

    private void OnDisable()
    {
        if (rectTransform != null)
        {
            rectTransform.localScale = normalScale;
            targetScale = normalScale;
        }
    }
}
