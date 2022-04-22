using Contract;
using System;

namespace RankingTracker.WPF.Mappers
{
    public static class RankingTrackResponseMapper
    {
        public static string ToText( this RankingTrackResponse response)
        {
            return String.Join(",", response.positions);
        }
    }
}
