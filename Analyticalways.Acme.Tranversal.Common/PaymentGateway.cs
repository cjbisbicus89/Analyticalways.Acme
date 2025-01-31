using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                error = false,  // Assuming error is false by default
                result = null    // Assuming result is null by default when amount is valid
            };

            if (amount < 0)
            {
                response.success = false;
                response.message = Messages.Msg007; // Set error message if amount is invalid
            }
            else
            {
                response.success = true;
                response.message = Messages.Msg003;
            }

            return response;

        }

        #endregion
    }
}
