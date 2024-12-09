using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cans : MonoBehaviour
{
    [SerializeField] string _FolderPath = "Sprites"; // Resources içindeki klasör adı
    private List<Sprite> _spriteList;
    [SerializeField] private SpriteRenderer _SpriteRenderer;

    void LoadSpritesFromFolder()
    {
        // Resources klasöründen tüm sprite'ları yükle
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(_FolderPath);

        // Listeyi oluştur ve yüklenen sprite'ları ekle
        _spriteList = new List<Sprite>(loadedSprites);
    }
    void Start()
    {
        LoadSpritesFromFolder();
        _SpriteRenderer.sprite = _spriteList[Random.Range(0, _spriteList.Count)];
    }
}
