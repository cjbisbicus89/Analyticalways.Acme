using Analyticalways.Acme.Tranversal.Interfaces;

namespace Analyticalways.Acme.Tranversal.Common
{
    public class PaymentGateway:IPaymentGateway
    {
        #region Builder
        public PaymentGateway()
        {
        }
        #endregion

        #region Public methods
        public async Task<dynamic> ProcessPayment(decimal amount)
        {
            var response = new Response<dynamic>
            {
                Error = false,  // Assuming error is false by default
                Result = null    // Assuming result is null by default when amount is valid
            };

            if (amount < 0)
            {
                response.Success = false;
                response.Message = Messages.MessageConnectionPaymentGatewayFailed; // Set error message if amount is invalid
            }
            else
            {
                response.Success = true;
                response.Message = Messages.MessageProcessCompletedSuccessfully;
            }

            return response;

        }

        #endregion
    }
}
