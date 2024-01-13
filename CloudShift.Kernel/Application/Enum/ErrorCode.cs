namespace CloudShift.Kernel.Application.Enum
{
    public enum ErrorCode
    {
        None,
        InvalidInput = 400,
        NotAuthenticated = 401,
        NotAuthorized = 403,
        NotFound = 404,
        IntegrationCommunicationError = 502
    }
}
