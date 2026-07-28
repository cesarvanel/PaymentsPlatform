using Billing.Core.Application.Ports.Out;
using Shared.Application.Cqrs.interfaces;
using Shared.Application.Messaging;
using Shared.Domain.Exceptions;
using Shared.Domain.Vo;

namespace Billing.Core.Application.Commands.MakePayment;

public sealed class MakePaymentHandler(
    IInvoiceRepository invoiceRepository,
    IPaymentRepository paymentRepository) : ICommandHandler<MakePaymentCommand, Guid>
{
    public async Task<Result<Guid>> HandleAsync(MakePaymentCommand command, CancellationToken ct = default)
    {
        try
        {
            var invoice = await invoiceRepository.GetByIdAsync(command.InvoiceId, ct);
            if (invoice is null)
            {
                return Result<Guid>.Failure($"Cette facture n'existe pas : {command.InvoiceId}");
            }

            var payment = invoice.AddPayment(new Money(command.Amount, command.Currency), command.PaymentMethod);

            await paymentRepository.SaveAsync(payment, ct);

            await invoiceRepository.UpdateAsync(invoice, ct);

            return Result<Guid>.Success(payment.Id);
        }
        catch (DomainException ex)
        {
            return Result<Guid>.Failure(ex.Message);
        }
    }
}