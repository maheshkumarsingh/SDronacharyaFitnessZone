using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDronacharyaFitnessZone.Core.DTOs;
using System.Security.Claims;
using WebApp.Core.Domain.Entities;
using WebApp.Core.DTOs;
using WebApp.Core.ServiceContracts;
using WebApp.Infrastructure.DBContext;
using WebApp.UserInterface.Extensions;

namespace SDronacharyaFitnessZone.UserInterface.Controllers
{
    [Authorize]
    public class MembersController : BaseAPIController
    {
        private readonly IMemberService _memberService;
        private readonly IPhotoService _photoService;
        private readonly ApplicationDBContext _dbContext;

        public MembersController(IMemberService memberService, IPhotoService photoService, ApplicationDBContext dBContext)
        {
            _memberService = memberService;
            _photoService = photoService;
            _dbContext = dBContext;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<MemberResponseDTO>>> GetAllMembers()
        {
            var members = await _memberService.GetAllMembers();
            return Ok(members);
        }
        [HttpGet("{memberLoginName}")]
        public async Task<IActionResult> GetMemberById(string memberLoginName)
        {
            var member = await _memberService.GetMemberById(memberLoginName);
            if (member == null)
                return NotFound();
            return Ok(member);
        }
        [HttpPost]
        public async Task<ActionResult<MemberResponseDTO>> CreateMember(AddMemberRequestDTO requestDTO)
        {
            MemberResponseDTO memberResponseDTO = await _memberService.CreateMember(requestDTO);
            return Ok(memberResponseDTO);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteMember(string memberLoginName)
        {
            return Ok(true);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateMember(UpdateMemberRequestDTO requestDTO)
        {
            var memberResponse = await _memberService.GetMemberById(User.GetMemberLoginNameByClaim());
            if (memberResponse != null)
            {
                var memberResponseDTO = _memberService.UpdateMember(requestDTO);
                return Ok(memberResponseDTO);
            }
            return BadRequest();
        }
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoResponseDTO>> AddMemberPhoto(IFormFile file)
        {
            var memberResponse = await _memberService.GetMemberById(User.GetMemberLoginNameByClaim());
            if (memberResponse != null)
            {
                var photoResponseDTO = await _photoService.AddPhotoAsync(file, memberResponse);
                return CreatedAtAction(nameof(GetMemberById), new
                { memberLoginName = memberResponse.MemberLoginName },
                            photoResponseDTO);
            }
            return BadRequest("Cannot add photo");
        }
        [HttpPut("set-main-photo/{photoId:int}")]
        public async Task<ActionResult> SetMainPhotoForMember(int photoId)
        {
            var memberResponse = await _memberService.GetMemberById(User.GetMemberLoginNameByClaim());
            if (memberResponse == null) return BadRequest("Could not find user");
            var photo = memberResponse.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo != null || photo.IsMain) return BadRequest("Cannot use as main photo");
            var currentMain = memberResponse.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null)
            {
                currentMain.IsMain = false;
            }
            photo.IsMain = true;
            if ((await _dbContext.SaveChangesAsync()) > 0)
                return NoContent();
            return BadRequest("Fail");
        }
    }

}
