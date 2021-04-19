// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.UI;
using osu.Game.Rulesets.UI;
using osuTK;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModBarrelRoll : Mod, IUpdatableByPlayfield, IApplicableToDrawableRuleset<OsuHitObject>
    {
        [SettingSource("Roll speed", "Rotations per minute")]
        public BindableNumber<double> SpinSpeed { get; } = new BindableDouble(0.5)
        {
            MinValue = 0.02,
            MaxValue = 4,
            Precision = 0.01,
        };

        [SettingSource("Direction", "The direction of rotation")]
        public Bindable<RotationDirection> Direction { get; } = new Bindable<RotationDirection>(RotationDirection.Clockwise);

        public override string Name => "Barrel Roll";
        public override string Acronym => "BR";
        public override string Description => "The whole playfield is on a wheel!";
        public override double ScoreMultiplier => 1;

        public override string SettingDescription => $"{SpinSpeed.Value} rpm {Direction.Value.GetDescription().ToLowerInvariant()}";

        public void Update(Playfield playfield)
        {
            playfield.Rotation = (Direction.Value == RotationDirection.Counterclockwise ? -1 : 1) * 360 * (float)(playfield.Time.Current / 60000 * SpinSpeed.Value);
        }

        public void ApplyToDrawableRuleset(DrawableRuleset<OsuHitObject> drawableRuleset)
        {
            // scale the playfield to allow all hitobjects to stay within the visible region.
            drawableRuleset.Playfield.Scale = new Vector2(OsuPlayfield.BASE_SIZE.Y / OsuPlayfield.BASE_SIZE.X);
        }
    }
}
