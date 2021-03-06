﻿using System.Collections.Generic;

namespace Trains.NET.Engine
{
    public static class DictionaryExtensions
    {
        public static void Deconstruct(this KeyValuePair<(int, int), IStaticEntity> keyValuePair, out int col, out int row, out IStaticEntity track)
        {
            col = keyValuePair.Key.Item1;
            row = keyValuePair.Key.Item2;
            track = keyValuePair.Value;
        }
    }
}
