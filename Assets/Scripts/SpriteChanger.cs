using UnityEngine;
using System.Collections.Generic;

public class SpriteChanger : MonoBehaviour
{
  public bool randomToggle = false; // Random değiştirme modu
  public List<Sprite> spritesToFind = new List<Sprite>(); // Değiştirilecek sprite listesi
  public Sprite spriteToReplace; // Yerine geçecek sprite (random kapalıysa)
  public List<Sprite> randomSprites = new List<Sprite>(); // Random modunda kullanılacak sprite listesi

  public void ChangeTheSprite()
  {
    // Sahnedeki tüm SpriteRenderer bileşenlerini al
    SpriteRenderer[] spriteRenderers = FindObjectsOfType<SpriteRenderer>();

    foreach (var spriteRenderer in spriteRenderers)
    {
      if (spritesToFind.Contains(spriteRenderer.sprite))
      {
        if (randomToggle)
        {
          // Random mod aktif, random bir sprite seç
          if (randomSprites.Count > 0)
          {
            spriteRenderer.sprite = randomSprites[Random.Range(0, randomSprites.Count)];
          }
          else
          {
            Debug.LogWarning("Random sprite listesi boş.");
          }
        }
        else
        {
          // Random mod kapalı, tek bir sprite ile değiştir
          spriteRenderer.sprite = spriteToReplace;
        }
      }
    }

    Debug.Log("Sprite değiştirme işlemi tamamlandı.");
  }
}