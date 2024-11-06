using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDronacharyaFitnessZone.Core.DTOs;
using SDronacharyaFitnessZone.UserInterface.Controllers;
using System.Security.Cryptography;
using WebApp.Core.ServiceContracts;

namespace WebApp.UserInterface.Controllers.Account
{
    public class AccountController : BaseAPIController
    {
        private readonly IMemberService _memberService;

        public AccountController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<MemberResponseDTO>> Register([FromBody] RegisterMemberRequestDTO addMember)
        {
            //Logic for already registered user
            //return await _memberService.RegisterMember(addMember);
            return null;
        }
        [HttpPost("login")]
        public async Task<ActionResult<MemberResponseDTO>> LoginMember([FromBody]LoginMemberDTO loginMember)
        {
            //write logic for login
            MemberResponseDTO memberResponse = await _memberService.GetMemberById(loginMember.MemberLoginName);
            if (memberResponse == null)
            {
                return Unauthorized("Wrong MemberLoginName" + loginMember.MemberLoginName);
            }
            memberResponse = await _memberService.LoginMember(loginMember);
            if (memberResponse == null)
            {
                return BadRequest("Wrong password");
            }
            return memberResponse;
        }
    }
}
