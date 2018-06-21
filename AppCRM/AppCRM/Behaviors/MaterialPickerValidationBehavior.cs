using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using AppCRM.Controls.TemplateMaterial;
using AppCRM.Validations;
using AppCRM.Models;

namespace AppCRM.Behaviors
{
    class MaterialPickerValidationBehavior : Behavior<MaterialPicker>
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
        protected override void OnAttachedTo(MaterialPicker bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.EntryUnfocused += Bindable_EntryUnfocused;
        }

        /// <summary>
        /// Detach events on detaching from view
        /// </summary>
        /// <param name="bindable">Bindable.</param>
        protected override void OnDetachingFrom(MaterialPicker bindable)
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
            var entry = (MaterialPicker)sender;
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
                        var pickerItem = entry.SelectedItem as PickerItem;
                        entry.IsValid = pickerItem != null && !Validator.IsEmpty(pickerItem.ID.ToString());
                        break;
                    //add more rule if needed
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
