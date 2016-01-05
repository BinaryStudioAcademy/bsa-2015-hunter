namespace SOS.Mobile.Common.Server
{
    /// <summary>
    /// Error/validation codes from server - should be updated to sos.mobile needs
    /// </summary>
    public enum ErrorCodes
    {
        /// <summary>
        /// No errors.
        /// </summary>
        None = 0,

        General_UnknownError,

        /// <summary>
        /// The user email wasn't confirmed.
        /// </summary>
        Security_WaitConfirmation,

        Security_WaitPasswordChange,

        Security_NoPermission,

        Security_UserNotFound,

        Security_IncorrectPassword,

        Security_TimeExpired,

        Security_IncorrectVersion,

        Security_UserAlreadyExists,
    }
}