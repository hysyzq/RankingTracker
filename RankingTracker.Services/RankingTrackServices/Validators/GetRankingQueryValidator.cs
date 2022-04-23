using FluentValidation;
using RankingTracker.Services.RankingTrackServices.Queries;

namespace RankingTracker.Services.RankingTrackServices.Validators
{
    public  class GetRankingQueryValidator : AbstractValidator<GetRankingQuery>
    {
        public GetRankingQueryValidator()
        {
            RuleFor(x=>x.SearchText).NotEmpty();
            RuleFor(x=>x.SearchUrl).NotEmpty();
        }
    }
}
