﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trains.NET.Engine
{
    internal class AsciiArtTrackSerializer : ITrackSerializer
    {
        private static readonly Dictionary<TrackDirection, char> s_trackMapping = new()
        {
            { TrackDirection.Horizontal, '═' },
            { TrackDirection.Vertical, '║' },
            { TrackDirection.LeftUp, '╝' },
            { TrackDirection.RightUp, '╚' },
            { TrackDirection.RightDown, '╔' },
            { TrackDirection.LeftDown, '╗' },
            { TrackDirection.RightUpDown, '╠' },
            { TrackDirection.LeftRightDown, '╦' },
            { TrackDirection.LeftUpDown, '╣' },
            { TrackDirection.LeftRightUp, '╩' },
            { TrackDirection.Cross, '╬' },

            { TrackDirection.Undefined, '?' }
        };

        private static readonly Dictionary<TrackDirection, char> s_alternateTrackMappings = new()
        {
            { TrackDirection.RightUpDown, '├' },
            { TrackDirection.LeftRightDown, '┬' },
            { TrackDirection.LeftUpDown, '┤' },
            { TrackDirection.LeftRightUp, '┴' }
        };

        public IEnumerable<IStaticEntity> Deserialize(string[] lines)
        {
            var tracks = new List<IStaticEntity>();

            var happiness = lines[^1].ToCharArray();
            var index = 0;

            for (int r = 0; r < lines.Length - 1; r++)
            {
                for (int c = 0; c < lines[r].Length; c++)
                {
                    char current = lines[r][c];

                    bool alternate = false;
                    if (current == '*')
                    {
                        tracks.Add(new Tree()
                        {
                            Column = c,
                            Row = r
                        });
                    }
                    else
                    {
                        KeyValuePair<TrackDirection, char> pair = s_trackMapping.FirstOrDefault(kvp => kvp.Value == current);
                        if (pair.Value == '\0')
                        {
                            pair = s_alternateTrackMappings.FirstOrDefault(kvp => kvp.Value == current);
                            alternate = true;
                        }

                        if (pair.Value != default)
                        {
                            var track = new Track()
                            {
                                Column = c,
                                Row = r,
                                Direction = pair.Key,
                                AlternateState = alternate,
                                Happy = happiness[index++] == 'H'
                            };
                            tracks.Add(track);
                        }
                    }
                }
            }

            return tracks;
        }

        public string Serialize(IEnumerable<IStaticEntity> tracks)
        {
            if (!tracks.Any()) return string.Empty;

            var sb = new StringBuilder();

            var happinessSb = new StringBuilder();

            int maxColumn = tracks.Max(t => t.Column);
            int maxRow = tracks.Max(t => t.Row);

            for (int r = 0; r <= maxRow; r++)
            {
                for (int c = 0; c <= maxColumn; c++)
                {
                    IStaticEntity entity = tracks.FirstOrDefault(t => t.Column == c && t.Row == r);
                    if (entity == null)
                    {
                        sb.Append(' ');
                    }
                    else if (entity is Tree tree)
                    {
                        sb.Append('*');
                    }
                    else if (entity is Track track)
                    {
                        Dictionary<TrackDirection, char>? mapping = track.AlternateState ? s_alternateTrackMappings : s_trackMapping;
                        sb.Append(mapping[track.Direction]);
                        if (track.Happy)
                        {
                            happinessSb.Append('H');
                        }
                        else
                        {
                            happinessSb.Append(' ');
                        }
                    }
                }
                sb.AppendLine();
            }
            sb.AppendLine(happinessSb.ToString());

            return sb.ToString();
        }
    }
}
