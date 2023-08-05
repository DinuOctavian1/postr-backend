using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
    
            public static Error UserAllreadyExists = Error.Conflict(code: "User_exists",
                                                               description: "User allready exists"); 
        }
    }
}
