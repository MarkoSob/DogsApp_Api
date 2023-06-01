namespace DogsApp_Core
{
    public static class AppConstants
    {
        public const string AppInfo = "Dogs house service. Version";
        public const string SortDescending = "desc";

        public static class ErrorMessages
        {
            public const string TailLengthErrorMessage = "The tail length must be a positive, numeric value (between 1 - 999)";
            public const string WeighthErrorMessage = "The weight must be a positive, numeric value (between 1 - 999)";
            public const string FieldLengthErrorMessage = "Field must contain at least 3 characters";
            public const string WrongModelErrorMessage = "You entered wrong model";
            public const string LoggerWrongModelErrorMessage = "User entered wrong model";
            public const string UnhandledExceptionErrorMessage = "Unhandled exception happened";
        }
    }
}
