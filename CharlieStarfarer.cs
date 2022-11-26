using CharlieStarfarer.Starfarers.Charlie;
using StarsAboveAPI;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

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
            OnDialogAdvanced: CharlieSet);
            CharlieStarfarerUnswap_Dialog = API.AddSpatialDiskDialogDatadriven("Mods.CharlieStarfarer.Dialog.Charlie_Unset", 10, () => false,
            OnDialogAdvanced: CharlieUnset);
            API.AddSpatialDiskDialogArchive(2, 0.12111111f, () => {
                if (API.GetCurrentStarfarer(Main.LocalPlayer).Item2 == ModContent.GetInstance<Charlie>().CustomStarfarerObject)
                    return new("Charlie's Vacation", "Get your original Starfarer back", CharlieStarfarerUnswap_Dialog);
                return new("Charlie's Apotheosis", "Switch your Starfarer to Charlie", CharlieStarfarerSwap_Dialog);
            });

            //temp parser 
            /*
            string datadriven = "";

            Logger.Info(TEMPORARY_PARSER.Parse(ref datadriven));
            Logger.Info(datadriven);*/
        }
        public void CharlieSet()
        {
            API.SetStarfarer(Main.LocalPlayer, ModContent.GetInstance<Charlie>().CustomStarfarerObject);
        }
        public void CharlieUnset()
        {
            API.ResetStarfarer(Main.LocalPlayer);
        }
    }
}