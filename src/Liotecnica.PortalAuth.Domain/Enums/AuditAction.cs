namespace Liotecnica.PortalAuth.Domain.Enums;

public enum AuditAction
{
    LoginSucceeded = 1,
    LoginFailed = 2,
    Logout = 3,
    Created = 4,
    Updated = 5,
    PasswordReset = 6,
    Deactivated = 7,
    Reactivated = 8,
    Deleted = 9,
    RetentionUpdated = 10,
    AuditPurged = 11,
    AuditExported = 12,
    PasswordChanged = 13,
    StatusSnapshotRecorded = 14
}
