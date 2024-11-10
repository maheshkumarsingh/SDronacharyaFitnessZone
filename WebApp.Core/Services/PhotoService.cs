using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SDronacharyaFitnessZone.Core.DTOs;
using System.Reflection.Metadata.Ecma335;
using WebApp.Core.Domain.Entities;
using WebApp.Core.Domain.RepositoryContracts;
using WebApp.Core.DTOs;
using WebApp.Core.Helpers;
using WebApp.Core.ServiceContracts;

namespace WebApp.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IMemberRepository _memberRepository;
        public PhotoService(IOptions<CloudinarySettings> config, IMemberRepository memberRepository)
        {
            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            _cloudinary = new Cloudinary(acc);
            _memberRepository = memberRepository;
        }
        public async Task<PhotoResponseDTO> AddPhotoAsync(IFormFile formFile, MemberResponseDTO memberResponse)
        {
            var uploadResult = new ImageUploadResult();
            if (formFile.Length > 0)
            {
                using var stream = formFile.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(formFile.FileName, stream),
                    Transformation = new Transformation()
                                                .Height(500)
                                                .Width(500)
                                                .Crop("fill")
                                                .Gravity("face"),
                    Folder = "SDronacharyaFitnessZone"
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            Member member = await _memberRepository.GetMemberById(memberResponse.MemberLoginName);
            Photo photo= null;
            if (uploadResult.Error == null)
            {
                if (member != null)
                {
                    photo = new Photo()
                    {
                        Url = uploadResult.SecureUrl.AbsoluteUri,
                        PublicId = uploadResult.PublicId,
                        Member = member,
                    };
                    member.Photos.Add(photo);
                    member = await _memberRepository.AddMemberPhoto(member, photo);
                }
            }
            if (member != null && photo != null)
                return photo.ToPhotoResponseDTO();
            return null;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParam = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParam);
        }
    }
}
