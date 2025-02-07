namespace Application.Common.Results
{
    public sealed record Error(string Code, string Description)
    {
        internal static Error None => new(ErrorTypeConstant.None, string.Empty);
        internal static Error UserNotFound => new(ErrorTypeConstant.NotFound, "User not found.");
        internal static Error DeviceNotFound => new(ErrorTypeConstant.NotFound, "Device not found.");
        internal static Error PendingDeviceNotFound => new(ErrorTypeConstant.NotFound, "Pending device not found.");
        internal static Error OtpNotFound => new(ErrorTypeConstant.NotFound, "OTP not found.");
        internal static Error InvalidOtp => new(ErrorTypeConstant.BadRequest, "Invalid OTP.");
        internal static Error DeviceVerificationIncomplete => new(ErrorTypeConstant.BadRequest, "Device verification is incomplete.");
        internal static Error UserNameAlreadyExists => new(ErrorTypeConstant.Conflict, "User name already exists.");
    }
}
