using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Peak_Performance.Models;
using Peak_Performance.DAL;
using System.Web.Routing;
using reCAPTCHA.MVC;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;

namespace Peak_Performance.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private PeakPerformanceContext db = new PeakPerformanceContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    //for admin's logging in, redirect to admin home page
                    var role = db.AspNetUsers.Where(r => r.Email == model.Email).Select(r => r.AspNetRoles.Select(t => t.Name)).First().ToArray();
                    if (role[0] == "Admin")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (role[0] == "Coach")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Coach" });
                    }
                    else if (role[0] == "Athlete")
                    {
                        return RedirectToAction("Index", "Home", new { area = "Athlete" });
                    }
                    return RedirectToAction(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes.
            // If a user enters incorrect codes for a specified amount of time then the user account
            // will be locked out for a specified amount of time.
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                //return RedirectToLocal(model.ReturnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewData["Roles"] = db.AspNetRoles.ToList();
            ViewData["Teams"] = new SelectList(db.Teams.ToList(), "ID", "TeamName");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [CaptchaValidator]
        public async Task<ActionResult> Register(RegistrationTypes model, bool captchaValid)
        {
            bool isAdmin, isCoach, isAthlete;
            isAdmin = isCoach = isAthlete = false;
            string tempEmail, tempPassword, tempFName, tempLName, tempSex, tempGender;
            tempEmail = tempPassword = tempFName = tempLName = tempSex = tempGender = null;
            DateTime tempDOB = new DateTime(DateTime.MinValue.Ticks);
            int tempHeight, tempWeight;
            tempHeight = tempWeight = 0;

            if (ModelState.IsValid)
            {
                if (model.adminVM != null)
                {
                    isAdmin = true;
                    tempEmail = model.adminVM.Email;
                    tempPassword = model.adminVM.Password;
                }
                if (model.coachVM != null)
                {
                    isCoach = true;
                    tempEmail = model.coachVM.Email;
                    tempPassword = model.coachVM.Password;
                    tempFName = model.coachVM.FirstName;
                    tempLName = model.coachVM.LastName;
                }
                if (model.athleteVM != null)
                {
                    isAthlete = true;
                    tempEmail = model.athleteVM.Email;
                    tempPassword = model.athleteVM.Password;
                    tempFName = model.athleteVM.FirstName;
                    tempLName = model.athleteVM.LastName;
                    tempDOB = model.athleteVM.DOB;
                    string heightFeet = Request.Form["feet"].ToString();
                    string heightInches = Request.Form["inches"].ToString();
                    if (heightFeet != "")
                    {
                        tempHeight = (Convert.ToInt32(heightFeet) * 12) + Convert.ToInt32(heightInches);
                    }
                    string weight = Request.Form["weight"].ToString();
                    if (weight != "")
                    {
                        tempWeight = Convert.ToInt32(weight);
                    }
                    string sex = Request.Form["sex"].ToString();
                    if (sex != "")
                    {
                        tempSex = sex;
                    }
                    string gender = Request.Form["gender"].ToString();
                    if (gender != "")
                    {
                        tempGender = gender;
                    }
                }

                //if(isCoach || isAthlete) {
                //    //confirm account via email
                //}

                var user = new ApplicationUser { UserName = tempEmail, Email = tempEmail };
                var result = await UserManager.CreateAsync(user, tempPassword);

                if (result.Succeeded)
                {
                    string subject = "Please confirm your Peak Performance email";
                    string callbackUrl = await SendConfirmationTokenAsync(user.Id, subject, tempFName);

                    var tempUser = new Person
                    {
                        FirstName = tempFName,
                        LastName = tempLName,
                        ASPNetIdentityID = user.Id,
                        Active = true
                    };

                    PeakPerformanceContext db = new PeakPerformanceContext();

                    if (model.adminVM != null)
                    {
                        UserManager.AddToRole(user.Id, "Admin");
                    }
                    if (model.coachVM != null)
                    {
                        var newCoach = new Coach
                        {
                        };

                        newCoach.Person = tempUser;
                        db.Persons.Add(tempUser);
                        db.Coaches.Add(newCoach);
                        UserManager.AddToRole(user.Id, "Coach");
                    }
                    if (model.athleteVM != null)
                    {
                        var newAthlete = new Athlete
                        {
                            DOB = tempDOB,
                            Height = tempHeight,
                            Weight = tempWeight,
                            Sex = tempSex,
                            Gender = tempGender
                        };

                        newAthlete.Person = tempUser;
                        db.Persons.Add(tempUser);
                        db.Athletes.Add(newAthlete);
                        UserManager.AddToRole(user.Id, "Athlete");
                    }

                    await db.SaveChangesAsync();
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    if (isAdmin)
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (isCoach)
                    {
                        return RedirectToAction("Index", "Home", new { area = "Coach" });
                    }
                    else if (isAthlete)
                    {
                        return RedirectToAction("Index", "Home", new { area = "Athlete" });
                    }
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form

            return View(model);
        }

        private async Task<string> SendConfirmationTokenAsync(string userID, string subject, string name)
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userID, code = code }, protocol: Request.Url.Scheme);
            string emailBody = "Hello " + name + ", please follow <a href=\"" + callbackUrl + "\"> this link</a> to confirm your <i>Peak Performance</i> account";

            await UserManager.SendEmailAsync(userID, subject, emailBody);

            return callbackUrl;
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendConfirmationEmail(string urlOfReferrer)
        {
            string id = User.Identity.GetUserId();
            await SendConfirmationTokenAsync(id, "Confirm your account", ",");
            ViewBag.EmailSent = true;
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Home", action = "Index", message = AccountMessageId.EmailSentSuccess }));
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        ////
        //// POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        ////
        //// GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);

        //        case SignInStatus.LockedOut:
        //            return View("Lockout");

        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });

        //        case SignInStatus.Failure:
        //        default:
        //            // If the user does not have an account, then prompt the user to create an account
        //            ViewBag.ReturnUrl = returnUrl;
        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        ////
        //// POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #endregion Helpers

        [Authorize]
        public ActionResult RegisterAdmin()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> RegisterAdmin(AdminRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var currentUser = UserManager.FindByName(user.UserName);

                    var roleresult = UserManager.AddToRole(currentUser.Id, "Admin");

                    return RedirectToAction("CreateAdminSuccess", "Account");
                }
            }
            // If we got this far, something failed
            return RedirectToAction("CreateAdminFail", "Account");
        }

        public ActionResult CreateAdminSuccess()
        {
            return View();
        }

        public ActionResult CreateAdminFail()
        {
            return View();
        }

        [Authorize]
        public ActionResult RegisterCoach()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> RegisterCoach(CoachRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var currentUser = UserManager.FindByName(user.UserName);

                    var roleresult = UserManager.AddToRole(currentUser.Id, "Coach");

                    return RedirectToAction("CreateCoachSuccess", "Account");
                }
            }
            // If we got this far, something failed
            return RedirectToAction("CreateCoachFail", "Account");
        }

        public ActionResult CreateCoachSuccess()
        {
            return View();
        }

        public ActionResult CreateCoachFail()
        {
            return View();
        }

        [Authorize]
        public ActionResult RegisterAthlete()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> RegisterAthlete(AthleteRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var currentUser = UserManager.FindByName(user.UserName);

                    var roleresult = UserManager.AddToRole(currentUser.Id, "Athlete");

                    return RedirectToAction("CreateAthleteSuccess", "Account");
                }
            }
            // If we got this far, something failed
            return RedirectToAction("CreateAthleteFail", "Account");
        }

        public ActionResult CreateAthleteSuccess()
        {
            return View();
        }

        public ActionResult CreateAthleteFail()
        {
            return View();
        }
    }
}