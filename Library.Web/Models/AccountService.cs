using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Library.Web.Models
{
    public class AccountService //: IAccountService
    {
    //    private readonly LibraryContext _context;
    //    private readonly HttpContext _httpContext;

    //    public AccountService(LibraryContext context,
    //        IHttpContextAccessor httpContextAccessor)
    //    {
    //        _context = context;
    //        _httpContext = httpContextAccessor.HttpContext!;

    //        if(_httpContext.Request.Cookies.ContainsKey("user_challenge") &&
    //            !_httpContext.Session.Keys.Contains("user"))
    //        {
    //            Visitor? visitor = _context.Visitors.FirstOrDefault(
    //                v => v.UserChallenge == _httpContext.Request.Cookies["user-challenge"]);
    //            if(visitor != null)
    //            {
    //                _httpContext.Session.SetString("user", visitor.UserName);
    //            }
    //        }
    //    }

    //    public String? CurrentUserName => _httpContext.Session.GetString("user");

    //    public Visitor? GetVisitor(String userName)
    //    {
    //        return _context.Visitors.FirstOrDefault(v => v.UserName == userName);
    //    }

    //    public Boolean Login(LoginViewModel user)
    //    {
    //        if(!Validator.TryValidateObject(user, new ValidationContext(user,null,null),null))
    //            return false;

    //        Visitor? visitor = 
    //            _context.Visitors.FirstOrDefault(v => v.UserName == user.UserName);

    //        if (visitor == null)
    //            return false;

    //        Byte[] passwordBytes;
    //        using (var alg = SHA512.Create())
    //        {
    //            passwordBytes = alg.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
    //        }

    //        if (!passwordBytes.SequenceEqual(visitor.Password))
    //            return false;

    //        _httpContext.Session.SetString("user", user.UserName);

    //        if (user.RememberLogin)
    //        {
    //            _httpContext.Response.Cookies.Append("user_challenge",
    //                visitor.UserChallenge,
    //                new CookieOptions
    //                {
    //                    Expires = DateTime.Today.AddDays(365),
    //                    HttpOnly = true
    //                });
    //        }

    //        return true;
    //    }

    //    public Boolean Logout()
    //    {
    //        if (!_httpContext.Session.Keys.Contains("user"))
    //            return false;

    //        _httpContext.Session.Remove("user");

    //        _httpContext.Response.Cookies.Delete("user_challenge");

    //        return true;
    //    }

    //    public Boolean Register(RegistrationViewModel visitor)
    //    {
    //        if (!Validator.TryValidateObject(visitor, new ValidationContext(visitor, null, null), null))
    //            return false;

    //        if(_context.Visitors.Count(v => v.UserName == visitor.UserName) != 0)
    //        {
    //            return false;
    //        }

    //        Byte[] passwordBytes;
    //        using(var alg = SHA512.Create())
    //        {
    //            passwordBytes = alg.ComputeHash(Encoding.UTF8.GetBytes(visitor.Password));
    //        }

    //        _context.Visitors.Add(new Visitor
    //        {
    //            Name = visitor.Name,
    //            Email = visitor.Email,
    //            PhoneNumber = visitor.PhoneNumber,
    //            Password = passwordBytes,
    //            UserName = visitor.UserName,
    //            UserChallenge = Guid.NewGuid().ToString()
    //        });

    //        try
    //        {
    //            _context.SaveChanges();
    //        }
    //        catch
    //        {
    //            return false;
    //        }

    //        return true;
    //    }
    }
}
