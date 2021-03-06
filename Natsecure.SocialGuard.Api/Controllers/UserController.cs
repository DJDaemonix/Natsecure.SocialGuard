﻿using Microsoft.AspNetCore.Mvc;
using Natsecure.SocialGuard.Api.Data.Models;
using Natsecure.SocialGuard.Api.Services;
using Natsecure.SocialGuard.Api.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Natsecure.SocialGuard.Api.Controllers
{
	[ApiController, Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly TrustlistUserService service;

		public UserController(TrustlistUserService service)
		{
			this.service = service;
		}

		/// <summary>
		/// Enumerates all users present in the Trustlist.
		/// </summary>
		/// <response code="200">Returns List</response>
		/// <response code="204">If Trustlist is empty</response>    
		/// <returns>List of user IDs</returns>
		[HttpGet("list"), ProducesResponseType(200), ProducesResponseType(204)]
		public IActionResult ListUsersIds()
		{
			IEnumerable<ulong> users = service.ListUserIds();
			return users.Any()
				? StatusCode(200, users)
				: StatusCode(204);
		}


		/// <summary>
		/// Gets Trustlist record on user with specified Trustlist
		/// </summary>
		/// <param name="id">ID of user</param>
		/// <response code="200">Returns record</response>
		/// <response code="404">If user ID is not found in DB</response>    
		/// <returns>Trustlist info</returns>
		[HttpGet("{id}"), ProducesResponseType(200), ProducesResponseType(404)]
		public async Task<IActionResult> FetchUser(ulong id) 
		{
			TrustlistUser user = await service.FetchUserAsync(id);
			return StatusCode(user is not null ? 200 : 404, user);
		}


		/// <summary>
		/// Inserts record into Trustlist
		/// </summary>
		/// <param name="userRecord">User record to insert</param>
		/// <response code="201">User was created</response>
		/// <response code="409">If User record already exists</response> 
		[HttpPost, AccessKey(AccessScopes.Insert), ProducesResponseType(201), ProducesResponseType(409)]
		public async Task<IActionResult> InsertUserRecord([FromBody] TrustlistUser userRecord) 
		{
			try
			{
				await service.InsertNewUserAsync(userRecord);
			}
			catch (ArgumentOutOfRangeException)
			{
				return StatusCode(409, "User already exists in DB. Use PUT request to update his Escalation Level.");
			}

			return StatusCode(201);
		}

		/// <summary>
		/// Escalates existing record in Trustlist
		/// </summary>
		/// <param name="userRecord">User record to escalate</param>
		/// <response code="202">Record escalation request was accepted</response>
		/// <response code="404">If user ID is not found in DB</response>
		[HttpPut, AccessKey(AccessScopes.Escalate), ProducesResponseType(202), ProducesResponseType(404)]
		public async Task<IActionResult> EscalateUserRecord([FromBody] TrustlistUser userRecord) 
		{
			try
			{
				await service.EscalateUserAsync(userRecord);
			}
			catch (ArgumentOutOfRangeException)
			{
				return StatusCode(404, "No user found in DB.");
			}

			return StatusCode(202);
		}


		/// <summary>
		/// Wipes User record from Trustlist
		/// </summary>
		/// <param name="id">ID of User to wipe</param>
		/// <response code="200">Record was wiped (if any)</response>
		[HttpDelete("{id}"), AccessKey(AccessScopes.Delete)]
		public async Task<IActionResult> DeleteUserRecord(ulong id) 
		{
			await service.DeleteUserAsync(id);
			return StatusCode(200);
		}
	}
}
