using System.Collections.Generic;
using System.Linq;
using Reactor.Localization.Utilities;
using UnityEngine;

namespace RoyaleUs.Couleurs;

public sealed record CustomColor(string Name, Color32 Main, Color32 Shadow);

public static class CustomColors
{
    /// <summary>Nombre de couleurs du jeu de base.</summary>
    public static int VanillaCount { get; private set; }

    /// <summary>Index de la couleur arc-en-ciel animée.</summary>
    public static int RainbowId { get; private set; } = -1;

    public static readonly List<CustomColor> Colors = new()
    {
        // Couleurs du mod Rainbow (MoltenMods)
        new("Sarcelle", new Color32(0, 128, 128, 255), new Color32(0, 100, 100, 255)),
        new("Olive", new Color32(99, 114, 24, 255), new Color32(66, 91, 15, 255)),
        new("Bordeaux", new Color32(176, 48, 96, 255), new Color32(112, 29, 60, 255)),
        new("Or", new Color32(218, 165, 32, 255), new Color32(156, 117, 22, 255)),
        new("Menthe", new Color32(168, 255, 195, 255), new Color32(123, 186, 143, 255)),
        new("Lavande", new Color32(201, 146, 224, 255), new Color32(156, 113, 173, 255)),

        // Couleurs en plus (inspirées du mod Extra Colors de MengTube)
        new("Chocolat", new Color32(123, 63, 0, 255), new Color32(80, 40, 0, 255)),
        new("Cramoisi", new Color32(220, 20, 60, 255), new Color32(150, 10, 40, 255)),
        new("Tomate", new Color32(255, 99, 71, 255), new Color32(190, 60, 45, 255)),
        new("Violet", new Color32(148, 0, 211, 255), new Color32(95, 0, 140, 255)),
        new("Bleu nuit", new Color32(25, 25, 112, 255), new Color32(12, 12, 70, 255)),
        new("Bleu ciel", new Color32(30, 144, 255, 255), new Color32(20, 95, 180, 255)),
        new("Blurple", new Color32(88, 101, 242, 255), new Color32(55, 64, 170, 255)),
        new("Vert acide", new Color32(176, 191, 26, 255), new Color32(120, 130, 15, 255)),
        new("Neige", new Color32(240, 248, 255, 255), new Color32(180, 200, 220, 255)),

        // Animée : sa teinte change en continu (voir RainbowUpdater)
        new("Arc-en-ciel", new Color32(255, 0, 0, 255), new Color32(160, 0, 0, 255)),
    };

    public static void Load()
    {
        VanillaCount = Palette.PlayerColors.Length;

        var names = Palette.ColorNames.ToList();
        var main = Palette.PlayerColors.ToList();
        var shadow = Palette.ShadowColors.ToList();

        foreach (var color in Colors)
        {
            names.Add(CustomStringName.CreateAndRegister(color.Name));
            main.Add(color.Main);
            shadow.Add(color.Shadow);
        }

        Palette.ColorNames = names.ToArray();
        Palette.PlayerColors = main.ToArray();
        Palette.ShadowColors = shadow.ToArray();

        RainbowId = VanillaCount + Colors.Count - 1;
    }

    public static int Total => Palette.PlayerColors.Length;
}
