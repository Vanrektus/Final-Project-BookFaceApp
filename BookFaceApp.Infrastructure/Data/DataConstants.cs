namespace BookFaceApp.Infrastructure.Data
{
    public class DataConstants
    {
        public class UserConstants
        {
            public const int MinUserFirstName = 2;
            public const int MaxUserFirstName = 30;

            public const int MinUserLastName = 2;
            public const int MaxUserLastName = 320;

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

        public class CategoryConstants
        {
            public const int MinCategoryName = 2;
            public const int MaxCategoryName = 50;
        }

        public class GroupConstants
        {
            public const int MinGroupName = 2;
            public const int MaxGroupName = 50;
        }

        public class RequestConstants
        {
            public const int MinRequestName = 1;
            public const int MaxRequestName = 20;

            public const int MinRequestStatus = 7;
            public const int MaxRequestStatus = 8;
        }

        public class RoleConstants
        {
            public const int MinRoleName = 1;
            public const int MaxRoleName = 50;
        }
    }
}
