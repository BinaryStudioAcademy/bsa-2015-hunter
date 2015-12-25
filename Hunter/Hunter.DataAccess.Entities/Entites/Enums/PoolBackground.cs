using System;
using System.Collections.Generic;

namespace Hunter.DataAccess.Entities.Enums
{
    public sealed class PoolBackground
    {
        public string color { get; private set; }
        public string code { get; private set; }

        public static readonly List<PoolBackground> BgColors = new List<PoolBackground>
        {
            new PoolBackground{color = "Green", code = "#009688"},
            new PoolBackground{color = "Yellow", code = "#FFEB3B"},
            new PoolBackground{color = "Red", code = "#E91E63"},
            new PoolBackground{color = "Blue", code = "#03A9F4"},
            new PoolBackground{color = "Aquamarine", code = "rgb(76,222,181)"},
            new PoolBackground{color = "Purple", code = "rgb(181,60,181)"},
            new PoolBackground{color = "Blue", code = "rgb(87,166,235)"}
        };
    }
}
