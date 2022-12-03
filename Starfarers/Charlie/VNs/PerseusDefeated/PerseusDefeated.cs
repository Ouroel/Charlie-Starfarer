using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using StarsAboveAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace CharlieStarfarer.Starfarers.Charlie.VNs.PerseusDefeated
{
    internal class PerseusDefeated : StarsAboveVN_Datadriven
    {
        public override string LocalizationPath => "Mods.CharlieStarfarer.Starfarers.Charlie.VN.PerseusDefeated";

        public override bool UsesCustomDrawLogic => true;

        public override void CustomDrawLogic(SpriteBatch spriteBatch, UIState UI, int SceneProgression, string CurrentSpeaker, VNDrawData[] RawDrawData, int BlinkTimer)
        {
            Texture2D tex = ModContent.Request<Texture2D>("CharlieStarfarer/Starfarers/Charlie/VNs/PerseusDefeated/test", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw(tex, RawDrawData[0].CharacterDrawRectangle.Center.ToVector2(), tex.Bounds, Color.White, 0, tex.Bounds.Center.ToVector2(), 1, SpriteEffects.None, 0);
        }
    }
}
