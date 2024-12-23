﻿using Microsoft.AspNetCore.Authorization;
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
                var status = await _memberService.UpdateMember(requestDTO);
                if(status > 0)
                return Ok(requestDTO);
            }
            return BadRequest("Member cannot be updated. Check fields");
            //Member member = await _dbContext.Members.FindAsync(requestDTO.MemberLoginName);
            //member.FirstName = requestDTO.FirstName;
            //try
            //{
            //    await _dbContext.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            //return Ok(requestDTO);
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
            var status = await _memberService.SetMainPhotoForMember(photoId, memberResponse);

            if(status) return NoContent();
            return BadRequest("Fail");
        }
        [HttpDelete("delete-photo/{photoId:int}")]
        public async Task<ActionResult> DeleteMemberPhoto(int photoId)
        {
            var memberResponse = await _memberService.GetMemberById(User.GetMemberLoginNameByClaim());
            var cloudinaryStatus = await _photoService.DeletePhotoAsync(memberResponse, photoId);
            if (cloudinaryStatus == null) return BadRequest(cloudinaryStatus.Error.Message);
            var dbStatus = await _memberService.DeleteMemberPhoto(memberResponse, photoId);
            if (dbStatus)
                return Ok();
            return BadRequest("Photo not deleted from DB");
        }
    }

}
