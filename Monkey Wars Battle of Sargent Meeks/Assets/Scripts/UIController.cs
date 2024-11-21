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

    // Variable to Hold the Scene Number 
    private int sceneNumber;

    // Variable to Determine if the Panels are Active or Not
    private bool isPanelActive = false;

    private GameObject panel;

    /*
     * EXTRA/CUSTOM METHODS
     */

    // Method used to Change Between Scenes
    public void LoadScene(int sceneNumber)
    {
        if (sceneNumber == 0) 
        { 
            StatisticManager.WriteData(); 
        }

        // Loading up the Scene of the Variable
        SceneManager.LoadScene(sceneNumber);

    } // END OF METHOD


    // Method to End the Game when Player Hits Quit
    public void QuitGame()
    {
        StatisticManager.WriteData();
        // Exiting the Game 
        Application.Quit();

    } // END OF METHOD


    // Method that Controls the Activation of the Objectives/Controls Panel
    public void PanelActivation(GameObject panel)
    {
        // Checking if the Panels are Active 
        if (isPanelActive)
        {
            // Setting the Panel to Inactive
            panel.SetActive(false);

            // Reset Boolean
            isPanelActive = false;

        }
        else
        {
            // Setting the Panel to Active
            panel.SetActive(true);

            // Reset Boolean
            isPanelActive = true;

        } // END OF IF/ELSE

    } // END OF METHOD
}
