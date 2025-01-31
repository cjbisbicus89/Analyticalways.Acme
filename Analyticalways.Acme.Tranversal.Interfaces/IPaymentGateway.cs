namespace Analyticalways.Acme.Tranversal.Interfaces
{
    public interface IPaymentGateway
    {
        Task<dynamic> ProcessPayment(decimal amount);
    }
}
