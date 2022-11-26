using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StarsAboveAPI;
using Terraria.GameContent;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;

namespace CharlieStarfarer.Starfarers.Charlie
{
    internal class Charlie : StarsAboveStarfarer_Datadriven
    {
        const string Path = "CharlieStarfarer/Starfarers/Charlie/";
        static Asset<Texture2D> Charlie_StellarNova;
        static Asset<Texture2D> Charlie_Menu_Body;
        static Asset<Texture2D> Charlie_Menu_Default;
        static Asset<Texture2D> Charlie_Menu_Blink;
        static Asset<Texture2D> Charlie_Menu_Shades;

        static Asset<Texture2D> Charlie_Nova_Menu_Default;

        public override string GetLocalizationPath()
        {
            return "Mods.CharlieStarfarer.Starfarers.Charlie";
        }

        internal class CharlieLoader : ILoadable
        {
            public void Load(Mod mod)
            {
                Charlie_StellarNova = Request<Texture2D>(Path + "CharlieStellarNova", AssetRequestMode.ImmediateLoad);
                Charlie_Menu_Body = Request<Texture2D>(Path + "CharlieMenuBody", AssetRequestMode.ImmediateLoad);
                Charlie_Menu_Default = Request<Texture2D>(Path + "CharlieMenuClothes_Triumphant", AssetRequestMode.ImmediateLoad);
                Charlie_Menu_Blink = Request<Texture2D>(Path + "CharlieMenuBlink", AssetRequestMode.ImmediateLoad);
                Charlie_Menu_Shades = Request<Texture2D>(Path + "CharlieMenuShades", AssetRequestMode.ImmediateLoad);

                Charlie_Nova_Menu_Default = Request<Texture2D>(Path + "CharlieNovaMenuClothes_Triumphant", AssetRequestMode.ImmediateLoad);
            }

            public void Unload()
            {
                Charlie_StellarNova = null;
            }
        }


        public override int StarfarerToSpoof => 2;
        /*

        */
        public override bool HasCustomNovaCutInDialog => true;
        public override bool CustomNovaDialog(int ChosenStellarNova, int RandomNovaDialog)
        {
            return true;
        }

        public override bool ReplaceStarfarerUIDrawInfo(SpriteBatch SpriteBatch, int VagrantProgress, Rectangle Bounds, Color Opacity, Color CostumeChangeVisibility)
        {
            SpriteBatch.Draw(Charlie_Menu_Body.Value, new Vector2(Bounds.X, Bounds.Y), Opacity);
            SpriteBatch.Draw(Charlie_Menu_Default.Value, new Vector2(Bounds.X, Bounds.Y), Opacity);
            return true;
        }
        public override bool ReplaceStarfarerUIDrawInfoBlink(SpriteBatch SpriteBatch, int BlinkTimer, bool Shades, int VagrantProgress, Rectangle Bounds, Color Opacity)
        {
            if (BlinkTimer > 70 && BlinkTimer < 75 ||
                BlinkTimer > 320 && BlinkTimer < 325 ||
                BlinkTimer > 420 && BlinkTimer < 425 ||
                BlinkTimer > 428 && BlinkTimer < 433)
                SpriteBatch.Draw(Charlie_Menu_Blink.Value, new Vector2(Bounds.X, Bounds.Y), Opacity);
            if (Shades)
                SpriteBatch.Draw(Charlie_Menu_Shades.Value, new Vector2(Bounds.X, Bounds.Y), Opacity);
            return true;
        }


        const int nova_menu_height_disp_default = 86;

        int GetNovaHeightDisp() {

            int outfit = API.GetCurrentVisibleAttire(Main.CurrentPlayer);
            int height_disp;
            switch (outfit)
            {
                default:
                    height_disp = nova_menu_height_disp_default;
                    break;
            }
            return height_disp;
        }
        public override bool ReplaceNovaUIDrawInfo(SpriteBatch SpriteBatch, Rectangle Bounds, Color Opacity)
        {
            int height_disp = GetNovaHeightDisp();

            SpriteBatch.Draw(Charlie_Menu_Body.Value, new Vector2(Bounds.X, Bounds.Y - height_disp), Opacity);
            SpriteBatch.Draw(Charlie_Nova_Menu_Default.Value, new Vector2(Bounds.X, Bounds.Y - height_disp), Opacity);
            SpriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/NovaE"), Bounds, Opacity);
            return true;
        }

        public override bool ReplaceNovaUIDrawInfoBlink(SpriteBatch SpriteBatch, int BlinkTimer, Rectangle Bounds, Color Opacity)
        {
            int height_disp = GetNovaHeightDisp();
            Bounds.Y -= height_disp;
            return ReplaceStarfarerUIDrawInfoBlink(SpriteBatch, BlinkTimer, false, 2, Bounds, Opacity);
        }

        public override string ReplaceNovaCutInUIDrawInfo(SpriteBatch SpriteBatch, int ChosenStellarNova, int NovaCutInTimer, int RandomNovaDialogue, bool Shades, Rectangle Bounds, Rectangle Triangle1, Rectangle Triangle2, Color Opacity)
        {
            Texture2D Charlie = Charlie_StellarNova.Value;
            SpriteBatch.Draw(Charlie, new Rectangle(Bounds.Center.X - Charlie.Width / 2, Bounds.Top, Charlie.Width, Charlie.Height), Opacity);
            return "Be eclipsced by\nTheir pearlescence!";
        }
    }
}
