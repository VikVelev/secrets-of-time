﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class windows_xp : MonoBehaviour {

    public Camera character_camera;
    public GameObject Display;
    public CharacterController character;
    public Canvas windows_start;
    public GameObject player;
    bool computer_on = false;
    public GameObject[] disable;
    Vector3 cam_pos;
    Quaternion cam_rot;

    private void Update()
    {
        if (computer_on && Input.GetButton("Cancel"))
        {
            Interaction();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Interaction()
    {
        if (!computer_on)
        {
            cam_pos = character_camera.transform.position;
            cam_rot = character_camera.transform.rotation;
            for (int i = 0; i < disable.Length; i++)
            {
                disable[i].SetActive(false);
            }

            player.GetComponent<FirstPersonController>().enabled = false;
            player.GetComponent<pause>().enabled = false;
            player.GetComponent<choices>().enabled = false;
            player.GetComponent<crosshair>().enabled = false;
            player.GetComponent<player_lookat>().enabled = false;
            character.enabled = false;
            character_camera.transform.position = Display.transform.position;
            character_camera.transform.rotation = Display.transform.rotation;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            computer_on = true;

        }
        else
        {
            //Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<pause>().enabled = true;
            player.GetComponent<choices>().enabled = true;
            player.GetComponent<FirstPersonController>().enabled = true;
            player.GetComponent<crosshair>().enabled = true;
            player.GetComponent<player_lookat>().enabled = true;
            character.enabled = true;
            character_camera.transform.position = cam_pos;
            character_camera.transform.rotation = cam_rot;
            for (int i = 0; i < disable.Length; i++)
            {
                disable[i].SetActive(true);
            }
            computer_on = false;
            Cursor.visible = false;
        }
    }
}
