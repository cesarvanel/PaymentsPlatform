
namespace Billing.Core.Application.Commands.CancelPayment
{
    public record CancelPaymentResponse
    {
        public bool IsCancelled = false; 
        public string Message = string.Empty;
    }
}
