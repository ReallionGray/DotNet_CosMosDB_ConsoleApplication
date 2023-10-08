using Console_Api.Models.Request_Response.Preview;

namespace Console_Api.Interfaces
{
    public interface IPreviewService
    {
        Task<PreviewResponse> GetPreview(string ProgramId);
    }
}