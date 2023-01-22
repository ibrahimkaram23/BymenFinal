using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BymenFinal.Models;

namespace BymenFinal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        BiruetDbContext db = new BiruetDbContext();
        public ActionResult Index()
        {
            var spon = db.sponsers.ToList();
            return View(spon);
        }
        public ActionResult AboutUs()
        {
            var team = db.teams.ToList();
            return View(team);
        }
        public ActionResult Conference()
        {
            return View();
        }
        public ActionResult Announcement()
        {
            var annou = db.Announcements.ToList();
            return View(annou);
        }
        public ActionResult Apply()
        {
            fillDrops();
            return View();
        }
        public ActionResult ApplyDelegation()
        {
            fillDrops();
            return View();
        }

        private void fillDrops()
        {
            List<SelectListItem> ModesList = new List<SelectListItem>();
            ModesList.Add(new SelectListItem { Text = "Online", Value = "Online" });
            ModesList.Add(new SelectListItem { Text = "In-Person", Value = "In-Person" });
            ViewBag.modes = ModesList;

            List<SelectListItem> regType = new List<SelectListItem>();
            regType.Add(new SelectListItem { Text = "Delegates", Value = "Delegates" });
            regType.Add(new SelectListItem { Text = "Delegation", Value = "Delegation" });
            ViewBag.types = regType;
        }

        public PartialViewResult FindCommitees(string mode)
        {
            var com = db.commitees.Where(n => n.comType == mode).ToList();
            return PartialView("FindCommitees",com);
        }
        [HttpPost]
        public ActionResult Apply([Bind(Include = "fullName,emailAddress,university,eduYear,eduMajor,country,conferaneModel,committee,age,enrolledAs,gender,nationality,countryCode,phoneNb,isVaccinated,doses,reference")] person model, HttpPostedFileBase profilePic, HttpPostedFileBase passportPict, HttpPostedFileBase vaccinationCert)
        {
            if (ModelState.IsValid)
            {
                string profilePicn = null;
                string vaccinationCertn = null;
                string passportPictn = null;

                if (profilePic == null)
                {
                    TempData["Error"] = "Please attach payment proof and your pic";
                    fillDrops();
                    return View(model);
                }
                else
                {
                    profilePicn = Guid.NewGuid().ToString() + profilePic.FileName;
                    profilePic.SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/ProfilePic/") + profilePicn);
                    vaccinationCertn = Guid.NewGuid().ToString() + vaccinationCert.FileName;
                    vaccinationCert.SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/vaccinationCert/") + vaccinationCertn);
                    passportPictn = Guid.NewGuid().ToString() + passportPict.FileName;
                    passportPict.SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/passportPict/") + passportPictn);
                    //PaymentProofn = Guid.NewGuid().ToString() + Paymentfile.FileName;
                    //Paymentfile.SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/PaymentProof/") + PaymentProofn);
                }

                try
                {
                    db.persons.Add(new person
                    {
                        fullName = model.fullName,
                        emailAddress = model.emailAddress,
                        university = model.university,
                        eduMajor = model.eduMajor,
                        eduYear = model.eduYear,
                        conferaneModel = model.conferaneModel,
                        committee = model.committee,
                        age = model.age,
                        enrolledAs = model.enrolledAs,
                        gender = model.gender,
                        nationality = model.nationality,
                        countryCode = model.countryCode,
                        phoneNb = model.phoneNb,
                        isVaccinated = model.isVaccinated,
                        doses = model.doses,
                        reference = model.reference,
                        country = model.country,
                        profilePic = profilePicn,
                        partType = "Delegate"
                    });
                    db.SaveChanges();
                    TempData["Success"] = "Success Registration See You There!";
                    fillDrops();
                    return View();
                }
                catch (Exception ex)
                {

                    TempData["Error"] = ex.Message;
                    fillDrops();
                    return View(model);
                }
            }
            else
            {
                TempData["Error"] = "Please fill all requirements";
                fillDrops();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ApplyDelegation([Bind(Include = "fullName,emailAddress,university,eduYear,eduMajor,country,conferaneModel,committee,age,enrolledAs,gender,nationality,countryCode,phoneNb,isVaccinated,doses,reference")] person model, HttpPostedFileBase profilePic, HttpPostedFileBase passportPict, HttpPostedFileBase vaccinationCert)
        {
            if (ModelState.IsValid)
            {
                string profilePicn = null;
                string vaccinationCertn = null;
                string passportPictn = null;

                if (profilePic == null)
                {
                    TempData["Error"] = "Please attach payment proof and your pic";
                    fillDrops();
                    return View(model);
                }
                else
                {
                    profilePicn = Guid.NewGuid().ToString() + profilePic.FileName;
                    profilePic.SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/ProfilePic/") + profilePicn);
                    vaccinationCertn = Guid.NewGuid().ToString() + vaccinationCert.FileName;
                    vaccinationCert.SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/vaccinationCert/") + vaccinationCertn);
                    passportPictn = Guid.NewGuid().ToString() + passportPict.FileName;
                    passportPict.SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/passportPict/") + passportPictn);
                    //PaymentProofn = Guid.NewGuid().ToString() + Paymentfile.FileName;
                    //Paymentfile.SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/PaymentProof/") + PaymentProofn);
                }

                try
                {
                    db.persons.Add(new person
                    {
                        fullName = model.fullName,
                        emailAddress = model.emailAddress,
                        university = model.university,
                        eduMajor = model.eduMajor,
                        eduYear = model.eduYear,
                        conferaneModel = model.conferaneModel,
                        committee = model.committee,
                        age = model.age,
                        enrolledAs = model.enrolledAs,
                        gender = model.gender,
                        nationality = model.nationality,
                        countryCode = model.countryCode,
                        phoneNb = model.phoneNb,
                        isVaccinated = model.isVaccinated,
                        doses = model.doses,
                        reference = model.reference,
                        country = model.country,
                        profilePic = profilePicn,
                        vaccinationCert = vaccinationCertn,
                        passportPict = passportPictn,
                        partType = "Delegation"
                    });
                    db.SaveChanges();
                    TempData["Success"] = "Success Registration See You There!";
                    fillDrops();
                    return View();
                }
                catch (Exception ex)
                {

                    TempData["Error"] = ex.Message;
                    fillDrops();
                    return View(model);
                }
            }
            else
            {
                TempData["Error"] = "Please fill all requirements";
                fillDrops();
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult ApplyDelegationV2(Delegation model)
        {
                string profilePicn = null;
                string vaccinationCertn = null;
                string passportPictn = null;
                if (model.profilePic == null)
                {
                    TempData["Error"] = "Please attach payment proof and your pic";
                    fillDrops();
                    return View(model);
                }
                else
                {
                    for (int i = 0; i < model.profilePic.Length; i++)
                    {
                        profilePicn = Guid.NewGuid().ToString() + model.profilePic[i].FileName;
                        model.profilePic[i].SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/ProfilePic/") + profilePicn);
                        vaccinationCertn = Guid.NewGuid().ToString() + model.vaccinationCert[i].FileName;
                        model.vaccinationCert[i].SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/vaccinationCert/") + vaccinationCertn);
                        passportPictn = Guid.NewGuid().ToString() + model.passportPict[i].FileName;
                        model.passportPict[i].SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/passportPict/") + passportPictn);
                        try
                        {
                            db.persons.Add(new person
                            {
                                fullName = model.fullName[i],
                                emailAddress = model.emailAddress[i],
                                university = model.university[i],
                                eduMajor = model.eduMajor[i],
                                eduYear = model.eduYear[i],
                                conferaneModel = model.conferaneModel[i],
                                committee = model.committee[i],
                                age = model.age[i],
                                enrolledAs = model.enrolledAs[i],
                                gender = model.gender[i],
                                nationality = model.nationality[i],
                                countryCode = model.countryCode[i],
                                phoneNb = model.phoneNb[i],
                                isVaccinated = model.isVaccinated[i],
                                doses = model.doses[i],
                                reference = model.reference[i],
                                country = model.country[i],
                                profilePic = profilePicn,
                                vaccinationCert = vaccinationCertn,
                                passportPict = passportPictn,
                                partType = "Delegation"
                            });
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            TempData["Error"] = ex.Message;
                            fillDrops();
                            return View(model);
                        }
                    }
                    
                    //PaymentProofn = Guid.NewGuid().ToString() + Paymentfile.FileName;
                    //Paymentfile.SaveAs(HttpContext.Server.MapPath("~/Content/Partecepant/PaymentProof/") + PaymentProofn);
                }
                TempData["Success"] = "Success Registration See You There!";
                fillDrops();
                return View();
        }
        public ActionResult Commitees()
        {
            return View();
        }
        public ActionResult ECOFIN()
        {
            return View();
        }
        public ActionResult ECOSOC()
        {
            return View();
        }
        public ActionResult ICAO()
        {
            return View();
        }
        public ActionResult ILO()
        {
            return View();
        }
        public ActionResult INTERPOL()
        {
            return View();
        }
        public ActionResult SOCHUM()
        {
            return View();
        }
        public ActionResult UNODC()
        {
            return View();
        }
        public ActionResult UNSC()
        {
            return View();
        }
        public ActionResult WHO()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            var photos = db.Galleries.ToList();
            return View(photos);
        }
        public ActionResult VolApply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VolApply(Volanteer vol)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    db.Volanteers.Add(vol);
                    db.SaveChanges();
                    TempData["Success"] = "Success, We will contact you soon!";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
                
                return View();
            }
            else
            {
                TempData["Error"] = "Please enter data";
                return View();
            }

        }

        public PartialViewResult VolRoute(string position)
        {
            if (position == "1")
            {
                return PartialView("RelationsNational");
            }
            else if (position == "2")
            {
                return PartialView("RelationsInternational");
            }
            else if (position == "3")
            {
                return PartialView("FundraisingSponsorship");
            }
            else if (position == "4")
            {
                return PartialView("SocialEvents");
            }
            else if (position == "5")
            {
                return PartialView("Marketing");
            }
            else if (position == "6")
            {
                return PartialView("GraphicDesign");
            }
            else if (position == "7")
            {
                return PartialView("Logistics");
            }
            else if (position == "8")
            {
                return PartialView("Development");
            }
            else if (position == "9")
            {
                return PartialView("SafetyandSecurity");
            }
            else
            {
                return PartialView();
            }

        }
        public ActionResult RulesofProcedure()
        {
            return View();
        }
        public ActionResult DelegatesGuide()
        {
            return View();
        }
    }
}