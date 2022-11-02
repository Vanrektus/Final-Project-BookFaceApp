namespace BookFaceApp.Infrastructure.Data
{
    public class DataConstants
    {
        public class UserConstants
        {
            public const int MinUserUserName = 5;
            public const int MaxUserUserName = 20;

            public const int MinUserEmail = 10;
            public const int MaxUserEmail = 60;

            public const int MinUserPassword = 5;
            public const int MaxUserPassword = 20;
        }

        public class PublicationConstants
        {
            public const int MinPublicationTitle = 1;
            public const int MaxPublicationTitle = 100;
        }

        public class CommentConstants
        {
            public const int MinCommentText = 1;
            public const int MaxCommentText = 500;
        }
    }
}
