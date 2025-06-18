using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickColor : MonoBehaviour
{
    // ����� ���� (����, ���, �Ķ�, �ʷ�)
    private Color[] colorPool = new Color[]
    {
        new Color(220f / 255f, 30f / 255f, 30f / 255f),   // ����
        new Color(255f / 255f, 215f / 255f, 0f / 255f),   // ���
        new Color(0f / 255f, 90f / 255f, 170f / 255f),    // �Ķ�
        new Color(0f / 255f, 150f / 255f, 0f / 255f)      // �ʷ�
    };

    private void Start()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            Color randomColor = colorPool[Random.Range(0, colorPool.Length)];

            Material matInstance = new Material(renderer.sharedMaterial);
            matInstance.color = randomColor;
            renderer.material = matInstance;
        }
    }
}
