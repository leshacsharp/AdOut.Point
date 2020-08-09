﻿using AdOut.Point.Model;
using System.Linq;
using System.Security.Claims;

namespace AdOut.Point.WebApi.Claims
{
    public static class ClaimsHelper
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.Claims.SingleOrDefault(c => c.Type == Constants.ClaimsTypes.UserId)?.Value;
        }
    }
}