using System;

namespace EGID.Core.Common.ErrorHandler
{
    public static class ErrorHandler
    {
        public static string Message(ErrorMessages message)
        {
            return message switch
            {
                ErrorMessages.EntityNull => "The entity passed is null",

                ErrorMessages.DbCannotSaveChanges => "Can not save changes",

                ErrorMessages.EntityNotFounded => "The entity not founded",

                ErrorMessages.ModelValidation => "The request data is not correct.",

                ErrorMessages.AuthUserDoesNotExists => "The user doesn't not exists",

                ErrorMessages.AuthWrongCredentials => "The email or password are wrong",

                ErrorMessages.AuthCannotCreate => "Cannot create user",

                ErrorMessages.AuthCannotDelete => "Cannot delete user",

                ErrorMessages.AuthCannotUpdate => "Cannot update user",

                ErrorMessages.AuthNotValidInformation => "Invalid information",

                ErrorMessages.AuthCannotRetrieveToken => "Cannot retrieve token",

                _ => throw new ArgumentOutOfRangeException(nameof(message), message, null)
            };
        }
    }
}
