﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Configuration;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Skinning;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Judgements
{
    /// <summary>
    /// A drawable object which visualises the hit result of a <see cref="Judgements.Judgement"/>.
    /// </summary>
    public class DrawableJudgement : CompositeDrawable
    {
        private const float judgement_size = 128;

        [Resolved]
        private OsuColour colours { get; set; }

        protected readonly JudgementResult Result;
        protected readonly HitDetail Detail;

        public readonly DrawableHitObject JudgedObject;

        protected Container JudgementBody;
        protected SpriteText JudgementText;
        protected SpriteText JudgementDetailText;

        /// <summary>
        /// Duration of initial fade in.
        /// </summary>
        protected virtual double FadeInDuration => 100;

        /// <summary>
        /// Duration to wait until fade out begins. Defaults to <see cref="FadeInDuration"/>.
        /// </summary>
        protected virtual double FadeOutDelay => FadeInDuration;

        /// <summary>
        /// Creates a drawable which visualises a <see cref="Judgements.Judgement"/>.
        /// </summary>
        /// <param name="result">The judgement to visualise.</param>
        /// <param name="judgedObject">The object which was judged.</param>
        public DrawableJudgement(JudgementResult result, DrawableHitObject judgedObject)
        {
            Result = result;
            Detail = result.GetDetail();
            JudgedObject = judgedObject;

            Size = new Vector2(judgement_size);
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            if (config.Get<bool>(OsuSetting.ShowJudgementDetail)) {
                InternalChild = JudgementBody = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Children =  new Drawable []
                    {
                        new SkinnableDrawable(new GameplaySkinComponent<HitResult>(Result.Type), _ => JudgementText = new OsuSpriteText
                        {
                            Text = Result.Type.GetDescription().ToUpperInvariant(),
                            Font = OsuFont.Numeric.With(size: 20),
                            Colour = judgementColour(Result.Type),
                            Scale = new Vector2(0.85f, 1),
                        }, confineMode: ConfineMode.NoScaling),
                        new SkinnableDrawable(new GameplaySkinComponent<HitDetail>(Detail), _ => JudgementDetailText = new OsuSpriteText
                        {
                            Text = detailText(Result),
                            Font = OsuFont.Numeric.With(size: 15),
                            Y = -20,
                            Colour = detailColour(Detail),
                            Scale = new Vector2(0.7f, 1),
                        }, confineMode: ConfineMode.NoScaling)
                    }
                };
            } else {
                InternalChild = JudgementBody = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Children =  new Drawable []
                    {
                        new SkinnableDrawable(new GameplaySkinComponent<HitResult>(Result.Type), _ => JudgementText = new OsuSpriteText
                        {
                            Text = Result.Type.GetDescription().ToUpperInvariant(),
                            Font = OsuFont.Numeric.With(size: 20),
                            Colour = judgementColour(Result.Type),
                            Scale = new Vector2(0.85f, 1),
                        }, confineMode: ConfineMode.NoScaling),
                    }
                };
            }
        }

        protected virtual void ApplyHitAnimations()
        {
            JudgementBody.ScaleTo(0.9f);
            JudgementBody.ScaleTo(1, 500, Easing.OutElastic);

            this.Delay(FadeOutDelay).FadeOut(400);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            this.FadeInFromZero(FadeInDuration, Easing.OutQuint);

            switch (Result.Type)
            {
                case HitResult.None:
                    break;

                case HitResult.Miss:
                    JudgementBody.ScaleTo(1.6f);
                    JudgementBody.ScaleTo(1, 100, Easing.In);

                    JudgementBody.MoveToOffset(new Vector2(0, 100), 800, Easing.InQuint);
                    JudgementBody.RotateTo(40, 800, Easing.InQuint);

                    this.Delay(600).FadeOut(200);
                    break;

                default:
                    ApplyHitAnimations();
                    break;
            }

            Expire(true);
        }

        private Color4 judgementColour(HitResult judgement)
        {
            switch (judgement)
            {
                case HitResult.Perfect:
                case HitResult.Great:
                    return colours.Blue;

                case HitResult.Ok:
                case HitResult.Good:
                    return colours.Green;

                case HitResult.Meh:
                    return colours.Yellow;

                case HitResult.Miss:
                    return colours.Red;

                default:
                    return Color4.White;
            }
        }

        private Color4 detailColour(HitDetail detail)
        {
            switch (detail)
            {
                case HitDetail.Fast:
                    return colours.Cyan;

                case HitDetail.Slow:
                    return colours.RedLight;

                default:
                    return Color4.White;
            }
        }

        private string detailText(JudgementResult result)
        {
            if (result.TimeOffset == 0)
                return "";
            switch (result.Type)
            {
                case HitResult.Perfect:
                case HitResult.Great:
                case HitResult.Miss:
                    return "";
                default:
                    return result.GetDetail().ToString().ToUpperInvariant();
            }
        }
    }
}
