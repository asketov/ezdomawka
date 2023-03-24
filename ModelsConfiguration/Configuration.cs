namespace ModelsConfiguration
{
    public static class BanConfiguration
    {
        public const int MaxReasonTextLenght = 1500;
        public const int MinReasonTextLenght = 0;
        
        public const int MinDuration = 0;
        public const int MaxDuration = 10000000;
    }

    public static class SubjectConfiguration
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 100;
    }

    public static class ThemeConfiguration
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 160;
    }

    public static class MailConfiguration
    {
        public const int MaxLength = 150;
    }

    public static class ConfirmCodeConfiguration
    {
        public const int MaxValue = 999999;
        public const int MinValue = 100000;
    }

    public static class PasswordConfiguration
    {
        public const int MaxLength = 50;
        public const int MinLength = 5;
    }

    public static class NickConfiguration
    {
        public const int MaxLength = 50;
        public const int MinLength = 5;
    }

    public static class SolutionConfiguration
    {
        public const int TextMaxLength = 200;
        public const int TextMinLength = 1;

        public const int MaxPrice = 20000;
        public const int MinPrice = 0;

        public static class GetRequest
        {
            public const int MaxSkip = 20000;
            public const int MinSkip = 0;

            public const int MaxTake = 11;
            public const int MinTake = 9;
        }
    }

    public static class ReportConfiguration
    {
        public const int MaxReasonLength = 200;
    }

    public static class ConnectionConfiguration
    {
        public const int MaxConnectionLength = 100;
        public const int MinConnectionLength = 5;
    }
    
    public static class SuggestionConfiguration
    {
        public const int MaxTextLength = 500;
        public const int MinTextLength = 1;
    }

    public static class NotificationConfiguration
    {
        public const int MaxTextLength = 500;
        public const int MinTextLength = 1;
    }

    public static class ReviewConfiguration
    {
        public const int MaxTextLength = 500;
        public const int MinTextLength = 1;
    }

    public static class RoleConfiguration
    {
        public const int MaxNameLength = 100;
        public const int MinNameLength = 1;
    }
}