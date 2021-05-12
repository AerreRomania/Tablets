using System.Threading.Tasks;
using Acr.UserDialogs;
using SmartB.Core.Contracts.Services.General;
namespace SmartB.Core.Services.General
{
    public class DialogService : IDialogService
    {
        public Task<PromptResult> ShowPromptDialog(string message, string title,
                                     string confirmationButton, string cancelButton,
                                     string pin)
        {
            var config = new PromptConfig()
            {
                Message = message,
                Title = title,
                OkText = confirmationButton,
                CancelText = cancelButton,
                Placeholder = pin,
                InputType= InputType.NumericPassword
            };
            return UserDialogs.Instance.PromptAsync(config);
        }
        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }
        public Task<bool> ShowConfirmationDialog(string title, string message, string okButton, string cancelButton)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, okButton, cancelButton);
        }
        public IProgressDialog ShowProgressDialog(string message)
        {
            return UserDialogs.Instance.Loading(message);
        }
        public void ShowToast(string message)
        {
            UserDialogs.Instance.Toast(message);
        }
    }
}
