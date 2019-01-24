using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonStart : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("level_lightpath");
    }
}
