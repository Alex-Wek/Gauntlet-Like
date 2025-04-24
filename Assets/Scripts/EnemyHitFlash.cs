using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitFlash : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer bodyRenderer;
    [SerializeField] private Material flashMaterial;
    private Material[] originalMaterials;






    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.1f;

    private List<SkinnedMeshRenderer> renderers = new List<SkinnedMeshRenderer>();

    void Awake()
    {
    originalMaterials = bodyRenderer.materials;
    }




    public void FlashWhite()
{
    StopAllCoroutines();
    StartCoroutine(FlashCoroutine());
}

private IEnumerator FlashCoroutine()
{
    Material[] flashMats = new Material[bodyRenderer.materials.Length];
    for (int i = 0; i < flashMats.Length; i++)
    {
        flashMats[i] = flashMaterial;
    }

    bodyRenderer.materials = flashMats;

    yield return new WaitForSeconds(0.1f);

    bodyRenderer.materials = originalMaterials;
}


}
