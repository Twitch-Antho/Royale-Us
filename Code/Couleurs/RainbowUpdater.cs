using System;
using Reactor.Utilities.Attributes;
using UnityEngine;

namespace RoyaleUs.Couleurs;

/// <summary>Fait tourner la teinte de la couleur « Arc-en-ciel » et la réapplique aux joueurs qui l'ont.</summary>
[RegisterInIl2Cpp]
public class RainbowUpdater : MonoBehaviour
{
    private const float CycleSeconds = 4f;
    private const float RefreshInterval = 0.05f;

    private float _timer;

    public RainbowUpdater(IntPtr ptr) : base(ptr)
    {
    }

    private void Update()
    {
        var id = CustomColors.RainbowId;
        if (id < 0 || id >= Palette.PlayerColors.Length) return;

        _timer += Time.deltaTime;
        if (_timer < RefreshInterval) return;
        _timer = 0f;

        var hue = Time.time / CycleSeconds % 1f;
        Palette.PlayerColors[id] = Color.HSVToRGB(hue, 0.8f, 0.9f);
        Palette.ShadowColors[id] = Color.HSVToRGB(hue, 0.8f, 0.55f);

        Palette.TextColors[id] = Palette.PlayerColors[id];

        foreach (var player in PlayerControl.AllPlayerControls)
        {
            if (!player || player.Data == null || player.Data.DefaultOutfit == null) continue;
            if (player.Data.DefaultOutfit.ColorId != id || !player.cosmetics) continue;
            player.cosmetics.SetColor(id);
        }
    }
}
