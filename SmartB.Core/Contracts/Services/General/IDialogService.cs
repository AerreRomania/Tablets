using System.Threading.Tasks;
using Acr.UserDialogs;
namespace SmartB.Core.Contracts.Services.General
{
    public interface IDialogService
    {
        Task<PromptResult> ShowPromptDialog(string message, string title,
                                     string confirmationButton, string cancelButton,
                                     string pin);
        Task ShowDialog(string message, string title, string buttonLabel);
        Task<bool> ShowConfirmationDialog(string title, string message, string okButton, string cancelButton);
        IProgressDialog ShowProgressDialog(string message);
        void ShowToast(string message);
    }
}
