using System.Collections.Generic;
using UnityEngine;

public class ChooseVariant : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _spriteVariants;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _spriteRenderer.sprite = _spriteVariants[Random.Range(0, _spriteVariants.Count)];
    }
}
