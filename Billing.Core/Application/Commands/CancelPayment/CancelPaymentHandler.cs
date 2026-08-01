using Billing.Core.Application.Ports.Out;
using Billing.Core.Domain.Exceptions;
using Shared.Application.Cqrs.interfaces;
using Shared.Application.Messaging;
using Shared.Domain.Exceptions;

namespace Billing.Core.Application.Commands.CancelPayment
{
    public sealed class CancelPaymentHandler(IInvoiceRepository invoiceRepository) : ICommandHandler<CancelPaymentCommand, CancelPaymentResponse>
    {
        public async Task<Result<CancelPaymentResponse>> HandleAsync(CancelPaymentCommand command, CancellationToken ct = default)
        {
            var response = new CancelPaymentResponse();
            try
            {
                var invoice = await invoiceRepository.GetByIdAsync(command.InvoiceId, ct) ?? throw new InvoiceNotFoundException($"cette commande n'existe pas");

                invoice.CancelPayment(command.PaymentId);

                await invoiceRepository.UpdateAsync(invoice, ct);
             
            }
            catch (DomainException ex)
            {
               return Result<CancelPaymentResponse>.Failure(ex.Message);
            }

            response.IsCancelled = true;
            response.Message = "Votre paiement à été annuler avec succèss";
            return Result<CancelPaymentResponse>.Success(response);






        }
    }
}
