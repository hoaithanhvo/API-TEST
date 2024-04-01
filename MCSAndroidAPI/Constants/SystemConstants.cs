namespace MCSAndroidAPI.Constants
{
    public class SystemConstants
    {
        public const string MainConnectionString = "MCSConnectionString";

        public struct Status
        {
            public const string SUCCESS = "success";
            public const string ERROR = "error";
        }

        public struct Message
        {
            public const string NOT_FOUND = "Not found";

            public const string MATERIAL_NOT_FOUND = "Material not found";

            public const string PRODUCT_NOT_FOUND = "Product not found";

            public const string MODEL_NOT_FOUND = "Model not found";

            public const string LOGIN_ERROR = "Username or Password is incorrect.";

            public const string USER_INACTIVE = "User is inactive.";

            public const string SERVER_ERROR = "Internal Server Error.";

            public const string CREATED = "Created successfully";

            public const string UPDATED = "Updated successfully";

            public const string DELETED = "Deleted successfully";

            public const string FIELD_IS_REQUIRED = "The {0} field is required.";

            public const string MAXLENGTH = "The {0} length cannot exceed {1} characters.";
        }

        public struct RankCode
        {
            public const string RANK_CD_20 = "20";
            public const string RANK_CD_30 = "30";
        }
    }
}
