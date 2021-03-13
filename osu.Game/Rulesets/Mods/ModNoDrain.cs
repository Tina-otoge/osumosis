// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics.Sprites;
using osu.Game.Beatmaps;

namespace osu.Game.Rulesets.Mods
{
    public abstract class ModNoDrain : Mod, IApplicableToDifficulty
    {
        public override string Name => "No drain";
        public override string Acronym => "ND";
        public override string Description => "Removes the passive HP drain, because punishing slow parts is objectively unfair.";
        public override double ScoreMultiplier => 1;
        public override IconUsage? Icon => FontAwesome.Solid.BandAid;
        public override ModType Type => ModType.DifficultyReduction;
        public override bool Ranked => true;

        public virtual void ReadFromDifficulty(BeatmapDifficulty difficulty)
        {
        }

        public virtual void ApplyToDifficulty(BeatmapDifficulty difficulty)
        {
            difficulty.DrainRate = 0;
        }
    }
}
