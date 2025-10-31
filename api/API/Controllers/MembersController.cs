using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
// https://localhost:5001/api/members
public class MembersController(IMembersRepository membersRepository) : BaseApiController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
    {
        return Ok(await membersRepository.GetMembersAsync());
    }

    [AllowAnonymous]
    [HttpGet("{id}")] // https://localhost:5001/api/members/{id}
    public async Task<ActionResult<Member>> GetMember(string id)
    {
        var member = await membersRepository.GetMemberAsync(id);
        if (member == null) return NotFound();
        return member;
    }

    [AllowAnonymous]
    [HttpGet("{id}/photos")]
    public async Task<ActionResult<IReadOnlyList<Photo>>> GetPhotos(string id)
    {
        return Ok(await membersRepository.GetPhotosAsync(id));
    }
}
