﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace AppCRM.Models
{
    public class Contact : INotifyPropertyChanged
    {
        public string UserID { get; set; }
        private string _coverUrl;
        public string CoverUrl
        {
            get { return _coverUrl; }
            set
            {
                _coverUrl = value;
                OnPropertyChanged("CoverUrl");
            }
        }
        private string _avatarUrl;
        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set
            {
                _avatarUrl = value;
                OnPropertyChanged("AvatarUrl");
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public Guid? CityID { get; set; }
        public string Nationality { get; set; }
        public Guid? NationalityID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string AboutMe { get; set; }
        public string RoleAndAddress { get; set; }
        public string Greeting { get; set; }
        public string Introduction { get; set; }
        public string Password { get; set; }

        public List<ContactLink> ContactLinks { get; set; }
        public List<ContactEducation> Educations { get; set; }
        public List<ContactWorkExprience> WorkExpriences { get; set; }
        public List<ContactSkill> Skills { get; set; }
        public List<ContactQualification> Qualifications { get; set; }
        public List<ContactLicence> Licences { get; set; }
        public List<ContactReference> References { get; set; }
        public List<Document> Documents { get; set; }
        public ObservableCollection<PickerItem> NationalityDDL { get; set; }
        public PickerItem SelectedNationality { get; set; }
        public ObservableCollection<PickerItem> CityDDL { get; set; }
        public PickerItem SelectedCity { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
