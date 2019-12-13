// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Bindables;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Graphics;

namespace osu.Game.Rulesets.Mods
{
    public abstract class ModApproachRate : Mod, IApplicableToDifficulty
    {
        public override string Name => "Approach Rate";
        public override string Acronym => "AR";
        public override IconUsage Icon => FontAwesome.Regular.Circle;
        public override ModType Type => ModType.Conversion;
        public override string Description => "Your game, your rules";

        [SettingSource("Approach Rate", "You know what this is already")]
        public BindableNumber<float> approachRate { get; } = new BindableFloat
        {
            MinValue = 1F,
            MaxValue = 13F,
            Default = 9F,
            Value = 9F,
            Precision = 0.1F,
        };

        public void ApplyToDifficulty(BeatmapDifficulty difficulty)
        {
            difficulty.ApproachRate = approachRate.Value;
        }
    }
}
