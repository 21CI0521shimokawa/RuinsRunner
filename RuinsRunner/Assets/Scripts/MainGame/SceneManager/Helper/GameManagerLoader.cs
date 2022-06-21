using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerLoader : MonoBehaviour
{
    private static bool isLoadedGM { get; set; }

    private void Awake()
    {
        if (isLoadedGM) return;

        SceneManager.LoadScene("Manager", LoadSceneMode.Additive);
        isLoadedGM = true;
    }
}