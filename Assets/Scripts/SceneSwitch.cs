using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public bool isGameMap = true; // combat/game scene
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // M key switches to opposite map
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isGameMap)
            {
                isGameMap = false;
                SceneManager.LoadScene("CampaignScene");
            }
            else
            {
                isGameMap = true;
                SceneManager.LoadScene("CombatScene");
            }
        }
    }
}
