using AmongUs.Data.Legacy;
using HarmonyLib;
using UnityEngine;

namespace RoyaleUs.Couleurs;

internal static class ColorPatches
{
    // Réorganise les pastilles de couleur en grille pour qu'elles tiennent toutes dans l'onglet.
    [HarmonyPatch(typeof(PlayerTab), nameof(PlayerTab.OnEnable))]
    internal static class PlayerTab_OnEnable
    {
        private const int Columns = 7;

        public static PlayerTab Current;

        public static void Postfix(PlayerTab __instance)
        {
            Current = __instance;
            var chips = __instance.ColorChips;
            for (var i = 0; i < chips.Count; i++)
            {
                var chip = chips[i];
                if (!chip) continue;
                int row = i / Columns, col = i % Columns;
                chip.transform.localPosition = new Vector3(-0.975f + col * 0.5f, 1.475f - row * 0.5f, chip.transform.localPosition.z);
                chip.transform.localScale = Vector3.one * 0.76f;
            }
        }
    }

    // Côté hôte : accepte les nouvelles couleurs et évite les doublons.
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CheckColor))]
    internal static class PlayerControl_CheckColor
    {
        private static bool IsTaken(PlayerControl player, uint color)
        {
            foreach (var info in GameData.Instance.AllPlayers)
            {
                if (info && !info.Disconnected && info.PlayerId != player.PlayerId && info.DefaultOutfit.ColorId == color)
                    return true;
            }
            return false;
        }

        public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] byte bodyColor)
        {
            var total = (uint) CustomColors.Total;
            uint color = bodyColor;
            var tries = 0;
            while (tries++ < total && (color >= total || IsTaken(__instance, color)))
                color = (color + 1) % total;

            __instance.RpcSetColor((byte) color);
            return false;
        }
    }

    // Une couleur sauvegardée qui n'existe plus (mod retiré puis remis) est ramenée dans la palette.
    [HarmonyPatch(typeof(LegacySaveManager), nameof(LegacySaveManager.LoadPlayerPrefs))]
    internal static class LegacySaveManager_LoadPlayerPrefs
    {
        public static void Postfix()
        {
            if (LegacySaveManager.colorConfig >= CustomColors.Total)
                LegacySaveManager.colorConfig = 0;
        }
    }
}
