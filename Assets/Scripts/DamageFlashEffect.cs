using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DamageFlashEffect : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 0.2f; // 受伤闪光持续时间
    [SerializeField] private float fadeOutDuration = 0.3f; // 颜色淡出时间

    private Color originalColor;

    private void Awake()
    {
        // 在Awake中保存原始颜色
        originalColor = spriteRenderer.color;
    }

    public void Damage()
    {
        // 开始受伤闪光效果
        StartCoroutine(FlashDamageEffect());
    }

    private IEnumerator FlashDamageEffect()
    {
        // 立即变为红色
        spriteRenderer.color = new Color(1f, 0f, 0f, originalColor.a);

        yield return new WaitForSeconds(flashDuration);

        // 开始淡出至原色
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Lerp(0f, 1f, elapsedTime / fadeOutDuration);
            spriteRenderer.color = Color.Lerp(new Color(1f, 0f, 0f, originalColor.a), originalColor, t);

            yield return null;
        }
    }
}