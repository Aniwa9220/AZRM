namespace AZRM2023v1.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string ControllerName { get; set; } = string.Empty;
        public string ActionName { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}