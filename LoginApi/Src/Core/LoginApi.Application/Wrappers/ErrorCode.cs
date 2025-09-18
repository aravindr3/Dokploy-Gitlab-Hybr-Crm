namespace HyBrForex.Application.Wrappers;

public enum ErrorCode : short
{
    ModelStateNotValid = 0,
    FieldDataInvalid = 1,
    NotFound = 2,
    AccessDenied = 3,
    ErrorInIdentity = 4,
    Exception = 5,
    BadRequest = 6,
    AlreadyExists = 7,
    Validation = 8,
    InvalidArgument = 9
}