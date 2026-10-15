using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Reactor;
using Reactor.Utilities;

namespace RoyaleUs.Couleurs;

/// <summary>
/// Couleurs supplémentaires pour les joueurs dans le lobby.
/// Basé sur le mod Rainbow de MoltenMods (https://github.com/MoltenMods/Rainbow, GPLv2),
/// mis à jour pour Among Us 2026.9.29. Tous les joueurs doivent avoir le mod.
/// </summary>
[BepInPlugin(Id, "Royale Us Couleurs", Version)]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
public class CouleursPlugin : BasePlugin
{
    public const string Id = "royaleus.couleurs";
    public const string Version = "1.0.0";

    public Harmony Harmony { get; } = new(Id);

    public override void Load()
    {
        CustomColors.Load();
        AddComponent<RainbowUpdater>();
        ReactorCredits.Register("Royale Us Couleurs", Version, false, ReactorCredits.AlwaysShow);
        Harmony.PatchAll();
        Log.LogInfo($"{CustomColors.Colors.Count} couleurs ajoutées.");
    }
}
