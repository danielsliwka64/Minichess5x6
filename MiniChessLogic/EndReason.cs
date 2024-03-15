using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChessLogic
{
    public enum EndReason
    {
        Chekmate,
        Stalemate,
        FiftyMoveRule,
        InsufficientMaterial,
        ThreefoldRepetition
    }
}
