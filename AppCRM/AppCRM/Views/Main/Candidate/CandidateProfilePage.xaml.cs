using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCRM.Views.Main.Candidate
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CandidateProfilePage : ContentView
    {
        public CandidateProfilePage()
        {
            InitializeComponent();
            EducationListView.PropertyChanged += ListView_PropertyChanged;
            WorkExperienceListView.PropertyChanged += ListView_PropertyChanged;
            SkillListView.PropertyChanged += ListView_PropertyChanged;
            QualificationListView.PropertyChanged += ListView_PropertyChanged;
            LicenseListView.PropertyChanged += ListView_PropertyChanged;
            DocumentListView.PropertyChanged += ListView_PropertyChanged;
            ReferenceListView.PropertyChanged += ListView_PropertyChanged;
        }

        private void ListView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            (sender as SfListView).RefreshView();
            (sender as SfListView).ForceLayout();
        }
    }
}