// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Scoring;
using osuTK;

namespace osu.Game.Rulesets.Judgements
{
    public class DefaultJudgementPiece : FillFlowContainer, IAnimatableJudgement
    {
        protected readonly JudgementResult Judge;
        protected readonly HitResult Result;

        protected SpriteText JudgementText { get; private set; }
        protected SpriteText FastSlowText { get; private set; }

        [Resolved]
        private OsuColour colours { get; set; }

        public DefaultJudgementPiece(JudgementResult result)
        {
            Judge = result;
            Result = result.Type;
            Origin = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            AutoSizeAxes = Axes.Both;
            Direction = FillDirection.Vertical;
            Spacing = new Vector2(5, 0);

            InternalChildren = new Drawable[]
            {
                FastSlowText = new OsuSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Text = Judge.FastSlow.ToString().ToUpperInvariant(),
                    /* Text = Judge.FastSlow.GetDescription().ToUpperInvariant(), */
                    Colour = colours.ForFastSlow(Judge.FastSlow),
                    Font = OsuFont.Numeric.With(size: 18),
                    Scale = new Vector2(0.85f, 1),
                },
                JudgementText = new OsuSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Text = Result.GetDescription().ToUpperInvariant(),
                    Colour = colours.ForHitResult(Result),
                    Font = OsuFont.Numeric.With(size: 20),
                    Scale = new Vector2(0.85f, 1),
                }
            };
        }

        public virtual void PlayAnimation()
        {
            switch (Result)
            {
                case HitResult.Miss:
                    this.ScaleTo(1.6f);
                    this.ScaleTo(1, 100, Easing.In);

                    this.MoveTo(Vector2.Zero);
                    this.MoveToOffset(new Vector2(0, 100), 800, Easing.InQuint);

                    this.RotateTo(0);
                    this.RotateTo(40, 800, Easing.InQuint);

                    break;

                default:
                    this.ScaleTo(0.9f);
                    this.ScaleTo(1, 500, Easing.OutElastic);
                    break;
            }

            this.FadeOutFromOne(800);
        }

        public Drawable GetAboveHitObjectsProxiedContent() => null;
    }
}
