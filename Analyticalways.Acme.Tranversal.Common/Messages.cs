namespace Analyticalways.Acme.Tranversal.Common
{
    public static class Messages
    {
        public static string Msg001
        { get { return "Dear user, you are not of legal age to continue the process. "; } }
        public static string Msg002
        { get { return "The process could not be completed. "; } }
        public static string Msg003
        { get { return "Process completed successfully"; } }
        public static string Msg004
        { get { return "Course name cannot be null or empty. "; } }
        public static string Msg005
        { get { return "Enrollment fee must be greater than zero. "; } }
        public static string Msg006
        { get { return "End date must be after the start date."; } }
        public static string Msg007
        { get { return "It was not possible to establish a connection to the payment gateway."; } }
        public static string Msg008
        { get { return "No courses found within the specified date range."; } }

    }
}
