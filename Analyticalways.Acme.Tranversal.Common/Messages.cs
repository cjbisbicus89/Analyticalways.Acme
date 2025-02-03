namespace Analyticalways.Acme.Tranversal.Common
{
    public static class Messages
    {
        public static string MessageAgeValidation
        { get { return "Dear user, you are not of legal age to continue the process."; } }
        public static string MessageProcessNotCompleted
        { get { return "The process could not be completed."; } }
        public static string MessageProcessCompletedSuccessfully
        { get { return "Process completed successfully"; } }
        public static string MessageValidationNullOrEmptyField
        { get { return "Course name cannot be null or empty."; } }
        public static string MessageRegistrationFee
        { get { return "Enrollment fee must be greater than zero."; } }
        public static string MessageValidationOfDates
        { get { return "End date must be after the start date."; } }
        public static string MessageConnectionPaymentGatewayFailed
        { get { return "It was not possible to establish a connection to the payment gateway."; } }
        public static string MessageCoursesNotFoundInTheDateRange
        { get { return "No courses found within the specified date range."; } }

    }
}
