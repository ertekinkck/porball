using UnityEngine;

// Bu script'i projenizin herhangi bir yerine ekleyin,
// bir GameObject'e eklemenize gerek YOKTUR.
public static class SceneTransitionData
{
    // Nullable Vector3? ve Vector2? kullanarak veri olup olmad���n� kontrol edebiliriz.
    public static Vector3? NextSpawnPosition { get; set; } = null;
    public static Vector2? NextSpawnVelocity { get; set; } = null;

    // �ste�e ba�l�: Veriyi kulland�ktan sonra temizlemek i�in bir metot
    public static void Clear()
    {
        NextSpawnPosition = null;
        NextSpawnVelocity = null;
        Debug.Log("SceneTransitionData cleared.");
    }
}