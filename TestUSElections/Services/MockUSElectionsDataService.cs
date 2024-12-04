using System.Reactive.Subjects;
using USElections.Models.USElectionsData;

namespace USElections.USElectionsData
{
    public class MockUSElectionsDataService : IUSElectionsDataService
    {
        public BehaviorSubject<VoteCountResult> ElectoralVotesDemocrat { get; } = new(null);
        public BehaviorSubject<VoteCountResult> PopularVotesRepublican { get; } = new(null);
        public BehaviorSubject<VoteCountResult> PopularVotesDemocrat { get; } = new(null);
        public BehaviorSubject<Candidate> DemocratCandidate { get; } = new(null);
        public BehaviorSubject<VoteCountResult> ElectoralVotesRepublican { get; } = new(null);
        public BehaviorSubject<Candidate> RepublicanCandidate { get; } = new(null);

        public Task<List<YearModel>> GetYearModelList()
        {
            return Task.FromResult<List<YearModel>>(new());
        }

        public Task<List<VoteResult>> GetVoteResultList(int? year)
        {
            return Task.FromResult<List<VoteResult>>(new());
        }

        public Task<List<VoteResult>> GetVoteResultList1(int? year)
        {
            return Task.FromResult<List<VoteResult>>(new());
        }

        public Task<List<CandidateVoteResult>> GetCandidateVoteResultList(int? year)
        {
            return Task.FromResult<List<CandidateVoteResult>>(new());
        }

        public Task<List<StateVoteResult>> GetStateVoteResultList(int? year)
        {
            return Task.FromResult<List<StateVoteResult>>(new());
        }

        public Task<VoteCountResult> GetVoteCountResult(int? year, string? party)
        {
            return Task.FromResult<VoteCountResult>(new());
        }

        public Task<VoteCountResult> GetVoteCountResult1(int? year, string? party)
        {
            return Task.FromResult<VoteCountResult>(new());
        }

        public Task<Candidate> GetCandidate(int? year)
        {
            return Task.FromResult<Candidate>(new());
        }

        public Task<Candidate> GetCandidate1(int? year)
        {
            return Task.FromResult<Candidate>(new());
        }
    }
}
