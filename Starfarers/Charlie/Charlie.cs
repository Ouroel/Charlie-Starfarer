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
using CharlieStarfarer.Starfarers.Charlie.VNs.PerseusDefeated;

namespace CharlieStarfarer.Starfarers.Charlie
{
    internal class Charlie : StarsAboveStarfarer_Datadriven
    {
        const string Path = "CharlieStarfarer/Starfarers/Charlie/";
        public override StarsAboveVN_Custom PerseusDefeated => ModContent.GetInstance<PerseusDefeated>();
        static Asset<Texture2D>[] Charlie_StellarNova;
        static Asset<Texture2D> Charlie_Menu_Body;
        static Asset<Texture2D>[] Charlie_Menu_Clothes;
        static Asset<Texture2D> Charlie_Menu_Blink;
        static Asset<Texture2D>[] Charlie_Menu_Shades;

        static Asset<Texture2D>[] Charlie_Nova_Menu_Default;

        static Asset<Texture2D>[] Charlie_Dialog_Face;
        static Asset<Texture2D>[] Charlie_Dialog_Blink;
        static Asset<Texture2D>[][] Charlie_Dialog_Skin;
        static Asset<Texture2D>[][] Charlie_Dialog_Shades;
        static Asset<Texture2D>[][] Charlie_Dialog_OverUI;

        public override string GetLocalizationPath()
        {
            return "Mods.CharlieStarfarer.Starfarers.Charlie";
        }

        internal class CharlieLoader : ILoadable
        {
            public void Load(Mod mod)
            {
                Charlie_Menu_Body = Request<Texture2D>(Path + "CharlieMenuBody", AssetRequestMode.ImmediateLoad);
                Charlie_Menu_Blink = Request<Texture2D>(Path + "CharlieMenuBlink", AssetRequestMode.ImmediateLoad);


                LoadExpressions();

                Charlie_Dialog_Skin = new Asset<Texture2D>[4][];
                Charlie_Dialog_Shades = new Asset<Texture2D>[4][];
                Charlie_Dialog_OverUI = new Asset<Texture2D>[4][];

                Charlie_Menu_Clothes = new Asset<Texture2D>[4];
                Charlie_Menu_Shades = new Asset<Texture2D>[4];
                Charlie_Nova_Menu_Default = new Asset<Texture2D>[4];
                Charlie_StellarNova = new Asset<Texture2D>[4];

                LoadSkin(Path + "Triumphant", 0);
            }

            void LoadSkin(string Path, int index)
            {
                Charlie_Menu_Clothes[index] = Request<Texture2D>(Path + "/CharlieMenuClothes", AssetRequestMode.ImmediateLoad);
                Charlie_Menu_Shades[index] = Request<Texture2D>(Path + "/CharlieMenuShades", AssetRequestMode.ImmediateLoad);
                Charlie_StellarNova[index] = Request<Texture2D>(Path + "/CharlieStellarNova", AssetRequestMode.ImmediateLoad);

                Charlie_Nova_Menu_Default[index] = Request<Texture2D>(Path + "/CharlieNovaMenuClothes", AssetRequestMode.ImmediateLoad);
                LoadExpressions_Skins(Path + "/Expressions/", index, Charlie_Dialog_Skin, Charlie_Dialog_Shades, Charlie_Dialog_OverUI);
            }

            void LoadExpressions_Skins(string path, int index, Asset<Texture2D>[][] Clothes_Data, Asset<Texture2D>[][] Shades_Data, Asset<Texture2D>[][] OverUI_Data)
            {
                Clothes_Data[index] = new Asset<Texture2D>[20];
                Shades_Data[index] = new Asset<Texture2D>[20];
                OverUI_Data[index] = new Asset<Texture2D>[20];
                for (int x = 0; x < 20; x++)
                {
                    if (x == GetWhitelistedExpression(x))
                    {
                        Clothes_Data[index][x] = Request<Texture2D>(path + x + "_Clothes", AssetRequestMode.ImmediateLoad);
                        Shades_Data[index][x] = Request<Texture2D>(path + x + "_Shades", AssetRequestMode.ImmediateLoad);
                        OverUI_Data[index][x] = Request<Texture2D>(path + x + "_OverUI", AssetRequestMode.ImmediateLoad);
                    }
                }
            }
            void LoadExpressions()
            {
                Charlie_Dialog_Face = new Asset<Texture2D>[20];
                Charlie_Dialog_Blink = new Asset<Texture2D>[20];
                for (int x = 0; x < 20; x++)
                {
                    if (x == GetWhitelistedExpression(x))
                    {
                        Charlie_Dialog_Face[x] = Request<Texture2D>(Path + "Expressions/" + x + "_Face", AssetRequestMode.ImmediateLoad);
                        Charlie_Dialog_Blink[x] = Request<Texture2D>(Path + "Expressions/" + x + "_Blink", AssetRequestMode.ImmediateLoad);
                    }
                }
            }

            public void Unload()
            {
                Charlie_StellarNova = null;
                Charlie_Menu_Body = null;
                Charlie_Menu_Clothes = null;
                Charlie_Menu_Blink = null;
                Charlie_Menu_Shades = null;

                Charlie_Dialog_Face = null;
                Charlie_Dialog_Blink = null;

                Charlie_Dialog_Skin = null;
                Charlie_Dialog_Shades = null;
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

        static int GetWhitelistedExpression(int Expression) {

            switch (Expression)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    return Expression;
            }
            return 0;
        }

        public override void CustomDrawDialog_OverUI(SpriteBatch SpriteBatch, int Expression, Rectangle Bounds, Color Alpha)
        {
            int skin = GetSkin();
            Expression = GetWhitelistedExpression(Expression);

            SpriteBatch.Draw(Charlie_Dialog_OverUI[skin][Expression].Value, Bounds, Color.White);

        }
        public override void CustomDrawDialog(SpriteBatch SpriteBatch, int Expression, Rectangle Bounds, Color Alpha)
        {
            Expression = GetWhitelistedExpression(Expression);
            int skin = GetSkin();

            SpriteBatch.Draw(Charlie_Dialog_Face[Expression].Value, Bounds, Color.White);
            SpriteBatch.Draw(Charlie_Dialog_Skin[skin][Expression].Value, Bounds, Color.White);
            if (ShouldDrawBlink(API.GetBlinkTimer(Main.LocalPlayer)))
                SpriteBatch.Draw(Charlie_Dialog_Blink[Expression].Value, Bounds, Color.White);
            if (API.GetShadesOn())
                SpriteBatch.Draw(Charlie_Dialog_Shades[skin][Expression].Value, Bounds, Color.White);

        }

        int GetSkin() {

            int skin = API.GetCurrentVisibleAttire(Main.LocalPlayer);
            switch (skin)
            {
                case 0:
                    break;
                default:
                    skin = 0;
                    break;
            }
            return skin;
        }
        public override void CustomDrawExpression(SpriteBatch SpriteBatch, int Expression, Rectangle Bounds, Color Alpha)
        {
            CustomDrawDialog(SpriteBatch, Expression, Bounds, Alpha);
        }

        public override void CustomDrawExpression_OverUI(SpriteBatch SpriteBatch, int Expression, Rectangle Bounds, Color Alpha)
        {
            CustomDrawDialog_OverUI(SpriteBatch, Expression, Bounds, Alpha);
        }
        public override bool ReplaceStarfarerUIDrawInfo(SpriteBatch SpriteBatch, int VagrantProgress, Rectangle Bounds, Color Opacity, Color CostumeChangeVisibility)
        {
            int skin = GetSkin();
            SpriteBatch.Draw(Charlie_Menu_Body.Value, new Vector2(Bounds.X, Bounds.Y), Opacity);
            SpriteBatch.Draw(Charlie_Menu_Clothes[skin].Value, new Vector2(Bounds.X, Bounds.Y), Opacity);
            return true;
        }
        public override bool ReplaceStarfarerUIDrawInfoBlink(SpriteBatch SpriteBatch, int BlinkTimer, bool Shades, int VagrantProgress, Rectangle Bounds, Color Opacity)
        {
            int skin = GetSkin();
            if (ShouldDrawBlink(BlinkTimer))
                SpriteBatch.Draw(Charlie_Menu_Blink.Value, new Vector2(Bounds.X, Bounds.Y), Opacity);
            if (Shades)
                SpriteBatch.Draw(Charlie_Menu_Shades[skin].Value, new Vector2(Bounds.X, Bounds.Y), Opacity);
            return true;
        }

        bool ShouldDrawBlink(int BlinkTimer)
        {
            return (BlinkTimer > 70 && BlinkTimer < 75 ||
                    BlinkTimer > 320 && BlinkTimer < 325 ||
                    BlinkTimer > 420 && BlinkTimer < 425 ||
                    BlinkTimer > 428 && BlinkTimer < 433);
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

            int skin = GetSkin();
            SpriteBatch.Draw(Charlie_Menu_Body.Value, new Vector2(Bounds.X, Bounds.Y - height_disp), Opacity);
            SpriteBatch.Draw(Charlie_Nova_Menu_Default[skin].Value, new Vector2(Bounds.X, Bounds.Y - height_disp), Opacity);
            SpriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/NovaE"), Bounds, Opacity);
            return true;
        }

        public override bool ReplaceNovaUIDrawInfoBlink(SpriteBatch SpriteBatch, int BlinkTimer, Rectangle Bounds, Color Opacity)
        {
            int height_disp = GetNovaHeightDisp();
            Bounds.Y -= height_disp;
            return ReplaceStarfarerUIDrawInfoBlink(SpriteBatch, BlinkTimer, API.GetShadesOn(), 2, Bounds, Opacity);
        }

        public override string ReplaceNovaCutInUIDrawInfo(SpriteBatch SpriteBatch, int ChosenStellarNova, int NovaCutInTimer, int RandomNovaDialogue, bool Shades, Rectangle Bounds, Rectangle Triangle1, Rectangle Triangle2, Color Opacity)
        {
            int skin = GetSkin();
            Texture2D Charlie = Charlie_StellarNova[skin].Value;
            SpriteBatch.Draw(Charlie, new Rectangle(Bounds.Center.X - Charlie.Width / 2, Bounds.Top, Charlie.Width, Charlie.Height), Opacity);
            return "Be eclipsced by\nTheir pearlescence!";
        }
    }
}
