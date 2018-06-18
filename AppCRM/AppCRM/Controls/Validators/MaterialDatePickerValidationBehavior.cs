using AppCRM.Controls.TemplateMaterial;
using AppCRM.Validations;
using Xamarin.Forms;

namespace AppCRM.Controls.Validators
{
    class MaterialDatePickerValidationBehavior : Behavior<MaterialDatePicker>
    {
        public string RuleNames { get; set; }
        public string RuleMessages { get; set; }
        public string ValidMessage { get; set; } = string.Empty;
        public Color ValidMessageColor { get; set; } = Color.FromHex("#52CD9F");
        public string ValidationLabelName { get; set; }

        /// <summary>
        /// Attach events on attachment to view
        /// </summary>
        /// <param name="bindable">Bindable.</param>
        protected override void OnAttachedTo(MaterialDatePicker bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.EntryUnfocused += Bindable_EntryUnfocused;
        }

        /// <summary>
        /// Detach events on detaching from view
        /// </summary>
        /// <param name="bindable">Bindable.</param>
        protected override void OnDetachingFrom(MaterialDatePicker bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.EntryUnfocused -= Bindable_EntryUnfocused;
        }

        /// <summary>
        /// Set invalid on unfocus if the min is not met
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void Bindable_EntryUnfocused(object sender, FocusEventArgs e)
        {
            var entry = (MaterialDatePicker)sender;
            var validationLabel = entry.Parent.FindByName<Label>(ValidationLabelName);

            var message = this.ValidMessage;
            var color = this.ValidMessageColor;

            var rules = this.RuleNames.Split(',');
            var ruleMessages = this.RuleMessages.Split(',');
            var i = 0;
            foreach (var rule in rules)
            {
                switch (rule.Trim())
                {
                    case ValidationRule.Required:
                        entry.IsValid = !Validator.IsEmpty(entry.Date.ToString());
                        break;
                    default: break;
                }
                if (!entry.IsValid)
                {
                    message = ruleMessages[i].Trim();
                    color = entry.InvalidColor;
                    break;
                }
                i++;
            }
            if (validationLabel != null)
            {
                validationLabel.Text = message.Trim();
                validationLabel.TextColor = color;
            }
        }
    }
}
