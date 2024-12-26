using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using WebApp.Core.DTOs;

namespace WebApp.Core.ServiceContracts
{
    public interface IPhotoService
    {
        Task<PhotoResponseDTO> AddPhotoAsync(IFormFile formFile, MemberResponseDTO memberResponse);
        Task<DeletionResult> DeletePhotoAsync(MemberResponseDTO photoResponse, int photoId);
    }
}
