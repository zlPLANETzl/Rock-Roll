using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerInvincibilityEffect : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private int flashCount = 4;

    public void Flash()
    {
        Debug.Log("[InvincibilityEffect] Flash Ω√¿€µ ");

        foreach (var renderer in renderers)
        {
            Color originalColor = renderer.material.color;
            Sequence seq = DOTween.Sequence();

            for (int i = 0; i < flashCount; i++)
            {
                seq.Append(renderer.material.DOColor(Color.red, flashDuration / 2f));
                seq.Append(renderer.material.DOColor(originalColor, flashDuration / 2f));
            }

            seq.OnComplete(() => Debug.Log("[InvincibilityEffect] Flash ¡æ∑·µ "));
        }
    }
}
