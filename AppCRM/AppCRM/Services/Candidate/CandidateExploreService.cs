using AppCRM.Models;
using AppCRM.Services.Request;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace AppCRM.Services.Candidate
{
    public interface ICandidateExploreService
    {
        Task<dynamic> GetCandidateJobsSearch(SearchParameters parameters);
        Task<dynamic> SaveSearchDefinition(SearchParameters parameters);
        Task<dynamic> GetSavedSearchDefinition();
    }

    class CandidateExploreService : ICandidateExploreService
    {
        private readonly IRequestService _requestService;

        public CandidateExploreService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<dynamic> GetCandidateJobsSearch(SearchParameters parameters)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateJob/GetCandidateJobsSearch", parameters);
            return result;
        }
        public async Task<dynamic> SaveSearchDefinition(SearchParameters parameters)
        {
            var result = await _requestService.postDataFromServiceAuthority("api/CandidateJob/SaveSearchDefinition", parameters);
            return result;
        }

        public async Task<dynamic> GetSavedSearchDefinition()
        {
            var result = await _requestService.getDataFromServiceAuthority("api/CandidateJob/GetSavedSearchDefinition");
            return result;
        }
    }
}
