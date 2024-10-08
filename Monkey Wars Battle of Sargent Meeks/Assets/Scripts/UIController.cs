using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * This Script Will Control the Different UI Elements in the Game
 */
public class UIController : MonoBehaviour
{
    /*
     * VARIABLES/OBJECTS
     */

    private int sceneNumber;



    /*
     * UNITY DEFAULT METHODS
     */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * EXTRA/CUSTOM METHODS
     */

    // Method used to Change Between Scenes
    public void LoadScene(int sceneNumber)
    {
        // Loading up the Scene of the Variable
        SceneManager.LoadScene(sceneNumber);

    } // END OF METHOD


    // Method to End the Game when Player Hits Quit
    public void QuitGame()
    {
        // Exiting the Game 
        Application.Quit();

    } // END OF METHOD
}
