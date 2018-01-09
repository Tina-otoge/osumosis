﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Online.Chat;
using System;
using System.Diagnostics;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Game.Overlays;

namespace osu.Game.Graphics.Containers
{
    public class LinkFlowContainer : OsuTextFlowContainer
    {
        public LinkFlowContainer(Action<SpriteText> defaultCreationParameters = null)
            : base(defaultCreationParameters)
        {
        }

        public override bool HandleInput => true;

        private BeatmapSetOverlay beatmapSetOverlay;
        private ChatOverlay chat;
        private OsuGame game;

        [BackgroundDependencyLoader(true)]
        private void load(BeatmapSetOverlay beatmapSetOverlay, ChatOverlay chat, OsuGame game)
        {
            this.beatmapSetOverlay = beatmapSetOverlay;
            this.chat = chat;
            // this will be null in tests
            this.game = game;
        }

        public void AddLink(string text, string url, LinkAction linkType = LinkAction.External, string linkArgument = null, string tooltipText = null)
        {
            AddInternal(new DrawableLinkCompiler(AddText(text).ToList())
            {
                TooltipText = tooltipText ?? (url != text ? url : string.Empty),
                Action = () =>
                {
                    switch (linkType)
                    {
                        case LinkAction.OpenBeatmap:
                            // todo: implement this when overlay.ShowBeatmap(id) exists
                            break;
                        case LinkAction.OpenBeatmapSet:
                            if (int.TryParse(linkArgument, out int setId))
                                beatmapSetOverlay.ShowBeatmapSet(setId);
                            break;
                        case LinkAction.OpenChannel:
                            chat.OpenChannel(chat.AvailableChannels.Find(c => c.Name == linkArgument));
                            break;
                        case LinkAction.OpenEditorTimestamp:
                            game?.LoadEditorTimestamp();
                            break;
                        case LinkAction.JoinMultiplayerMatch:
                            if (int.TryParse(linkArgument, out int matchId))
                                game?.JoinMultiplayerMatch(matchId);
                            break;
                        case LinkAction.Spectate:
                            // todo: implement this when spectating exists
                            break;
                        case LinkAction.External:
                            Process.Start(url);
                            break;
                        default:
                            throw new NotImplementedException($"This {nameof(LinkAction)} ({linkType.ToString()}) is missing an associated action.");
                    }
                }
            });
        }
    }
}
