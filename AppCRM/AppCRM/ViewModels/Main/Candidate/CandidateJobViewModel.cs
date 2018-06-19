using AppCRM.Services.Candidate;
using AppCRM.Services.Navigation;
using AppCRM.ViewModels.Base;

namespace AppCRM.ViewModels.Main.Candidate
{
    public class CandidateJobViewModel : ViewModelBase
    {
        private readonly ICandidateJobService _candidateJobService;
        private readonly INavigationService _navigationService;
        //private CandidateProfile _profile;

        // height listview 
        private int _contactLinksListViewHeightRequest;
        private int _educationListViewHeightRequest;
        private int _workExperienceListViewHeightRequest;
        private int _skillListViewHeightRequest;
        private int _qualificationListViewHeightRequest;
        private int _licenceListViewHeightRequest;
        private int _documentListViewHeightRequest;
        private int _referenceListViewHeightRequest;
        //private string _avatarUrl;
        public CandidateJobViewModel(ICandidateJobService candidateJobService, INavigationService navigationService)
        {
            _candidateJobService = candidateJobService;
            _navigationService = navigationService;
        }
    }
}
