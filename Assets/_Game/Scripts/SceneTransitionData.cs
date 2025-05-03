using UnityEngine;

// Bu script'i projenizin herhangi bir yerine ekleyin,
// bir GameObject'e eklemenize gerek YOKTUR.
public static class SceneTransitionData
{
    // Nullable Vector3? ve Vector2? kullanarak veri olup olmadýðýný kontrol edebiliriz.
    public static Vector3? NextSpawnPosition { get; set; } = null;
    public static Vector2? NextSpawnVelocity { get; set; } = null;

    // Ýsteðe baðlý: Veriyi kullandýktan sonra temizlemek için bir metot
    public static void Clear()
    {
        NextSpawnPosition = null;
        NextSpawnVelocity = null;
        Debug.Log("SceneTransitionData cleared.");
    }
}