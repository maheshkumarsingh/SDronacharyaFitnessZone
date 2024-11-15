using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using SDronacharyaFitnessZone.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.DTOs;

namespace WebApp.Core.ServiceContracts
{
    public interface IPhotoService
    {
        Task<PhotoResponseDTO> AddPhotoAsync(IFormFile formFile, MemberResponseDTO memberResponse);
        Task<DeletionResult> DeletePhotoAsync(MemberResponseDTO photoResponse, int photoId);
    }
}
