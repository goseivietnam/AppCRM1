using System.Collections.Generic;
using AppCRM.Views.Account;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCRM.Views
{
	[XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class RegisterPopupPage : PopupPage
    {
		public RegisterPopupPage ()
		{
			InitializeComponent ();
            var itemSource  = new List<View>();

            Frame candidate = new Frame();
            candidate.BackgroundColor = Color.FromHex("#FAFAFA");
            StackLayout candidateSL = new StackLayout { Spacing = 5 };
            candidateSL.Children.Add(new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 12,
                TextColor = Color.FromHex("#7F8890"),
                Text = "Hi, please select your role to join with us"
            });
            candidateSL.Children.Add(new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile("candidate_vectorized.png"),
                Margin = new Thickness(0, 25)
            });
            candidateSL.Children.Add(new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 14,
                TextColor = Color.FromHex("#231F20"),
                Text = "I'm looking for work"
            });
            candidateSL.Children.Add(new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 12,
                TextColor = Color.FromHex("#7F8890"),
                Text = "Candidate"
            });
            Button btnCandidate = new Button { HorizontalOptions = LayoutOptions.Fill, BackgroundColor = Color.FromHex("#2A76FD"),
                BorderColor = Color.FromHex("#266CE9"),
                BorderWidth = 1,
                TextColor = Color.WhiteSmoke,
                Text = "Register",
                Margin = new Thickness(0, 20)
            };
            btnCandidate.SetBinding (Button.CommandProperty, new Binding ("CandidateRegisterCommand"));
            candidateSL.Children.Add(btnCandidate);
            candidate.Content = candidateSL;
            itemSource.Add(candidate);

            Frame employer = new Frame();
            employer.BackgroundColor = Color.FromHex("#FAFAFA");
            StackLayout employerSL = new StackLayout();
            employerSL.Children.Add(new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 12,
                TextColor = Color.FromHex("#7F8890"),
                Text = "Hi, please select your role to join with us"
            });
            employerSL.Children.Add(new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile("employer_vectorized.png"),
                Margin = new Thickness(0, 25)
            });
            employerSL.Children.Add(new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 14,
                TextColor = Color.FromHex("#231F20"),
                Text = "I'm wanting to hire"
            });
            employerSL.Children.Add(new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 12,
                TextColor = Color.FromHex("#7F8890"),
                Text = "Employer"
            });
            Button btnEmployer = new Button
            {
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromHex("#2A76FD"),
                BorderColor = Color.FromHex("#266CE9"),
                BorderWidth = 1,
                TextColor = Color.WhiteSmoke,
                Text = "Register",
                Margin = new Thickness(0, 20)
            };
            btnEmployer.SetBinding(Button.CommandProperty, new Binding("EmployerRegisterCommand"));
            employerSL.Children.Add(btnEmployer);
            employer.Content = employerSL;
            itemSource.Add(employer);
            carousel.ItemsSource = itemSource;
		}
        protected override bool OnBackButtonPressed()
        {
            CloseAllPopup();
            return false;
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }
      
    }
}