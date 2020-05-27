// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Osu.Scoring
{
    public class OsuHitWindows : HitWindows
    {
        private static readonly DifficultyRange[] osu_ranges =
        {
            new DifficultyRange(HitResult.Great, 80, 50, 20),
            new DifficultyRange(HitResult.Good, 140, 100, 60),
            new DifficultyRange(HitResult.Meh, 200, 150, 100),
            new DifficultyRange(HitResult.Miss, 400, 400, 400),
        };

        public override bool IsHitResultAllowed(HitResult result)
        {
            switch (result)
            {
                case HitResult.SliderTick:
                case HitResult.SliderEnd:
                case HitResult.Great:
                case HitResult.Good:
                case HitResult.Meh:
                case HitResult.Miss:
                    return true;
            }

            return false;
        }

        protected override DifficultyRange[] GetRanges() => osu_ranges;
    }

    public class OsuSliderTickHitWindows : OsuHitWindows
    {
        private static readonly DifficultyRange[] slidertick_ranges =
        {
            new DifficultyRange(HitResult.SliderTick, 0, 0, 0),
            new DifficultyRange(HitResult.Miss, 0, 0, 0),
        };

        public override bool IsHitResultAllowed(HitResult result)
        {
            switch (result)
            {
                case HitResult.SliderTick:
                case HitResult.Miss:
                    return true;
            }

            return false;
        }

        protected override DifficultyRange[] GetRanges() => slidertick_ranges;
    }

    public class OsuSliderEndHitWindows : OsuHitWindows
    {
        private static readonly DifficultyRange[] sliderend_ranges =
        {
            new DifficultyRange(HitResult.SliderEnd, 0, 0, 0),
            new DifficultyRange(HitResult.Miss, 0, 0, 0),
        };

        public override bool IsHitResultAllowed(HitResult result)
        {
            switch (result)
            {
                case HitResult.SliderEnd:
                case HitResult.Miss:
                    return true;
            }

            return false;
        }

        protected override DifficultyRange[] GetRanges() => sliderend_ranges;
    }
}
