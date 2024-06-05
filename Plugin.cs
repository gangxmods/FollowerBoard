using System;
using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using Utilla;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using System.Reflection.Emit;

namespace FollowBoard
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]

    public class Plugin : BaseUnityPlugin
    {
        public static GameObject FollowBoard;
        public static bool inModded = false;

        void Start()
        {
            LoadAssets();
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/
            //FollowBoard.SetActive(true);
            inModded = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/
            //FollowBoard.transform.position = new Vector3(0, 0, 0);
            //FollowBoard.SetActive(true);
            inModded = false;
        }

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            FollowBoard = Instantiate(FollowBoard);
            FollowBoard.SetActive(true);
            FollowBoard.transform.position = new Vector3(-63.4311f, 11.4414f, -83.7735f);
            FollowBoard.transform.rotation = Quaternion.Euler(358.2487f, 92.3339f, 0f);
            FollowBoard.transform.localScale = new Vector3(2f, 2f, 2f);
        }

        public static void LoadAssets()
        {
            AssetBundle bundle = LoadAssetBundle("FollowBoard.followboard");
            FollowBoard = bundle.LoadAsset<GameObject>("FollowerBoard");
            GameObject T = bundle.LoadAsset<GameObject>("FollowerBoard");
            bundle = (AssetBundle)null;
            T = (GameObject)null;
        }

        public static AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        void L(string msg)
        {
            UnityEngine.Debug.Log(msg);
        }
    }
}