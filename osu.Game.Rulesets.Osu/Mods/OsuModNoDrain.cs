// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Scoring;
using osu.Game.Scoring;
using osu.Framework.Graphics.Sprites;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModNoDrain : Mod, IApplicableToHealthProcessor
    {
        public override string Name => "No Drain";
        public override string Description => "Removes the passive HP drain.";
        public override string Acronym => "ND";

        public override IconUsage? Icon => FontAwesome.Solid.BandAid;
        public override ModType Type => ModType.DifficultyReduction;

        public override bool Ranked => true;

        public override double ScoreMultiplier => 1;

        public void ApplyToHealthProcessor(HealthProcessor processor)
        {
            DrainingHealthProcessor p = (DrainingHealthProcessor)processor;
            p.lockedRate = true;
        }

        public ScoreRank AdjustRank(ScoreRank rank, double accuracy) => rank;
    }
}
