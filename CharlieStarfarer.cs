using CharlieStarfarer.Starfarers.Charlie;
using StarsAboveAPI;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using CharlieStarfarer.Starfarers.Charlie.VNs.PerseusDefeated;

namespace CharlieStarfarer
{
    public class CharlieStarfarer : Mod
    {
        object CharlieStarfarerSwap_Dialog;
        object CharlieStarfarerUnswap_Dialog;
        bool CharlieStarfarerSeen = false;
        public override void PostSetupContent()
        {
            CharlieStarfarerSwap_Dialog = API.AddSpatialDiskDialogDatadriven("Mods.CharlieStarfarer.Dialog.Charlie_Set", 10, () => false,
            OnDialogAdvanced: CharlieSet, customDraw: ModContent.GetInstance<Charlie>().CustomDrawDialog, customDrawOverUI: ModContent.GetInstance<Charlie>().CustomDrawDialog_OverUI);
            CharlieStarfarerUnswap_Dialog = API.AddSpatialDiskDialogDatadriven("Mods.CharlieStarfarer.Dialog.Charlie_Unset", 10, () => false,
            OnDialogAdvanced: CharlieUnset, customDraw: ModContent.GetInstance<Charlie>().CustomDrawDialog, customDrawOverUI: ModContent.GetInstance<Charlie>().CustomDrawDialog_OverUI);
            API.AddSpatialDiskDialogArchive(2, 0.12111111f, () => {
                if (API.GetCurrentStarfarer(Main.LocalPlayer).Item2 == ModContent.GetInstance<Charlie>().CustomStarfarerObject)
                    return new("Charlie's Vacation", "Get your original Starfarer back", CharlieStarfarerUnswap_Dialog);
                return new("Charlie's Apotheosis", "Switch your Starfarer to Charlie", CharlieStarfarerSwap_Dialog);
            });

            API.AddSpatialDiskDialogArchive(2, 0.12111111f, () => {
                return new("Test", "test", ModContent.GetInstance<PerseusDefeated>().ID);
            });

            //temp parser 
            /*
            string datadriven = "";

            Logger.Info(TEMPORARY_PARSER.Parse(ref datadriven));
            Logger.Info(datadriven);*/
        }
        public void CharlieSet()
        {
            object Charlie = ModContent.GetInstance<Charlie>().CustomStarfarerObject;
            if (API.GetCurrentStarfarer(Main.LocalPlayer).Item2 != Charlie)
                API.ResetPopup(null, () => { }, () => { });
            API.SetStarfarer(Main.LocalPlayer, Charlie);
        }
        public void CharlieUnset()
        {
            object Charlie = ModContent.GetInstance<Charlie>().CustomStarfarerObject;
            if (API.GetCurrentStarfarer(Main.LocalPlayer).Item2 == Charlie)
                API.ResetPopup(null, () => { }, () => { });
            API.ResetStarfarer(Main.LocalPlayer);
        }
    }
}