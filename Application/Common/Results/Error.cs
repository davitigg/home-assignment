namespace Application.Common.Results
{
    public sealed record Error(string Code, string Description)
    {
        internal static Error None => new(ErrorTypeConstant.None, string.Empty);
        internal static Error UserNameAlreadyExists => new(ErrorTypeConstant.Conflict, "User name already exists");
        internal static Error UserNotFound => new(ErrorTypeConstant.NotFound, "User not found");
        internal static Error OtpResendNotPermitted => new(ErrorTypeConstant.BadRequest, "OTP resend is not permitted at this time. Please wait before requesting another OTP.");
        internal static Error OtpInvalidOrExpired => new(ErrorTypeConstant.BadRequest, "OTP is invalid or expired");
        internal static Error DeviceNotFoundOrSessionExpired => new(ErrorTypeConstant.NotFound, "Device session not found or has expired.");

    }
}
