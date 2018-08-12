using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {
    public string levelName;

    void LoadNewLevel() {
        SceneManager.LoadScene(levelName);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            LoadNewLevel();
    }
}
