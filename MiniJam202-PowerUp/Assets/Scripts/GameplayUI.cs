using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUI : MonoBehaviour
{
    public UIDocument UIDocument;
    public Texture fullHeart;
    public Texture emptyHeart;

    void Start()
    {
        if (UIDocument == null)
        {
            Debug.LogError("UI document component is not configured.");
        }

        if (fullHeart == null || emptyHeart == null)
        {
            Debug.LogError("Heart texture is not configured.");
        }
    }

    public void SetHealthDisplay(uint currentHealth)
    {
        for (int i = 0; i < 5; i++)
        {
            Image heartImage = UIDocument.rootVisualElement.Q<Image>("Heart_" + i);
            heartImage.image = (i + 1) <= currentHealth ? fullHeart : emptyHeart;
        }
    }
}
