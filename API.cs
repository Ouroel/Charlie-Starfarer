using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAboveAPI;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using static StarsAboveAPI.StarsAboveVN_Custom;

//replace this namespace with your own
namespace StarsAboveAPI
{
    //copy this class and throw it in your mod
    internal static class API
    {
        //Set this in Mod.Load()
        public static Mod YourMod;
        public static Mod StarsAboveAPI { get { if (starsAboveAPI == null) ModLoader.TryGetMod("StarsAboveAPI", out starsAboveAPI); return starsAboveAPI; } }
        private static Mod starsAboveAPI = null;

        /// <summary>
        /// Gets the current starfarer visible attire.
        /// </summary>
        /// <param name="player">Player</param>
        internal static int GetCurrentVisibleAttire(Player player)
        {
            return (int)StarsAboveAPI.Call(YourMod, 11, player);
        }


        /// <summary>
        /// Returns the blink timer of the current player. Used for making starfarers blink at regular intervals.
        /// </summary>
        /// <param name="player">Player</param>
        internal static int GetBlinkTimer(Player player)
        {
            return (int)StarsAboveAPI.Call(YourMod, 14, player);
        }


        /// <summary>
        /// Returns true if Stars Above April Fools is enabled (places shades on everyone)
        /// </summary>
        /// <param name="player">Player</param>
        internal static bool GetShadesOn()
        {
            return (bool)StarsAboveAPI.Call(YourMod, 15);
        }

        /// <summary>
        /// Gets the current starfarer. 0 = no starfarer, 1 = aspho 2 = eri. Item2 is the modded starfarer, if any.
        /// </summary>
        /// <param name="player">Player</param>
        internal static Tuple<int, object> GetCurrentStarfarer(Player player)
        {
            return (Tuple<int, object>)StarsAboveAPI.Call(YourMod, 0, player);
        }

        /// <summary>
        /// Gets the original starfarer. 0 = no starfarer, 1 = aspho 2 = eri.
        /// </summary>
        /// <param name="player">Player</param>
        internal static int GetOriginalStarfarer(Player player)
        {
            return (int)StarsAboveAPI.Call(YourMod, 16, player);
        }

        /// <summary>
        /// Resets popups. If OnResetDialog is not null, resets dialog boxes. If OnResetExpression is not null, reset expressions. If OnResetVN is not null, reset the VN.
        /// </summary>
        internal static void ResetPopup(Action OnResetDialog = null, Action OnResetExpression = null, Action OnResetVN = null)
        {
            StarsAboveAPI.Call(YourMod, 17, OnResetDialog, OnResetExpression, OnResetVN);
        }

        /// <summary>
        /// Gets the current vagrant dialog level. Used for shiny hair/whatever.
        /// </summary>
        /// <param name="player">Player</param>
        internal static int GetCurrentVagrantDialogLevel(Player player)
        {
            return (int)StarsAboveAPI.Call(YourMod, 8, player);
        }

        /// <summary>
        /// Adds an event to the "Spatial Disk Resonate" pool
        /// </summary>
        /// <param name="priority">As compared to the rest in the pool</param>
        /// <param name="condition">return true if stuff is done, otherwise return false. Also do stuff in here.</param>
        internal static void SpatialDiskResonateDialog(float priority, Func<bool> condition)
        {
            StarsAboveAPI.Call(YourMod, 1, priority, condition);
        }

        /// <summary>
        /// Adds a Spatial Disk dialog. 
        /// </summary>
        /// <param name="textData">Tuples of text and expression ID. Decompile or experiment to figure it out. BTW 20 is Tsuki</param>
        /// <param name="priority">As compared to the rest in the pool. Setting this to 1 or lower will mess with the initial "choose starfarer" dialogue.</param>
        /// <param name="condition">return true if stuff is done, otherwise return false. Also do stuff in here. Can be null.</param>
        /// <param name="itemID">The item to spawn. -1 if not spawning.</param>
        /// <param name="itemCount">Quantity of spawned items</param>
        internal static object AddSpatialDiskDialog(Tuple<string, int>[] textData, float priority, Func<bool> condition, int itemID = -1, int itemCount = 1, Action<SpriteBatch, int, Rectangle, Color> customDraw = null, Action<SpriteBatch, int, Rectangle, Color> customDrawOverUI = null, Action OnDialogAdvanced = null)
        {
            return StarsAboveAPI.Call(YourMod, 2, textData, condition, priority, itemID, itemCount, customDraw, customDrawOverUI, OnDialogAdvanced);
        }

        /// <summary>
        /// Adds a datadriven Spatial Disk dialog. 
        /// </summary>
        /// <param name="LocalizationPath">The path in your Localization file</param>
        /// <param name="priority">As compared to the rest in the pool. Setting this to 1 or lower will mess with the initial "choose starfarer" dialogue.</param>
        /// <param name="condition">return true if stuff is done, otherwise return false. Also do stuff in here. Can be null.</param>
        /// <param name="itemID">The item to spawn. -1 if not spawning.</param>
        /// <param name="itemCount">Quantity of spawned items</param>
        internal static object AddSpatialDiskDialogDatadriven(string LocalizationPath, float priority, Func<bool> condition, int itemID = -1, int itemCount = 1, Action<SpriteBatch, int, Rectangle, Color> customDraw = null, Action<SpriteBatch, int, Rectangle, Color> customDrawOverUI = null, Action OnDialogAdvanced = null)
        {
            return StarsAboveAPI.Call(YourMod, 2, LocalizationPath, condition, priority, itemID, itemCount, customDraw, customDrawOverUI, OnDialogAdvanced);
        }

        /*
        
        Here is how datadriven dialogs work
        Say your path is "Mods.YourMod.Dialogs.Whatever.NewDialog"
        In your localization file, you would have
        Mods: {
            YourMod: {
                Dialogs: {
                    Whatever: {
                        NewDialog: {
                            1: {
                                Text: This is my text
                                Expression: 1 //Your face number
                                }
                            2: {
                                Text: This is my text
                                Expression: 1 //Your face number
                                }
                            3: {
                                Text: This is my text
                                Expression: 1 //Your face number
                                }
                            4: {
                                Text: This is my text
                                Expression: 1 //Your face number
                                }
                        }
                    }
                }
            }
        }

         */

        /// <summary>
        /// Adds an entry to the archive. Entry can be Spatial Disk Dialog or VN ID.
        /// </summary>
        /// <param name="archiveType">Type of archive. Experiment to find out, or decompile.</param>
        /// <param name="priority">As compared to the rest in the pool</param>
        /// <param name="ArchiveData">Returns the title, description, and the spatial disk dialog object/VN ID</param>
        internal static object AddSpatialDiskDialogArchive(int archiveType, float priority, Func<Tuple<string, string, object>> ArchiveData)
        {
            return StarsAboveAPI.Call(YourMod, 3, archiveType, priority, ArchiveData);
        }


        /// <summary>
        /// Forcibly opens the dialog box
        /// </summary>
        /// <param name="DialogTree">Dialog tree to open</param>
        internal static void ForceOpenSpatialDiskDialogWindow(object DialogTree)
        {
            StarsAboveAPI.Call(YourMod, 4, DialogTree);
        }


        /// <summary>
        /// Opens a prompt
        /// </summary>
        /// <param name="Identifier">The identifier for other mods which adds starfarers to detect</param>
        /// <param name="Dialogue">The Dialog</param>
        /// <param name="Expression">The expression. Decompile to find out (20 is tsuki)</param>
        /// <param name="ActiveTimer">-1 for default (config modified), otherwise it will last for this long</param>
        /// <param name="force">If it will ignore the prompt cooldown</param>
        /// <param name="CustomDraw">Overwrites the portrait drawing function. Takes in the expression, destination rectangle, and color.</param>
        internal static void SetPromptExpression(string Identifier, string Dialogue, int Expression, int ActiveTimer = -1, bool force = false, Action<SpriteBatch, int, Rectangle, Color> CustomDraw = null, Action<SpriteBatch, int, Rectangle, Color> CustomDrawOverUI = null)
        {
            StarsAboveAPI.Call(YourMod, 5, Identifier, Dialogue, Expression, ActiveTimer, force, CustomDraw, CustomDrawOverUI);
        }

        /// <summary>
        /// Adds a modded VN scene. Returns the scene ID.
        /// </summary>
        /// <param name="VNLogic">Takes in the VN scene progress and gives out the VN data.</param>
        /// <param name="CustomDrawLogic">The function where you draw stuff.</param>
        /// <param name="GetDialogOptions">Allows you to manually overwrite the dialog options you see at the end of the VN. Can return an arbitrary number of buttons.</param>
        /// <param name="priority">As compared to the rest in the pool. Setting this to 1 or lower will mess with the initial "choose starfarer" dialogue.</param>
        /// <param name="condition">return true if stuff is done, otherwise return false. Also do stuff in here. Can be null.</param>
        /// <param name="OnDialogAdvanced">When dialog is advanced</param>
        /// <param name="OnOptionPressed">When option button is pressed. Top is 0, etc.</param>
        internal static int AddModdedVNScene(Action<int, object[]> VNLogic, Action<SpriteBatch, UIState, int, string, Tuple<string, int, int, Rectangle, Color>[], int> CustomDrawLogic = null, Func<Tuple<string, int>[]> GetDialogOptions = null, float priority = 1, Func<bool> condition = null, Action OnDialogAdvanced = null, Action<int> OnOptionPressed = null)
        {
            return (int)StarsAboveAPI.Call(YourMod, 6, VNLogic, CustomDrawLogic, GetDialogOptions, priority, condition, OnDialogAdvanced, OnOptionPressed);
        }

        /// <summary>
        /// Opens the VN
        /// </summary>
        /// <param name="VNID">VN ID to open.</param>
        internal static void OpenVN(int VNID)
        {
            StarsAboveAPI.Call(YourMod, 7, VNID);
        }

        // NOTE: You can just create a new class which derives from StarsAboveVN_Custom. Check the Stars Above Github to see how it works.

        /*  This is the code which calls CustomDrawLogic
		 * 
            Color color1 = Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker));
            Color color2 = Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker2));
            Tuple<string, int, int, Rectangle, Color>[] data = new Tuple<string, int, int, Rectangle, Color>[]
            {
                new(modPlayer.VNCharacter1, modPlayer.VNCharacter1Pose, modPlayer.VNCharacter1Expression, chara1Rect, color1),
                new(modPlayer.VNCharacter2, modPlayer.VNCharacter2Pose, modPlayer.VNCharacter2Expression, chara2Rect, color2),
            };
            currentScene.Draw(spriteBatch, VN, modPlayer.sceneProgression, modPlayer.VNDialogueVisibleName, data, modPlayer.blinkTimer);
		 *
		 *
		 *	This is an example of how Stars Above draws a character
		 *
			spriteBatch.Draw((!0)ModContent.Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter1.Substring(0, 2) + modPlayer.VNCharacter1Pose.ToString(), 2), rectangle2, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker)));
		 *
		 */

        /// <summary>
        /// Adds a modded starfarer. I REFUSE TO DOCUMENT THIS. READ COMMENTS.
        /// </summary>
        /// <param name="StarfarerToSpoof">Causes Stars Above to believe that you are that particular starfarer. Cannot be 0 or it will break.</param>
        /// <param name="ReplaceDialogue">Takes in the originating mod and internal dialog ID. Call ForceOpenSpatialDiskDialogWindow in this and return true if you did.</param>
        /// <param name="ReplaceExpression">Takes in the originating mod and internal dialog ID/priority. Call SetPromptExpression with Forced = true in this and return true if you did.</param>
        /// <param name="ReplaceVN">Takes in the originating mod and internal dialog ID/priority. Call OpenVN in this and return true if you did.</param>
        internal static object AddNewStarfarer(
            string StarfarerName,
            int StarfarerToSpoof = 1,
            //expressions
            Func<Mod, string, int, int, bool> ReplaceExpression = null, //takes in mod, string, expression face id, and duration, does stuff. Forcibly call SetPromptExpression in this.
            Func<Mod, string, SpriteBatch, int, Rectangle, Color, bool> ReplaceExpressionDrawInfo = null, //return false if you dont wanna replace
            Func<Mod, string, SpriteBatch, int, Rectangle, Color, bool> ReplaceExpressionDrawInfoOverUI = null, //return false if you dont wanna replace

            //dialogue
            Func<Mod, float, bool> ReplaceDialogue = null, //call ForceOpenSpatialDiskDialogWindow
            Func<Mod, float, SpriteBatch, int, Rectangle, Color, bool> ReplaceDialogueDrawInfo = null,
            Func<Mod, float, SpriteBatch, int, Rectangle, Color, bool> ReplaceDialogueDrawInfoOverUI = null,

            //VN
            Func<Mod, float, bool> ReplaceVN = null, //call OpenVN
            Func<Mod, float, SpriteBatch, UIState, int, string, Tuple<string, int, int, Rectangle, Color>[], int, bool> ReplaceVNDrawInfo = null,

            //UI Draws
            Func<SpriteBatch, int, Rectangle, Color, Color, bool> ReplaceStarfarerUIDrawInfo = null, //takes in VagrantDialog. Second color is costume change visibility
            Func<SpriteBatch, int, bool, int, Rectangle, Color, bool> ReplaceStarfarerUIDrawInfoBlink = null, //takes in blinkTimer, shades, VagrantDialog
            Func<SpriteBatch, Rectangle, Color, bool> ReplaceNovaUIDrawInfo = null,
            Func<SpriteBatch, int, Rectangle, Color, bool> ReplaceNovaUIDrawInfoBlink = null, //takes in blinkTimer
            Func<SpriteBatch, int, int, int, bool, Rectangle, Rectangle, Rectangle, Color, string> ReplaceNovaCutInUIDrawInfo = null, //takes in chosenStellarNova, NovaCutInTimer, randomNovaDialogue, shades. Rectangles are A0, AS1, AS respectively.

            //Nova
            Func<int, int, bool> CustomNovaDialog = null, //takes in chosenStellarNova, randomNovaDialog
            Func<Player, int, Vector2, bool> OnActivateStellarNova = null, //takes in player, chosenStellarNova, position

            //Menu
            Func<int, string> MenuOnOpen = null, //takes in inCombat
            Func<bool, string> MenuOnArchiveConfirmButton = null, //takes in archiveActive
            Func<string> MenuOnArchiveHoverButton = null,
            Func<string> MenuOnConfirmHoverButton = null,
            Func<string> MenuOnStellarArrayHoverButton = null,
            Func<string> MenuOnStellarArrayConfirmButton = null,
            Func<bool, string> MenuOnStellarNovaHoverButton = null, //takes in unlocked
            Func<string> MenuOnStellarNovaConfirmButton = null,
            Func<string> MenuOnVoyageHoverButton = null,
            Func<string> MenuOnArmorSlotHover = null,
            Func<string> MenuOnVanitySlotHover = null,

            //Nova Menu
            Func<int, string> NovaOnEdinGenesisQuasarHover = null, //takes in unlocked
            Func<int, string> NovaOnGardenOfAvalonHover = null, //takes in unlocked
            Func<int, string> NovaOnKiwamiRyukenHover = null, //takes in unlocked
            Func<int, string> NovaOnLaevateinnHover = null, //takes in unlocked
            Func<int, string> NovaOnTheofaniaHover = null, //takes in unlocked

            Func<string> NovaOnAffix1Hover = null,
            Func<string> NovaOnAffix2Hover = null,
            Func<string> NovaOnAffix3Hover = null,

            Func<string> NovaOnConfirmHover = null,

            Func<string> NovaResetHover = null,
            Func<string> NovaSpecialAffixHover = null
            )
        {
            return StarsAboveAPI.Call(YourMod, 9,
                StarfarerName,
                StarfarerToSpoof,
                //expressions
                ReplaceExpression, //takes in mod and string, does stuff. Forcibly call SetPromptExpression in this.
                ReplaceExpressionDrawInfo, //return false if you dont wanna replace
                ReplaceExpressionDrawInfoOverUI,

                //dialogue
                ReplaceDialogue, //call ForceOpenSpatialDiskDialogWindow
                ReplaceDialogueDrawInfo,
                ReplaceDialogueDrawInfoOverUI,

                //VN
                ReplaceVN, //call OpenVN
                ReplaceVNDrawInfo,

                //UI Draws
                ReplaceStarfarerUIDrawInfo, //takes in VagrantDialog
                ReplaceStarfarerUIDrawInfoBlink, //takes in blinkTimer, shades, VagrantDialog
                ReplaceNovaUIDrawInfo,
                ReplaceNovaUIDrawInfoBlink, //takes in blinkTimer
                ReplaceNovaCutInUIDrawInfo, //takes in chosenStellarNova, NovaCutInTimer, randomNovaDialogue, shades

                //Nova
                CustomNovaDialog, //takes in chosenStellarNova, randomNovaDialog
                OnActivateStellarNova, //takes in player, chosenStellarNova, position

                //Menu
                MenuOnOpen, //takes in inCombat
                MenuOnArchiveConfirmButton, //takes in archiveActive
                MenuOnArchiveHoverButton,
                MenuOnConfirmHoverButton,
                MenuOnStellarArrayHoverButton,
                MenuOnStellarArrayConfirmButton,
                MenuOnStellarNovaHoverButton, //takes in unlocked
                MenuOnStellarNovaConfirmButton,
                MenuOnVoyageHoverButton,
                MenuOnArmorSlotHover,
                MenuOnVanitySlotHover,

                //Nova Menu
                NovaOnEdinGenesisQuasarHover, //takes in unlocked
                NovaOnGardenOfAvalonHover, //takes in unlocked
                NovaOnKiwamiRyukenHover, //takes in unlocked
                NovaOnLaevateinnHover, //takes in unlocked
                NovaOnTheofaniaHover, //takes in unlocked

                NovaOnAffix1Hover,
                NovaOnAffix2Hover,
                NovaOnAffix3Hover,

                NovaOnConfirmHover,

                NovaResetHover,
                NovaSpecialAffixHover);
        }
        /// <summary>
        /// Sets the current starfarer to a modded starfarer
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="Starfarer">The starfarer object or vanilla ID</param>
        internal static void SetStarfarer(Player player, object Starfarer)
        {
            StarsAboveAPI.Call(YourMod, 10, player, Starfarer);
        }
        /// <summary>
        /// Resets the player's starfarer to their original starfarer
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="Starfarer">The starfarer object or vanilla ID</param>
        internal static void ResetStarfarer(Player player)
        {
            StarsAboveAPI.Call(YourMod, 12, player);
        }
        /// <summary>
        /// Sets the player's starfarer to that modded starfarer if it's not the current starfarer, otherwise calls ResetStarfarer
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="Starfarer">The starfarer object or vanilla ID</param>
        internal static void ToggleStarfarer(Player player, object Starfarer)
        {
            StarsAboveAPI.Call(YourMod, 13, player, Starfarer);
        }
    }

    //Does most of the starfarer creation work for you. Throw the starfarer data into the localization file.
    public abstract class StarsAboveStarfarer_Datadriven : StarsAboveStarfarer_Custom
    {
        //The localization path which gets all your text data, etc.
        public abstract string GetLocalizationPath();

        //The file names of your Stellar Nova Cut-Ins
        //public abstract Tuple<string, string> GetNovaCutInTrianglePaths();

        object[] Dialogue = new object[305];
        object Dialog2_Hardmode;

        //It's an array of 305. Look up StarsAboveDialogueSystem to figure out which corresponds to which.
        public virtual Tuple<int, int>[] WeaponDialogDrops => getWeaponDialogDrops();
        Tuple<int, int>[] weaponDialogDrops = null;

        private Tuple<int, int>[] getWeaponDialogDrops()
        {
            if (weaponDialogDrops == null)
            {
                weaponDialogDrops = new Tuple<int, int>[305];
                for (int x = 0; x < 305; x++)
                {
                    weaponDialogDrops[x] = new(-1, 0);
                }
            }
            return weaponDialogDrops;
        }
        public override string MenuOnArchiveHoverButton => Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnArchiveHoverButton", Main.LocalPlayer.name);
        public override string MenuOnConfirmHoverButton => Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnConfirmHoverButton", Main.LocalPlayer.name);
        public override string MenuOnArchiveConfirmButton(bool archiveActive)
        {
            if (archiveActive)
                return Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnArchiveConfirmButton_ArchiveActive", Main.LocalPlayer.name);
            return Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnArchiveConfirmButton_ArchiveNotActive", Main.LocalPlayer.name);
        }

        public override string MenuOnStellarArrayHoverButton => Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnStellarArrayHoverButton", Main.LocalPlayer.name);
        public override string MenuOnStellarArrayConfirmButton => Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnStellarArrayConfirmButton", Main.LocalPlayer.name);
        public override string MenuOnStellarNovaConfirmButton => Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnStellarNovaConfirmButton", Main.LocalPlayer.name);
        public override string MenuOnVoyageHoverButton => Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnVoyageHoverButton", Main.LocalPlayer.name);
        public override string MenuOnArmorSlotHover => Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnArmorSlotHover", Main.LocalPlayer.name);
        public override string MenuOnVanitySlotHover => Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnVanitySlotHover", Main.LocalPlayer.name);
        public override string MenuOnOpen(int inCombat)
        {
            if (inCombat < 0)
                return Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnOpen", Main.LocalPlayer.name);
            return Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnOpen_InCombat", Main.LocalPlayer.name, inCombat);
        }
        public override string MenuOnStellarNovaHoverButton(bool unlocked)
        {
            if (unlocked)
                return Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnStellarNovaHoverButton", Main.LocalPlayer.name);
            return Language.GetTextValue(GetLocalizationPath() + ".StarfarerMenu.MenuOnStellarNovaHoverButton_Locked", Main.LocalPlayer.name);
        }
        public override string NovaOnAffix1Hover => Language.GetTextValue(GetLocalizationPath() + ".StellarNovaMenu.NovaOnAffix1Hover", Main.LocalPlayer.name);
        public override string NovaOnAffix2Hover => Language.GetTextValue(GetLocalizationPath() + ".StellarNovaMenu.NovaOnAffix2Hover", Main.LocalPlayer.name);
        public override string NovaOnAffix3Hover => Language.GetTextValue(GetLocalizationPath() + ".StellarNovaMenu.NovaOnAffix3Hover", Main.LocalPlayer.name);
        public override string NovaOnConfirmHover => Language.GetTextValue(GetLocalizationPath() + ".StellarNovaMenu.NovaOnConfirmHover", Main.LocalPlayer.name);

        public override string NovaOnEdinGenesisQuasarHover(int unlockedStage)
        {
            return OnStellarNovaHover(unlockedStage, "EdinGenesisQuasar");
        }
        public override string NovaOnGardenOfAvalonHover(int unlockedStage)
        {
            return OnStellarNovaHover(unlockedStage, "GardenOfAvalon");
        }
        public override string NovaOnKiwamiRyukenHover(int unlockedStage)
        {
            return OnStellarNovaHover(unlockedStage, "KiwamiRyuken");
        }

        public override string NovaOnLaevateinnHover(int unlockedStage)
        {
            return OnStellarNovaHover(unlockedStage, "Laevateinn");
        }
        public override string NovaOnTheofaniaHover(int unlockedStage)
        {
            return OnStellarNovaHover(unlockedStage, "Theofania");
        }
        public override string NovaOnResetHover => Language.GetTextValue(GetLocalizationPath() + ".StellarNovaMenu.NovaOnResetHover", Main.LocalPlayer.name);
        public override string NovaSpecialAffixHover => Language.GetTextValue(GetLocalizationPath() + ".StellarNovaMenu.NovaSpecialAffixHover", Main.LocalPlayer.name);

        string OnStellarNovaHover(int unlockedStage, string novaName)
        {
            if (unlockedStage == 0)
                return Language.GetTextValue(GetLocalizationPath() + ".StellarNovaMenu." + novaName + "_Locked", Main.LocalPlayer.name);
            return Language.GetTextValue(GetLocalizationPath() + ".StellarNovaMenu." + novaName + "_Unlocked", Main.LocalPlayer.name);
        }

        public override void SetDefaults()
        {
            Dialog2_Hardmode = GenerateDialog("IdleDialogueHardmode");

            Dialogue[1] = GenerateDialog("WeaponDialogue", WeaponDialogDrops[1].Item1, WeaponDialogDrops[1].Item2);
            Dialogue[2] = GenerateDialog("IdleDialogue", WeaponDialogDrops[2].Item1, WeaponDialogDrops[2].Item2);
            Dialogue[3] = GenerateDialog("RegularIdleDialogue.NormalIdleDialogue1", WeaponDialogDrops[3].Item1, WeaponDialogDrops[3].Item2);
            Dialogue[4] = GenerateDialog("RegularIdleDialogue.NormalIdleDialogue2", WeaponDialogDrops[4].Item1, WeaponDialogDrops[4].Item2);
            Dialogue[5] = GenerateDialog("RegularIdleDialogue.NormalIdleDialogue3", WeaponDialogDrops[5].Item1, WeaponDialogDrops[5].Item2);
            Dialogue[6] = GenerateDialog("RegularIdleDialogue.NormalIdleDialogue4", WeaponDialogDrops[6].Item1, WeaponDialogDrops[6].Item2);
            Dialogue[7] = GenerateDialog("RegularIdleDialogue.NormalIdleDialogue5", WeaponDialogDrops[7].Item1, WeaponDialogDrops[7].Item2);
            Dialogue[8] = GenerateDialog("RegularIdleDialogue.NormalIdleDialogue6", WeaponDialogDrops[8].Item1, WeaponDialogDrops[8].Item2);
            Dialogue[9] = GenerateDialog("RegularIdleDialogue.NormalIdleDialogue7", WeaponDialogDrops[9].Item1, WeaponDialogDrops[9].Item2);
            Dialogue[10] = GenerateDialog("RegularIdleDialogue.NormalIdleDialogue8", WeaponDialogDrops[10].Item1, WeaponDialogDrops[10].Item2);
            Dialogue[11] = GenerateDialog("RegularIdleDialogue.NormalIdleDialogue9", WeaponDialogDrops[11].Item1, WeaponDialogDrops[11].Item2);
            Dialogue[12] = GenerateDialog("RegularIdleDialogue.HardIdleDialogue1", WeaponDialogDrops[12].Item1, WeaponDialogDrops[12].Item2);
            Dialogue[13] = GenerateDialog("RegularIdleDialogue.HardIdleDialogue2", WeaponDialogDrops[13].Item1, WeaponDialogDrops[13].Item2);
            Dialogue[14] = GenerateDialog("RegularIdleDialogue.HardIdleDialogue3", WeaponDialogDrops[14].Item1, WeaponDialogDrops[14].Item2);
            Dialogue[15] = GenerateDialog("RegularIdleDialogue.HardIdleDialogue4", WeaponDialogDrops[15].Item1, WeaponDialogDrops[15].Item2);
            Dialogue[16] = GenerateDialog("RegularIdleDialogue.HardIdleDialogue5", WeaponDialogDrops[16].Item1, WeaponDialogDrops[16].Item2);
            Dialogue[17] = GenerateDialog("RegularIdleDialogue.HardIdleDialogue6", WeaponDialogDrops[17].Item1, WeaponDialogDrops[17].Item2);
            Dialogue[18] = GenerateDialog("RegularIdleDialogue.HardIdleDialogue7", WeaponDialogDrops[18].Item1, WeaponDialogDrops[18].Item2);
            Dialogue[19] = GenerateDialog("RegularIdleDialogue.HardIdleDialogue8", WeaponDialogDrops[19].Item1, WeaponDialogDrops[19].Item2);
            Dialogue[20] = GenerateDialog("RegularIdleDialogue.HardIdleDialogue9", WeaponDialogDrops[20].Item1, WeaponDialogDrops[20].Item2);
            Dialogue[21] = GenerateDialog("RegularIdleDialogue.LightIdleDialogue", WeaponDialogDrops[21].Item1, WeaponDialogDrops[21].Item2);
            Dialogue[22] = GenerateDialog("WeaponDialogue", WeaponDialogDrops[22].Item1, WeaponDialogDrops[22].Item2);
            Dialogue[23] = GenerateDialog("WeaponDialogue", WeaponDialogDrops[23].Item1, WeaponDialogDrops[23].Item2);
            Dialogue[24] = GenerateDialog("WeaponDialogue", WeaponDialogDrops[24].Item1, WeaponDialogDrops[24].Item2);
            Dialogue[25] = GenerateDialog("WeaponDialogue", WeaponDialogDrops[25].Item1, WeaponDialogDrops[25].Item2);
            Dialogue[51] = GenerateDialog("BossDialogue.KingSlime", WeaponDialogDrops[51].Item1, WeaponDialogDrops[51].Item2);
            Dialogue[52] = GenerateDialog("BossDialogue.CthulhuEye", WeaponDialogDrops[52].Item1, WeaponDialogDrops[52].Item2);
            Dialogue[53] = GenerateDialog("BossDialogue.CorruptionBoss", WeaponDialogDrops[53].Item1, WeaponDialogDrops[53].Item2);
            Dialogue[54] = GenerateDialog("BossDialogue.QueenBee", WeaponDialogDrops[54].Item1, WeaponDialogDrops[54].Item2);
            Dialogue[55] = GenerateDialog("BossDialogue.Skeletron", WeaponDialogDrops[55].Item1, WeaponDialogDrops[55].Item2);
            Dialogue[56] = GenerateDialog("BossDialogue.WallOfFlesh", WeaponDialogDrops[56].Item1, WeaponDialogDrops[56].Item2);
            Dialogue[58] = GenerateDialog("BossDialogue.Twins", WeaponDialogDrops[58].Item1, WeaponDialogDrops[58].Item2);
            Dialogue[57] = GenerateDialog("BossDialogue.Destroyer", WeaponDialogDrops[57].Item1, WeaponDialogDrops[57].Item2);
            Dialogue[59] = GenerateDialog("BossDialogue.SkeletronPrime", WeaponDialogDrops[59].Item1, WeaponDialogDrops[59].Item2);
            Dialogue[60] = GenerateDialog("BossDialogue.AllMechs", WeaponDialogDrops[60].Item1, WeaponDialogDrops[60].Item2);
            Dialogue[61] = GenerateDialog("BossDialogue.Plantera", WeaponDialogDrops[61].Item1, WeaponDialogDrops[61].Item2);
            Dialogue[62] = GenerateDialog("BossDialogue.Golem", WeaponDialogDrops[62].Item1, WeaponDialogDrops[62].Item2);
            Dialogue[63] = GenerateDialog("BossDialogue.DukeFishron", WeaponDialogDrops[63].Item1, WeaponDialogDrops[63].Item2);
            Dialogue[64] = GenerateDialog("BossDialogue.Cultist", WeaponDialogDrops[64].Item1, WeaponDialogDrops[64].Item2);
            Dialogue[65] = GenerateDialog("BossDialogue.MoonLord", WeaponDialogDrops[65].Item1, WeaponDialogDrops[65].Item2);
            Dialogue[66] = GenerateDialog("BossDialogue.WarriorOfLight", WeaponDialogDrops[66].Item1, WeaponDialogDrops[66].Item2);
            Dialogue[67] = GenerateDialog("BossDialogue.AllVanillaBosses", WeaponDialogDrops[67].Item1, WeaponDialogDrops[67].Item2);
            Dialogue[68] = GenerateDialog("BossDialogue.VanillaAndWarrior", WeaponDialogDrops[68].Item1, WeaponDialogDrops[68].Item2);
            Dialogue[69] = GenerateDialog("BossDialogue.Vagrant", WeaponDialogDrops[69].Item1, WeaponDialogDrops[69].Item2);
            Dialogue[70] = GenerateDialog("BossDialogue.Nalhaun", WeaponDialogDrops[70].Item1, WeaponDialogDrops[70].Item2);
            Dialogue[71] = GenerateDialog("BossDialogue.Penth", WeaponDialogDrops[71].Item1, WeaponDialogDrops[71].Item2);
            Dialogue[72] = GenerateDialog("BossDialogue.Arbitration", WeaponDialogDrops[72].Item1, WeaponDialogDrops[72].Item2);
            Dialogue[73] = GenerateDialog("BossDialogue.Tsukiyomi", WeaponDialogDrops[73].Item1, WeaponDialogDrops[73].Item2);
            Dialogue[74] = GenerateDialog("BossDialogue.QueenSlime", WeaponDialogDrops[74].Item1, WeaponDialogDrops[74].Item2);
            Dialogue[75] = GenerateDialog("BossDialogue.EmpressOfLight", WeaponDialogDrops[75].Item1, WeaponDialogDrops[75].Item2);
            Dialogue[76] = GenerateDialog("BossDialogue.Deerclops", WeaponDialogDrops[76].Item1, WeaponDialogDrops[76].Item2);
            Dialogue[201] = GenerateDialog("CalamityBossDialogue.DesertScourge", WeaponDialogDrops[201].Item1, WeaponDialogDrops[201].Item2);
            Dialogue[202] = GenerateDialog("CalamityBossDialogue.Crabulon", WeaponDialogDrops[202].Item1, WeaponDialogDrops[202].Item2);
            Dialogue[203] = GenerateDialog("CalamityBossDialogue.HiveMind", WeaponDialogDrops[203].Item1, WeaponDialogDrops[203].Item2);
            Dialogue[204] = GenerateDialog("CalamityBossDialogue.Perforators", WeaponDialogDrops[204].Item1, WeaponDialogDrops[204].Item2);
            Dialogue[205] = GenerateDialog("CalamityBossDialogue.SlimeGod", WeaponDialogDrops[205].Item1, WeaponDialogDrops[205].Item2);
            Dialogue[206] = GenerateDialog("CalamityBossDialogue.Cryogen", WeaponDialogDrops[206].Item1, WeaponDialogDrops[206].Item2);
            Dialogue[207] = GenerateDialog("CalamityBossDialogue.AquaticScourge", WeaponDialogDrops[207].Item1, WeaponDialogDrops[207].Item2);
            Dialogue[208] = GenerateDialog("CalamityBossDialogue.BrimstoneElemental", WeaponDialogDrops[208].Item1, WeaponDialogDrops[208].Item2);
            Dialogue[209] = GenerateDialog("CalamityBossDialogue.Calamitas", WeaponDialogDrops[209].Item1, WeaponDialogDrops[209].Item2);
            Dialogue[210] = GenerateDialog("CalamityBossDialogue.Leviathan", WeaponDialogDrops[210].Item1, WeaponDialogDrops[210].Item2);
            Dialogue[211] = GenerateDialog("CalamityBossDialogue.AstrumAureus", WeaponDialogDrops[211].Item1, WeaponDialogDrops[211].Item2);
            Dialogue[212] = GenerateDialog("CalamityBossDialogue.PlaguebringerGoliath", WeaponDialogDrops[212].Item1, WeaponDialogDrops[212].Item2);
            Dialogue[213] = GenerateDialog("CalamityBossDialogue.Ravager", WeaponDialogDrops[213].Item1, WeaponDialogDrops[213].Item2);
            Dialogue[214] = GenerateDialog("CalamityBossDialogue.AstrumDeus", WeaponDialogDrops[214].Item1, WeaponDialogDrops[214].Item2);
            Dialogue[101] = GenerateDialog("WeaponDialogue.1", WeaponDialogDrops[101].Item1, WeaponDialogDrops[101].Item2);
            Dialogue[102] = GenerateDialog("WeaponDialogue.3", WeaponDialogDrops[102].Item1, WeaponDialogDrops[102].Item2);
            Dialogue[103] = GenerateDialog("WeaponDialogue.5", WeaponDialogDrops[103].Item1, WeaponDialogDrops[103].Item2);
            Dialogue[104] = GenerateDialog("WeaponDialogue.7", WeaponDialogDrops[104].Item1, WeaponDialogDrops[104].Item2);
            Dialogue[105] = GenerateDialog("WeaponDialogue.9", WeaponDialogDrops[105].Item1, WeaponDialogDrops[105].Item2);
            Dialogue[106] = GenerateDialog("WeaponDialogue.11", WeaponDialogDrops[106].Item1, WeaponDialogDrops[106].Item2);
            Dialogue[107] = GenerateDialog("WeaponDialogue.13", WeaponDialogDrops[107].Item1, WeaponDialogDrops[107].Item2);
            Dialogue[108] = GenerateDialog("WeaponDialogue.15", WeaponDialogDrops[108].Item1, WeaponDialogDrops[108].Item2);
            Dialogue[109] = GenerateDialog("WeaponDialogue.17", WeaponDialogDrops[109].Item1, WeaponDialogDrops[109].Item2);
            Dialogue[110] = GenerateDialog("WeaponDialogue.19", WeaponDialogDrops[110].Item1, WeaponDialogDrops[110].Item2);
            Dialogue[111] = GenerateDialog("WeaponDialogue.20", WeaponDialogDrops[111].Item1, WeaponDialogDrops[111].Item2);
            Dialogue[112] = GenerateDialog("WeaponDialogue.21", WeaponDialogDrops[112].Item1, WeaponDialogDrops[112].Item2);
            Dialogue[115] = GenerateDialog("WeaponDialogue.23", WeaponDialogDrops[115].Item1, WeaponDialogDrops[115].Item2);
            Dialogue[116] = GenerateDialog("WeaponDialogue.25", WeaponDialogDrops[116].Item1, WeaponDialogDrops[116].Item2);
            Dialogue[117] = GenerateDialog("WeaponDialogue.27", WeaponDialogDrops[117].Item1, WeaponDialogDrops[117].Item2);
            Dialogue[118] = GenerateDialog("WeaponDialogue.29", WeaponDialogDrops[118].Item1, WeaponDialogDrops[118].Item2);
            Dialogue[119] = GenerateDialog("WeaponDialogue.30", WeaponDialogDrops[119].Item1, WeaponDialogDrops[119].Item2);
            Dialogue[120] = GenerateDialog("WeaponDialogue.31", WeaponDialogDrops[120].Item1, WeaponDialogDrops[120].Item2);
            Dialogue[121] = GenerateDialog("WeaponDialogue.32", WeaponDialogDrops[121].Item1, WeaponDialogDrops[121].Item2);
            Dialogue[122] = GenerateDialog("WeaponDialogue.33", WeaponDialogDrops[122].Item1, WeaponDialogDrops[122].Item2);
            Dialogue[123] = GenerateDialog("WeaponDialogue.34", WeaponDialogDrops[123].Item1, WeaponDialogDrops[123].Item2);
            Dialogue[124] = GenerateDialog("WeaponDialogue.35", WeaponDialogDrops[124].Item1, WeaponDialogDrops[124].Item2);
            Dialogue[125] = GenerateDialog("WeaponDialogue.37", WeaponDialogDrops[125].Item1, WeaponDialogDrops[125].Item2);
            Dialogue[126] = GenerateDialog("WeaponDialogue.38", WeaponDialogDrops[126].Item1, WeaponDialogDrops[126].Item2);
            Dialogue[127] = GenerateDialog("WeaponDialogue.39", WeaponDialogDrops[127].Item1, WeaponDialogDrops[127].Item2);
            Dialogue[128] = GenerateDialog("WeaponDialogue.40", WeaponDialogDrops[128].Item1, WeaponDialogDrops[128].Item2);
            Dialogue[129] = GenerateDialog("WeaponDialogue.41", WeaponDialogDrops[129].Item1, WeaponDialogDrops[129].Item2);
            Dialogue[130] = GenerateDialog("WeaponDialogue.42", WeaponDialogDrops[130].Item1, WeaponDialogDrops[130].Item2);
            Dialogue[131] = GenerateDialog("WeaponDialogue.43", WeaponDialogDrops[131].Item1, WeaponDialogDrops[131].Item2);
            Dialogue[132] = GenerateDialog("WeaponDialogue.44", WeaponDialogDrops[132].Item1, WeaponDialogDrops[132].Item2);
            Dialogue[133] = GenerateDialog("WeaponDialogue.45", WeaponDialogDrops[133].Item1, WeaponDialogDrops[133].Item2);
            Dialogue[134] = GenerateDialog("WeaponDialogue.46", WeaponDialogDrops[134].Item1, WeaponDialogDrops[134].Item2);
            Dialogue[135] = GenerateDialog("WeaponDialogue.47", WeaponDialogDrops[135].Item1, WeaponDialogDrops[135].Item2);
            Dialogue[136] = GenerateDialog("WeaponDialogue.48", WeaponDialogDrops[136].Item1, WeaponDialogDrops[136].Item2);
            Dialogue[137] = GenerateDialog("WeaponDialogue.49", WeaponDialogDrops[137].Item1, WeaponDialogDrops[137].Item2);
            Dialogue[138] = GenerateDialog("WeaponDialogue.50", WeaponDialogDrops[138].Item1, WeaponDialogDrops[138].Item2);
            Dialogue[139] = GenerateDialog("WeaponDialogue.Perseus", ModContent.Find<ModItem>("StarsAbove/EssenceOfBloodshed").Type);
            Dialogue[140] = GenerateDialog("WeaponDialogue.51", WeaponDialogDrops[140].Item1, WeaponDialogDrops[140].Item2);
            Dialogue[141] = GenerateDialog("WeaponDialogue.53", WeaponDialogDrops[141].Item1, WeaponDialogDrops[141].Item2);
            Dialogue[142] = GenerateDialog("WeaponDialogue.54", WeaponDialogDrops[142].Item1, WeaponDialogDrops[142].Item2);
            Dialogue[143] = GenerateDialog("WeaponDialogue.55", WeaponDialogDrops[143].Item1, WeaponDialogDrops[143].Item2);
            Dialogue[144] = GenerateDialog("WeaponDialogue.57", WeaponDialogDrops[144].Item1, WeaponDialogDrops[144].Item2);
            Dialogue[145] = GenerateDialog("WeaponDialogue.59", WeaponDialogDrops[145].Item1, WeaponDialogDrops[145].Item2);
            Dialogue[146] = GenerateDialog("WeaponDialogue.60", WeaponDialogDrops[146].Item1, WeaponDialogDrops[146].Item2);
            Dialogue[147] = GenerateDialog("WeaponDialogue.61", WeaponDialogDrops[147].Item1, WeaponDialogDrops[147].Item2);
            Dialogue[148] = GenerateDialog("WeaponDialogue.62", WeaponDialogDrops[148].Item1, WeaponDialogDrops[148].Item2);
            Dialogue[149] = GenerateDialog("WeaponDialogue.63", WeaponDialogDrops[149].Item1, WeaponDialogDrops[149].Item2);
            Dialogue[150] = GenerateDialog("WeaponDialogue.64", WeaponDialogDrops[150].Item1, WeaponDialogDrops[150].Item2);
            Dialogue[151] = GenerateDialog("WeaponDialogue.66", WeaponDialogDrops[151].Item1, WeaponDialogDrops[151].Item2);
            Dialogue[152] = GenerateDialog("WeaponDialogue.68", WeaponDialogDrops[152].Item1, WeaponDialogDrops[152].Item2);
            Dialogue[153] = GenerateDialog("WeaponDialogue.70", WeaponDialogDrops[153].Item1, WeaponDialogDrops[153].Item2);
            Dialogue[154] = GenerateDialog("WeaponDialogue.72", WeaponDialogDrops[154].Item1, WeaponDialogDrops[154].Item2);
            Dialogue[155] = GenerateDialog("WeaponDialogue.73", WeaponDialogDrops[155].Item1, WeaponDialogDrops[155].Item2);
            Dialogue[156] = GenerateDialog("WeaponDialogue.74", WeaponDialogDrops[156].Item1, WeaponDialogDrops[156].Item2);
            Dialogue[157] = GenerateDialog("WeaponDialogue.76", WeaponDialogDrops[157].Item1, WeaponDialogDrops[157].Item2);
            Dialogue[158] = GenerateDialog("WeaponDialogue.76", WeaponDialogDrops[158].Item1, WeaponDialogDrops[158].Item2);
            Dialogue[301] = GenerateDialog("BossItemDialogue.Nalhaun", ModContent.Find<ModItem>("StarsAbove/AncientShard").Type);
            Dialogue[302] = GenerateDialog("BossItemDialogue.Penth", ModContent.Find<ModItem>("StarsAbove/UnsulliedCanvas").Type);
            Dialogue[303] = GenerateDialog("BossItemDialogue.Arbitration", ModContent.Find<ModItem>("StarsAbove/DemonicCrux").Type);
            Dialogue[304] = GenerateDialog("BossItemDialogue.Warrior", ModContent.Find<ModItem>("StarsAbove/ProgenitorWish").Type);
        }

        object GenerateDialog(string type, int itemID = -1, int itemCount = 1)
        {
            string path = GetLocalizationPath() + ".Dialog." + type;

            return API.AddSpatialDiskDialogDatadriven(path, 0, return_false, itemID, itemCount, CustomDrawDialog, CustomDrawDialog_OverUI);
        }

        public virtual void CustomDrawDialog(SpriteBatch SpriteBatch, int Expression, Rectangle Bounds, Color Alpha)
        {
        }
        public virtual void CustomDrawExpression(SpriteBatch SpriteBatch, int Expression, Rectangle Bounds, Color Alpha)
        {
        }
        public virtual void CustomDrawDialog_OverUI(SpriteBatch SpriteBatch, int Expression, Rectangle Bounds, Color Alpha)
        {
        }
        public virtual void CustomDrawExpression_OverUI(SpriteBatch SpriteBatch, int Expression, Rectangle Bounds, Color Alpha)
        {
        }

        static bool return_false() { return false; }

        //public override bool HasCustomNovaCutInDialog => true;
        /*public override string ReplaceNovaCutInUIDrawInfo(SpriteBatch SpriteBatch, int ChosenStellarNova, int NovaCutInTimer, int RandomNovaDialogue, bool Shades, Rectangle Bounds, Rectangle Triangle1, Rectangle Triangle2, Color Opacity)
        {
            return base.ReplaceNovaCutInUIDrawInfo(SpriteBatch, ChosenStellarNova, NovaCutInTimer, RandomNovaDialogue, Shades, Bounds, Triangle1, Triangle2, Opacity);
        }*/


        public override bool ReplaceDialogue(Mod DialogSourceMod, float DialogPriority)
        {
            if (DialogSourceMod.Name == "StarsAbove")
            {
                switch (DialogPriority)
                {
                    case 2:
                        if (Main.hardMode)
                        {
                            API.ForceOpenSpatialDiskDialogWindow(Dialog2_Hardmode);
                            return true;
                        }
                        break;
                    case 139:
                        return false; //Perzeus
                }
                API.ForceOpenSpatialDiskDialogWindow(Dialogue[(int)DialogPriority]);
                return true;
            }
            return false;
        }

        public override bool ReplaceExpression(Mod ExpressionSourceMod, string ExpressionText, int ExpressionFace, int ExpressionDuration)
        {
            string Localization = GetLocalizationPath() + ".Expression." + ExpressionText + ".";
            string TextPath = Localization + "Text";
            string CustomFacePath = Localization + "Expression";
            string FaceNumber = Language.GetText(CustomFacePath).Value;
            if (CustomFacePath != FaceNumber)
            {
                int Face = int.Parse(FaceNumber);
                API.SetPromptExpression(Localization, TextPath, Face, ExpressionDuration, CustomDraw: CustomDrawExpression, CustomDrawOverUI: CustomDrawExpression_OverUI);
            }
            else
            {
                API.SetPromptExpression(Localization, TextPath, ExpressionFace, ExpressionDuration, CustomDraw: CustomDrawExpression, CustomDrawOverUI: CustomDrawExpression_OverUI);
            }
            return true;
        }
    }
    public abstract class StarsAboveStarfarer_Custom : ModType
    {
        public object CustomStarfarerObject = null;

        public virtual string StarfarerName => "";
        protected override sealed void Register()
        {
            if (API.StarsAboveAPI == null) return;
            if (API.YourMod == null)
                API.YourMod = Mod;
            CustomStarfarerObject = API.AddNewStarfarer(StarfarerName, StarfarerToSpoof, ReplaceExpression, ReplaceExpressionDrawInfo, ReplaceExpressionDrawInfoOverUI, ReplaceDialogue, ReplaceDialogueDrawInfo, ReplaceDialogueDrawInfoOverUI, ReplaceVN, ReplaceVNDrawInfo, ReplaceStarfarerUIDrawInfo, ReplaceStarfarerUIDrawInfoBlink, ReplaceNovaUIDrawInfo, ReplaceNovaUIDrawInfoBlink, HasCustomNovaCutInDialog ? ReplaceNovaCutInUIDrawInfo : null,
                CustomNovaDialog, OnActivateStellarNova,
                MenuOnOpen(0) == null ? null : MenuOnOpen,
                MenuOnArchiveConfirmButton(true) == null ? null : MenuOnArchiveConfirmButton,
                MenuOnArchiveHoverButton == null ? null : () => MenuOnArchiveHoverButton,
                MenuOnConfirmHoverButton == null ? null : () => MenuOnConfirmHoverButton,
                MenuOnStellarArrayHoverButton == null ? null : () => MenuOnStellarArrayHoverButton,
                MenuOnStellarArrayConfirmButton == null ? null : () => MenuOnStellarArrayHoverButton,
                MenuOnStellarNovaHoverButton(true) == null ? null : MenuOnStellarNovaHoverButton,
                MenuOnStellarNovaConfirmButton == null ? null : () => MenuOnStellarNovaConfirmButton,
                MenuOnVoyageHoverButton == null ? null : () => MenuOnVoyageHoverButton,
                MenuOnArmorSlotHover == null ? null : () => MenuOnArmorSlotHover,
                MenuOnVanitySlotHover == null ? null : () => MenuOnVanitySlotHover,
                NovaOnEdinGenesisQuasarHover(0) == null ? null : NovaOnEdinGenesisQuasarHover,
                NovaOnGardenOfAvalonHover(0) == null ? null : NovaOnGardenOfAvalonHover,
                NovaOnKiwamiRyukenHover(0) == null ? null : NovaOnKiwamiRyukenHover,
                NovaOnLaevateinnHover(0) == null ? null : NovaOnLaevateinnHover,
                NovaOnTheofaniaHover(0) == null ? null : NovaOnTheofaniaHover,
                NovaOnAffix1Hover == null ? null : () => NovaOnAffix1Hover,
                NovaOnAffix2Hover == null ? null : () => NovaOnAffix2Hover,
                NovaOnAffix3Hover == null ? null : () => NovaOnAffix3Hover,
                NovaOnConfirmHover == null ? null : () => NovaOnConfirmHover,
                NovaOnResetHover == null ? null : () => NovaOnResetHover,
                NovaSpecialAffixHover == null ? null : () => NovaSpecialAffixHover
                );
            SetDefaults();
        }

        public virtual void SetDefaults() { }
        public virtual int StarfarerToSpoof => 1;
        //expressions
        public virtual bool ReplaceExpression(Mod ExpressionSourceMod, string ExpressionText, int ExpressionFace, int ExpressionDuration) { return false; } //takes in mod and string, does stuff. Forcibly call SetPromptExpression in this.
        public virtual bool ReplaceExpressionDrawInfo(Mod ExpressionSourceMod, string ExpressionText, SpriteBatch SpriteBatch, int BlinkTimer, Rectangle Bounds, Color Alpha) { return false; } //return false if you dont wanna replace
        public virtual bool ReplaceExpressionDrawInfoOverUI(Mod ExpressionSourceMod, string ExpressionText, SpriteBatch SpriteBatch, int BlinkTimer, Rectangle Bounds, Color Alpha) { return false; } //return false if you dont wanna replace

        //dialogue
        public virtual bool ReplaceDialogue(Mod DialogSourceMod, float DialogPriority) { return false; } //call ForceOpenSpatialDiskDialogWindow
        public virtual bool ReplaceDialogueDrawInfo(Mod DialogSourceMod, float DialogPriority, SpriteBatch SpriteBatch, int BlinkTimer, Rectangle Bounds, Color Alpha) { return false; }
        public virtual bool ReplaceDialogueDrawInfoOverUI(Mod DialogSourceMod, float DialogPriority, SpriteBatch SpriteBatch, int BlinkTimer, Rectangle Bounds, Color Alpha) { return false; }

        //VN
        public virtual bool ReplaceVN(Mod VNSourceMod, float VNPriority) { return false; } //call OpenVN
        bool ReplaceVNDrawInfo(Mod VNSourceMod, float VNPriority, SpriteBatch SpriteBatch, UIState VN, int SceneProgression, string DialogVisible, Tuple<string, int, int, Rectangle, Color>[] DrawData, int BlinkTimer)
        {
            return ReplaceVNDrawInfo(VNSourceMod, VNPriority, SpriteBatch, VN, SceneProgression, DialogVisible, VNDrawData.ToDrawDataArray(DrawData), BlinkTimer);
        }
        public virtual bool ReplaceVNDrawInfo(Mod VNSourceMod, float VNPriority, SpriteBatch SpriteBatch, UIState VN, int SceneProgression, string DialogVisible, VNDrawData[] DrawData, int BlinkTimer) { return false; }

        //UI Draws
        public virtual bool ReplaceStarfarerUIDrawInfo(SpriteBatch SpriteBatch, int VagrantProgress, Rectangle Bounds, Color Opacity, Color CostumeChangeVisibility) { return false; } //takes in VagrantDialog. Second color is costume change visibility
        public virtual bool ReplaceStarfarerUIDrawInfoBlink(SpriteBatch SpriteBatch, int BlinkTimer, bool Shades, int VagrantProgress, Rectangle Bounds, Color Opacity) { return false; } //takes in blinkTimer, shades, VagrantDialog
        public virtual bool ReplaceNovaUIDrawInfo(SpriteBatch SpriteBatch, Rectangle Bounds, Color Opacity) { return false; }
        public virtual bool ReplaceNovaUIDrawInfoBlink(SpriteBatch SpriteBatch, int BlinkTimer, Rectangle Bounds, Color Opacity) { return false; } //takes in blinkTimer
        public virtual bool HasCustomNovaCutInDialog => false;
        public virtual string ReplaceNovaCutInUIDrawInfo(SpriteBatch SpriteBatch, int ChosenStellarNova, int NovaCutInTimer, int RandomNovaDialogue, bool Shades, Rectangle Bounds, Rectangle Triangle1, Rectangle Triangle2, Color Opacity) { return ""; } //takes in chosenStellarNova, NovaCutInTimer, randomNovaDialogue, shades. Rectangles are A0, AS1, AS respectively.

        //Nova
        public virtual bool CustomNovaDialog(int ChosenStellarNova, int RandomNovaDialog) { return false; } //takes in chosenStellarNova, randomNovaDialog. Return false if no voice.
        public virtual bool OnActivateStellarNova(Player Player, int ChosenStellarNova, Vector2 Position) { return false; } //takes in player, chosenStellarNova, position

        //Menu
        public virtual string MenuOnOpen(int inCombat) { return null; } //takes in inCombat
        public virtual string MenuOnArchiveConfirmButton(bool archiveActive) { return null; } //takes in archiveActive
        public virtual string MenuOnArchiveHoverButton => null;
        public virtual string MenuOnConfirmHoverButton => null;
        public virtual string MenuOnStellarArrayHoverButton => null;
        public virtual string MenuOnStellarArrayConfirmButton => null;
        public virtual string MenuOnStellarNovaHoverButton(bool unlocked) { return null; } //takes in unlocked
        public virtual string MenuOnStellarNovaConfirmButton => null;
        public virtual string MenuOnVoyageHoverButton => null;
        public virtual string MenuOnArmorSlotHover => null;
        public virtual string MenuOnVanitySlotHover => null;

        //Nova Menu
        public virtual string NovaOnEdinGenesisQuasarHover(int unlockedStage) { return null; } //takes in unlocked
        public virtual string NovaOnGardenOfAvalonHover(int unlockedStage) { return null; } //takes in unlocked
        public virtual string NovaOnKiwamiRyukenHover(int unlockedStage) { return null; } //takes in unlocked
        public virtual string NovaOnLaevateinnHover(int unlockedStage) { return null; } //takes in unlocked
        public virtual string NovaOnTheofaniaHover(int unlockedStage) { return null; } //takes in unlocked

        public virtual string NovaOnAffix1Hover => null;
        public virtual string NovaOnAffix2Hover => null;
        public virtual string NovaOnAffix3Hover => null;

        public virtual string NovaOnConfirmHover => null;

        public virtual string NovaOnResetHover => null;
        public virtual string NovaSpecialAffixHover => null;
    }
    public class VNDrawData
    {

        public string Character;
        public int CharacterPoseID;
        public int CharacterExpressionID;
        public Rectangle CharacterDrawRectangle;
        public Color CharacterAlpha;
        public VNDrawData(Tuple<string, int, int, Rectangle, Color> rawTuple)
        {
            Character = rawTuple.Item1;
            CharacterPoseID = rawTuple.Item2;
            CharacterExpressionID = rawTuple.Item3;
            CharacterDrawRectangle = rawTuple.Item4;
            CharacterAlpha = rawTuple.Item5;
        }

        public static VNDrawData[] ToDrawDataArray(params Tuple<string, int, int, Rectangle, Color>[] rawTuple)
        {
            VNDrawData[] rv = new VNDrawData[rawTuple.Length];
            for (int x = 0; x < rv.Length; x++)
            {
                rv[x] = new(rawTuple[x]);
            }
            return rv;
        }
    }

    public abstract class StarsAboveVN_Custom : ModType
    {
        public bool AlreadySeen = false;
        public int ID = 0;
        public virtual float Priority => 1;
        public virtual DialogOption[] DialogOptions => null;

        Tuple<string, int>[] dialogOptions;

        public virtual bool UsesCustomDrawLogic => false;

        public struct DialogOption
        {
            public string Text;
            public int VNSceneID;
        }
        protected override sealed void Register()
        {
            if (API.StarsAboveAPI == null) return;
            if (API.YourMod == null)
                API.YourMod = Mod;
            ID = API.AddModdedVNScene(VNLogic, UsesCustomDrawLogic ? CustomDrawLogic : null, DialogOptions != null ? GetDialogOptions : null, Priority, condition, OnDialogAdvanced, OnOptionPressed);
        }

        Tuple<string, int>[] GetDialogOptions()
        {
            if (dialogOptions == null)
            {
                DialogOption[] orig = DialogOptions;
                dialogOptions = new Tuple<string, int>[orig.Length];
                for (int x = 0; x < orig.Length; x++)
                {
                    dialogOptions[x] = new(orig[x].Text, orig[x].VNSceneID);
                }
            }
            return dialogOptions;
        }

        bool condition()
        {
            if (!Condition() || AlreadySeen)
                return false;
            AlreadySeen = true;
            return true;
        }

        public virtual bool Condition() { return true; }

        public virtual void OnDialogAdvanced() { }

        public virtual void OnOptionPressed(int OptionPressed) { }
        void CustomDrawLogic(SpriteBatch spriteBatch, UIState UI, int SceneProgression, string CurrentSpeaker, Tuple<string, int, int, Rectangle, Color>[] RawDrawData, int BlinkTimer)
        {
            CustomDrawLogic(spriteBatch, UI, SceneProgression, CurrentSpeaker, VNDrawData.ToDrawDataArray(RawDrawData), BlinkTimer);
        }

        public virtual void CustomDrawLogic(SpriteBatch spriteBatch, UIState UI, int SceneProgression, string CurrentSpeaker, VNDrawData[] RawDrawData, int BlinkTimer)
        {

        }

        void VNLogic(int stage, object[] returnData)
        {
            int sceneLength = 0; //0
            bool sceneHasChoice = false; // 1
            string sceneChoice1 = ""; //2
            string sceneChoice2 = ""; //3
            int choice1Scene = 0; //4
            int choice2Scene = 0; //5
            string character1 = ""; //6
            int character1Pose = 0; //7
            int character1Expression = 0; //8
            string character2 = "None"; //9
            int character2Pose = 0; //10
            int character2Expression = 0; //11
            string name = ""; //12
            string dialogue = ""; //13

            VNLogic(stage, ref sceneLength, ref sceneHasChoice, ref sceneChoice1, ref sceneChoice2, ref choice1Scene, ref choice2Scene, ref character1, ref character1Pose, ref character1Expression, ref character2, ref character2Pose, ref character2Expression, ref name, ref dialogue);

            returnData[0] = sceneLength;
            returnData[1] = sceneHasChoice;
            returnData[2] = sceneChoice1;
            returnData[3] = sceneChoice2;
            returnData[4] = choice1Scene;
            returnData[5] = choice2Scene;
            returnData[6] = character1;
            returnData[7] = character1Pose;
            returnData[8] = character1Expression;
            returnData[9] = character2;
            returnData[10] = character2Pose;
            returnData[11] = character2Expression;
            returnData[12] = name;
            returnData[13] = dialogue;
        }

        public virtual void VNLogic(int stage,
            ref int sceneLength,
            ref bool sceneHasChoice,
            ref string sceneChoice1,
            ref string sceneChoice2,
            ref int choice1SceneID,
            ref int choice2SceneID,
            ref string character1,
            ref int character1Pose,
            ref int character1Expression,
            ref string character2,
            ref int character2Pose,
            ref int character2Expression,
            ref string speakerName,
            ref string dialogue)
        { }
    }
}