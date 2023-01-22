using BymenFinal.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BymenFinal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        BiruetDbContext db = new BiruetDbContext();
        public ActionResult Login()
        {
            Session["Email"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (email != null && password != null)
            {
                var admin = db.admins.Where(n => n.Email == email && n.Password == password).FirstOrDefault();
                if (admin != null)
                {
                    Session["Email"] = admin.Email;
                    Session["Name"] = admin.Name;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Email or password are not correct!";
                    return View(model: new { email, password });

                }
            }
            else
            {
                TempData["Error"] = "Enter required fileds";
                return View(model: new {email,password});

            }
        }
        public ActionResult Index()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                Session["RegCount"] = db.persons.Count();
                Session["InPerson"] = db.persons.Where(n => n.conferaneModel == "In-Person").Count();
                Session["Online"] = db.persons.Where(n => n.conferaneModel == "Online").Count();
                var persons = db.persons.ToList();
                return View(persons);
            }
        }
        public ActionResult OurTeam()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var model = db.teams.ToList();
                return View(model);
            }
        }
        public ActionResult AddTeamMember()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddTeamMember([Bind(Include = "Name,Position,qute")] team team, HttpPostedFileBase img)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    string teamPic = null;
                    if (img == null)
                    {
                        TempData["Error"] = "Please Upload Image";
                        return View(team);
                    }
                    else
                    {
                        teamPic = Guid.NewGuid().ToString() + img.FileName;
                        img.SaveAs(HttpContext.Server.MapPath("~/Content/images/Team/") + teamPic);
                        try
                        {
                            db.teams.Add(new team
                            {
                                image = teamPic,
                                Name = team.Name,
                                Position = team.Position,
                                qute = team.qute
                            });
                            db.SaveChanges();
                            return RedirectToAction("OurTeam");
                        }
                        catch (Exception ex)
                        {

                            TempData["Error"] = ex.Message;
                            return View(team);
                        }

                    }
                }
                else
                {
                    TempData["Error"] = "Please Fill All Required Data!";
                    return View(team);
                }
            }
        }
        public ActionResult DeleteTeam(int? Id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Id != null)
                {
                    var mem = db.teams.Where(n => n.Id == Id).FirstOrDefault();
                    if (mem != null)
                    {
                        db.teams.Remove(mem);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("OurTeam");
            }
        }
        public ActionResult Announcement()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var announc = db.Announcements.ToList();
                return View(announc);
            }
        }
        public ActionResult AddAnnouncement()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddAnnouncement(Announcement anno)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Announcements.Add(new Models.Announcement
                        {
                            Body = anno.Body,
                            Head = anno.Head
                        });
                        db.SaveChanges();
                        return RedirectToAction("Announcement");
                    }
                    catch (Exception ex)
                    {
                        TempData["Error"] = ex.Message;
                        return View(anno);
                    }
                }
                TempData["Error"] = "Please Fill All Required Data!";
                return View(anno);
            }
        }
        public ActionResult DeleteAnnounc(int? Id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Id != null)
                {
                    var mem = db.Announcements.Where(n => n.Id == Id).FirstOrDefault();
                    if (mem != null)
                    {
                        db.Announcements.Remove(mem);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Announcement");
            }
        }
        public ActionResult Commitees()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var com = db.commitees.ToList();
                return View(com);
            }
        }
        public ActionResult AddCommitee()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                fillcommlist();
                return View();
            }
        }

        private void fillcommlist()
        {
            List<SelectListItem> ModesList = new List<SelectListItem>();
            ModesList.Add(new SelectListItem { Text = "Online", Value = "Online" });
            ModesList.Add(new SelectListItem { Text = "In-Person", Value = "In-Person" });
            ViewBag.modes = ModesList;
        }

        [HttpPost]
        public ActionResult AddCommitee(commitee com)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.commitees.Add(com);
                        db.SaveChanges();
                        return RedirectToAction("Commitees");
                    }
                    catch (Exception ex)
                    {

                        TempData["Error"] = ex.Message;
                        fillcommlist();
                        return View();
                    }
                }
                TempData["Error"] = "Please Fill All Required Data";
                fillcommlist();
                return View(com);
            }
        }
        public ActionResult DeleteCommite(int? Id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Id != null)
                {
                    var mem = db.commitees.Where(n => n.Id == Id).FirstOrDefault();
                    if (mem != null)
                    {
                        db.commitees.Remove(mem);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Commitees");
            }
        }
        [HttpGet]
        public ActionResult Download(string Id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Id != null)
                {
                    string path = Server.MapPath("~/Content/Partecepant/PaymentProof/") + Id;
                    return File(path, "application/octet-stream", Id);
                }
                return RedirectToAction("Index");
            }
        }
        public ActionResult Sponsers()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var spons = db.sponsers.ToList();
                return View(spons);
            }
        }
        public ActionResult AddSponser()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddSponser(string Name, HttpPostedFileBase img)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    string teamPic = null;
                    if (img == null)
                    {
                        TempData["Error"] = "Please Upload Image";
                        return View();
                    }
                    else
                    {
                        teamPic = Guid.NewGuid().ToString() + img.FileName;
                        img.SaveAs(HttpContext.Server.MapPath("~/Content/images/Sponsers/") + teamPic);
                        try
                        {
                            db.sponsers.Add(new sponser
                            {
                                Image = teamPic,
                                Name = Name
                                
                            });
                            db.SaveChanges();
                            return RedirectToAction("Sponsers");
                        }
                        catch (Exception ex)
                        {

                            TempData["Error"] = ex.Message;
                            return View();
                        }

                    }
                }
                else
                {
                    TempData["Error"] = "Please Fill All Required Data!";
                    return View();
                }
            }
        }
        public ActionResult DeleteSponser(int? Id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Id != null)
                {
                    var mem = db.sponsers.Where(n => n.Id == Id).FirstOrDefault();
                    if (mem != null)
                    {
                        db.sponsers.Remove(mem);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Sponsers");
            }
        }
        public ActionResult DeleteGal(int? Id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Id != null)
                {
                    var mem = db.Galleries.Where(n => n.Id == Id).FirstOrDefault();
                    if (mem != null)
                    {
                        db.Galleries.Remove(mem);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Gellary");
            }
        }
        public void DownloadPart()
        {
            if (Session["Email"] == null)
            {
                
            }
            else
            {
                var pers = db.persons.ToList();
                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
                Sheet.Cells["A1"].Value = "Id";
                Sheet.Cells["B1"].Value = "fullName";
                Sheet.Cells["C1"].Value = "emailAddress";
                Sheet.Cells["D1"].Value = "university";
                Sheet.Cells["E1"].Value = "eduMajor";
                Sheet.Cells["F1"].Value = "eduYear";
                Sheet.Cells["G1"].Value = "conferenceMode";
                Sheet.Cells["H1"].Value = "committee";
                Sheet.Cells["I1"].Value = "age";
                Sheet.Cells["J1"].Value = "enrolledAs";

                Sheet.Cells["K1"].Value = "gender";
                Sheet.Cells["L1"].Value = "nationality";
                Sheet.Cells["M1"].Value = "countryOfResidence";
                Sheet.Cells["N1"].Value = "phoneNb";
                Sheet.Cells["O1"].Value = "vaccinated";
                Sheet.Cells["P1"].Value = "doses";
                Sheet.Cells["Q1"].Value = "passport/photoID";
                Sheet.Cells["R1"].Value = "vaccinationCert";
                Sheet.Cells["S1"].Value = "howHeard";
                int row = 2;
                foreach (var item in pers)
                {

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.fullName;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.emailAddress;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.university;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.eduMajor;
                    Sheet.Cells[string.Format("F{0}", row)].Value = item.eduYear;
                    Sheet.Cells[string.Format("G{0}", row)].Value = item.conferaneModel;
                    Sheet.Cells[string.Format("H{0}", row)].Value = item.committee;
                    Sheet.Cells[string.Format("I{0}", row)].Value = item.age;
                    Sheet.Cells[string.Format("J{0}", row)].Value = item.enrolledAs;

                    Sheet.Cells[string.Format("K{0}", row)].Value = item.gender;
                    Sheet.Cells[string.Format("L{0}", row)].Value = item.nationality;
                    Sheet.Cells[string.Format("M{0}", row)].Value = item.country;
                    Sheet.Cells[string.Format("N{0}", row)].Value = item.countryCode;
                    Sheet.Cells[string.Format("O{0}", row)].Value = item.isVaccinated;
                    Sheet.Cells[string.Format("P{0}", row)].Value = item.doses;
                    Sheet.Cells[string.Format("Q{0}", row)].Value = item.passportPict;
                    Sheet.Cells[string.Format("R{0}", row)].Value = item.vaccinationCert;
                    Sheet.Cells[string.Format("S{0}", row)].Value = item.reference;
                    row++;
                }


                Sheet.Cells["A:AZ"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();
            }
        }
        public ActionResult Gellary()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var images = db.Galleries.ToList();
                return View(images);  
            }
        }
        public ActionResult AddGellary(HttpPostedFileBase img)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    string teamPic = null;
                    if (img == null)
                    {
                        TempData["Error"] = "Please Upload Image";
                        return View();
                    }
                    else
                    {
                        teamPic = Guid.NewGuid().ToString() + img.FileName;
                        img.SaveAs(HttpContext.Server.MapPath("~/Content/images/Gallary/") + teamPic);
                        try
                        {
                            db.Galleries.Add(new Gallery
                            {
                                Image = teamPic
                            });
                            db.SaveChanges();
                            return RedirectToAction("Sponsers");
                        }
                        catch (Exception ex)
                        {

                            TempData["Error"] = ex.Message;
                            return View();
                        }

                    }
                }
                else
                {
                    TempData["Error"] = "Please Fill All Required Data!";
                    return View();
                }
            }
        }
        public void DownloadVol(int? Id)
        {
            if (Session["Email"] == null)
            {

            }
            else
            {
                if (Id != null)
                {
                    var pers = db.Volanteers.Where(n => n.position == Id).ToList();
                    ExcelPackage Ep = new ExcelPackage();
                    ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
                    if (Id == 1)
                    {

                        Sheet.Cells["A1"].Value = "Id";
                        Sheet.Cells["B1"].Value = "Full Name";
                        Sheet.Cells["C1"].Value = "Phone Number";
                        Sheet.Cells["D1"].Value = "Ex Grad Date";
                        Sheet.Cells["E1"].Value = "Major";
                        Sheet.Cells["F1"].Value = "Were you ever on academic probation?";
                        Sheet.Cells["G1"].Value = "Will you be attending AUB during FALL 2022 and SPRING 2023";
                        Sheet.Cells["H1"].Value = "Have you been a part of BEYMUN before? ";
                        Sheet.Cells["I1"].Value = "Why are you interested in this position?";
                        Sheet.Cells["J1"].Value = "Please rate your English writing skills. from 1 to 5";
                        Sheet.Cells["K1"].Value = "Have you ever held a position of mass communication before? If yes, please elaborate.";
                        Sheet.Cells["L1"].Value = "Do you have a general understanding of the landscape of local universities in Lebanon? Do you have any contacts in those universities? If so, please list them.";
                        int row = 2;
                        foreach (var item in pers)
                        {

                            Sheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.fullName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.phoneNumber;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.exGrad;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.major;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.academicProbation;
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.attendAUB;
                            Sheet.Cells[string.Format("H{0}", row)].Value = item.partBey;
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.gIntersted;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.gEnglishRate;
                            Sheet.Cells[string.Format("K{0}", row)].Value = item.gMassCommunication;
                            Sheet.Cells[string.Format("L{0}", row)].Value = item.sLandscape;
                            row++;
                        }


                        Sheet.Cells["A:AZ"].AutoFitColumns();
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment: filename=" + "RelationsNationalReport.xlsx");
                        Response.BinaryWrite(Ep.GetAsByteArray());
                        Response.End();  
                    }
                    else if (Id == 2)
                    {

                        Sheet.Cells["A1"].Value = "Id";
                        Sheet.Cells["B1"].Value = "Full Name";
                        Sheet.Cells["C1"].Value = "Phone Number";
                        Sheet.Cells["D1"].Value = "Ex Grad Date";
                        Sheet.Cells["E1"].Value = "Major";
                        Sheet.Cells["F1"].Value = "Were you ever on academic probation?";
                        Sheet.Cells["G1"].Value = "Will you be attending AUB during FALL 2022 and SPRING 2023";
                        Sheet.Cells["H1"].Value = "Have you been a part of BEYMUN before? ";
                        Sheet.Cells["I1"].Value = "Why are you interested in this position?";
                        Sheet.Cells["J1"].Value = "Please rate your English writing skills. from 1 to 5";
                        Sheet.Cells["K1"].Value = "Have you ever held a position of mass communication before? If yes, please elaborate.";
                        int row = 2;
                        foreach (var item in pers)
                        {

                            Sheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.fullName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.phoneNumber;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.exGrad;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.major;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.academicProbation;
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.attendAUB;
                            Sheet.Cells[string.Format("H{0}", row)].Value = item.partBey;
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.gIntersted;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.gEnglishRate;
                            Sheet.Cells[string.Format("K{0}", row)].Value = item.gMassCommunication;
                            row++;
                        }


                        Sheet.Cells["A:AZ"].AutoFitColumns();
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment: filename=" + "RelationsInterNationalReport.xlsx");
                        Response.BinaryWrite(Ep.GetAsByteArray());
                        Response.End();
                    }
                    else if (Id == 3)
                    {

                        Sheet.Cells["A1"].Value = "Id";
                        Sheet.Cells["B1"].Value = "Full Name";
                        Sheet.Cells["C1"].Value = "Phone Number";
                        Sheet.Cells["D1"].Value = "Ex Grad Date";
                        Sheet.Cells["E1"].Value = "Major";
                        Sheet.Cells["F1"].Value = "Were you ever on academic probation?";
                        Sheet.Cells["G1"].Value = "Will you be attending AUB during FALL 2022 and SPRING 2023";
                        Sheet.Cells["H1"].Value = "Have you been a part of BEYMUN before? ";
                        Sheet.Cells["I1"].Value = "Why are you interested in this position?";
                        Sheet.Cells["J1"].Value = "Please rate your English writing skills. from 1 to 5";
                        Sheet.Cells["K1"].Value = "What background do you have in working with sponsors/funders?";
                        Sheet.Cells["L1"].Value = "When looking for businesses that are willing to fully sponsor BEYMUN 2018, what are the criteria you would look for in prospective sponsors?";
                        int row = 2;
                        foreach (var item in pers)
                        {

                            Sheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.fullName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.phoneNumber;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.exGrad;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.major;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.academicProbation;
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.attendAUB;
                            Sheet.Cells[string.Format("H{0}", row)].Value = item.partBey;
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.gIntersted;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.gEnglishRate;
                            Sheet.Cells[string.Format("K{0}", row)].Value = item.sBackgroundSponser;
                            Sheet.Cells[string.Format("L{0}", row)].Value = item.sProspectiveSponsors;
                            row++;
                        }


                        Sheet.Cells["A:AZ"].AutoFitColumns();
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment: filename=" + "FundraisingSponsorshipReport.xlsx");
                        Response.BinaryWrite(Ep.GetAsByteArray());
                        Response.End();
                    }
                    else if (Id == 4)
                    {

                        Sheet.Cells["A1"].Value = "Id";
                        Sheet.Cells["B1"].Value = "Full Name";
                        Sheet.Cells["C1"].Value = "Phone Number";
                        Sheet.Cells["D1"].Value = "Ex Grad Date";
                        Sheet.Cells["E1"].Value = "Major";
                        Sheet.Cells["F1"].Value = "Were you ever on academic probation?";
                        Sheet.Cells["G1"].Value = "Will you be attending AUB during FALL 2022 and SPRING 2023";
                        Sheet.Cells["H1"].Value = "Have you been a part of BEYMUN before? ";
                        Sheet.Cells["I1"].Value = "Why are you interested in this position?";
                        Sheet.Cells["J1"].Value = "Please rate your English writing skills. from 1 to 5";
                        Sheet.Cells["K1"].Value = "Name three potential venues for our social events";
                        Sheet.Cells["L1"].Value = "Do you have previous experience in organizing events? If yes, explain what you did.";
                        Sheet.Cells["M1"].Value = "How do you plan on marketing BEYMUN 2018's social events?";
                        int row = 2;
                        foreach (var item in pers)
                        {

                            Sheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.fullName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.phoneNumber;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.exGrad;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.major;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.academicProbation;
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.attendAUB;
                            Sheet.Cells[string.Format("H{0}", row)].Value = item.partBey;
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.gIntersted;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.gEnglishRate;
                            Sheet.Cells[string.Format("K{0}", row)].Value = item.sPotentialVenues;
                            Sheet.Cells[string.Format("L{0}", row)].Value = item.sExperienceOrganizing;
                            Sheet.Cells[string.Format("M{0}", row)].Value = item.sMarketingPlan;
                            row++;
                        }


                        Sheet.Cells["A:AZ"].AutoFitColumns();
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment: filename=" + "FundraisingSocialEventsReport.xlsx");
                        Response.BinaryWrite(Ep.GetAsByteArray());
                        Response.End();
                    }
                    else if (Id == 5)
                    {

                        Sheet.Cells["A1"].Value = "Id";
                        Sheet.Cells["B1"].Value = "Full Name";
                        Sheet.Cells["C1"].Value = "Phone Number";
                        Sheet.Cells["D1"].Value = "Ex Grad Date";
                        Sheet.Cells["E1"].Value = "Major";
                        Sheet.Cells["F1"].Value = "Were you ever on academic probation?";
                        Sheet.Cells["G1"].Value = "Will you be attending AUB during FALL 2022 and SPRING 2023";
                        Sheet.Cells["H1"].Value = "Have you been a part of BEYMUN before? ";
                        Sheet.Cells["I1"].Value = "Why are you interested in this position?";
                        Sheet.Cells["J1"].Value = "Please rate your English writing skills. from 1 to 5";
                        Sheet.Cells["K1"].Value = " Do you have any previous experience in Marketing? Please elaborate. ";
                        
                        int row = 2;
                        foreach (var item in pers)
                        {

                            Sheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.fullName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.phoneNumber;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.exGrad;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.major;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.academicProbation;
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.attendAUB;
                            Sheet.Cells[string.Format("H{0}", row)].Value = item.partBey;
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.gIntersted;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.gEnglishRate;
                            Sheet.Cells[string.Format("K{0}", row)].Value = item.sMarketingPlan;
                            
                            row++;
                        }


                        Sheet.Cells["A:AZ"].AutoFitColumns();
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment: filename=" + "MarketingReport.xlsx");
                        Response.BinaryWrite(Ep.GetAsByteArray());
                        Response.End();
                    }
                    else if (Id == 6)
                    {

                        Sheet.Cells["A1"].Value = "Id";
                        Sheet.Cells["B1"].Value = "Full Name";
                        Sheet.Cells["C1"].Value = "Phone Number";
                        Sheet.Cells["D1"].Value = "Ex Grad Date";
                        Sheet.Cells["E1"].Value = "Major";
                        Sheet.Cells["F1"].Value = "Were you ever on academic probation?";
                        Sheet.Cells["G1"].Value = "Will you be attending AUB during FALL 2022 and SPRING 2023";
                        Sheet.Cells["H1"].Value = "Have you been a part of BEYMUN before? ";
                        Sheet.Cells["I1"].Value = "Why are you interested in this position?";
                        Sheet.Cells["J1"].Value = "Do you have previous experience in Graphic Design? Please elaborate.";
                        
                        int row = 2;
                        foreach (var item in pers)
                        {

                            Sheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.fullName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.phoneNumber;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.exGrad;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.major;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.academicProbation;
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.attendAUB;
                            Sheet.Cells[string.Format("H{0}", row)].Value = item.partBey;
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.gIntersted;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.sExperienceGrapgic;
                            
                            row++;
                        }


                        Sheet.Cells["A:AZ"].AutoFitColumns();
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment: filename=" + "GraphicDesignReport.xlsx");
                        Response.BinaryWrite(Ep.GetAsByteArray());
                        Response.End();
                    }
                    else if (Id == 7)
                    {

                        Sheet.Cells["A1"].Value = "Id";
                        Sheet.Cells["B1"].Value = "Full Name";
                        Sheet.Cells["C1"].Value = "Phone Number";
                        Sheet.Cells["D1"].Value = "Ex Grad Date";
                        Sheet.Cells["E1"].Value = "Major";
                        Sheet.Cells["F1"].Value = "Were you ever on academic probation?";
                        Sheet.Cells["G1"].Value = "Will you be attending AUB during FALL 2022 and SPRING 2023";
                        Sheet.Cells["H1"].Value = "Have you been a part of BEYMUN before? ";
                        Sheet.Cells["I1"].Value = "Why are you interested in this position?";
                        Sheet.Cells["J1"].Value = " Do you have previous experience in organizing events? ";
                        
                        int row = 2;
                        foreach (var item in pers)
                        {

                            Sheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.fullName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.phoneNumber;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.exGrad;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.major;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.academicProbation;
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.attendAUB;
                            Sheet.Cells[string.Format("H{0}", row)].Value = item.partBey;
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.gIntersted;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.sExperienceOrganizing;
                            
                            row++;
                        }


                        Sheet.Cells["A:AZ"].AutoFitColumns();
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment: filename=" + "LogisticsReport.xlsx");
                        Response.BinaryWrite(Ep.GetAsByteArray());
                        Response.End();
                    }
                    else if (Id == 8)
                    {

                        Sheet.Cells["A1"].Value = "Id";
                        Sheet.Cells["B1"].Value = "Full Name";
                        Sheet.Cells["C1"].Value = "Phone Number";
                        Sheet.Cells["D1"].Value = "Ex Grad Date";
                        Sheet.Cells["E1"].Value = "Major";
                        Sheet.Cells["F1"].Value = "Were you ever on academic probation?";
                        Sheet.Cells["G1"].Value = "Will you be attending AUB during FALL 2022 and SPRING 2023";
                        Sheet.Cells["H1"].Value = "Have you been a part of BEYMUN before? ";
                        Sheet.Cells["I1"].Value = "Why are you interested in this position?";
                        Sheet.Cells["J1"].Value = "Do you have any programming knowledge?";
                        Sheet.Cells["K1"].Value = "Do you have previous experience in programming?";
                        
                        int row = 2;
                        foreach (var item in pers)
                        {

                            Sheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.fullName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.phoneNumber;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.exGrad;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.major;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.academicProbation;
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.attendAUB;
                            Sheet.Cells[string.Format("H{0}", row)].Value = item.partBey;
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.gIntersted;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.sProgrammingKnow;
                            Sheet.Cells[string.Format("K{0}", row)].Value = item.sExperienceProgramming;
                            
                            row++;
                        }


                        Sheet.Cells["A:AZ"].AutoFitColumns();
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment: filename=" + "Development(IT).xlsx");
                        Response.BinaryWrite(Ep.GetAsByteArray());
                        Response.End();
                    }
                    else if (Id == 9)
                    {

                        Sheet.Cells["A1"].Value = "Id";
                        Sheet.Cells["B1"].Value = "Full Name";
                        Sheet.Cells["C1"].Value = "Phone Number";
                        Sheet.Cells["D1"].Value = "Ex Grad Date";
                        Sheet.Cells["E1"].Value = "Major";
                        Sheet.Cells["F1"].Value = "Were you ever on academic probation?";
                        Sheet.Cells["G1"].Value = "Will you be attending AUB during FALL 2022 and SPRING 2023";
                        Sheet.Cells["H1"].Value = "Have you been a part of BEYMUN before? ";
                        Sheet.Cells["I1"].Value = "Why are you interested in this position?";
                        Sheet.Cells["J1"].Value = "What protocols do you think are the most important to maintain security during the conference?";
                        
                        int row = 2;
                        foreach (var item in pers)
                        {

                            Sheet.Cells[string.Format("A{0}", row)].Value = item.Id;
                            Sheet.Cells[string.Format("B{0}", row)].Value = item.fullName;
                            Sheet.Cells[string.Format("C{0}", row)].Value = item.phoneNumber;
                            Sheet.Cells[string.Format("D{0}", row)].Value = item.exGrad;
                            Sheet.Cells[string.Format("E{0}", row)].Value = item.major;
                            Sheet.Cells[string.Format("F{0}", row)].Value = item.academicProbation;
                            Sheet.Cells[string.Format("G{0}", row)].Value = item.attendAUB;
                            Sheet.Cells[string.Format("H{0}", row)].Value = item.partBey;
                            Sheet.Cells[string.Format("I{0}", row)].Value = item.gIntersted;
                            Sheet.Cells[string.Format("J{0}", row)].Value = item.sMaintainSecurity;
                            row++;
                        }


                        Sheet.Cells["A:AZ"].AutoFitColumns();
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment: filename=" + "Development(IT).xlsx");
                        Response.BinaryWrite(Ep.GetAsByteArray());
                        Response.End();
                    }
                }
            }
        }
        public ActionResult RelationsNational()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var mem = db.Volanteers.Where(n => n.position == 1).ToArray().ToList();
                return View(mem);
            }
        }
        public ActionResult RelationsInternational()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var mem = db.Volanteers.Where(n => n.position == 2).ToArray().ToList();
                return View(mem);
            }
        }
        public ActionResult Sponsorship()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var mem = db.Volanteers.Where(n => n.position == 3).ToArray().ToList();
                return View(mem);
            }
        }
        public ActionResult SocialEvents()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var mem = db.Volanteers.Where(n => n.position == 4).ToArray().ToList();
                return View(mem);
            }
        }
        public ActionResult Marketing()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var mem = db.Volanteers.Where(n => n.position == 5).ToArray().ToList();
                return View(mem);
            }
        }
        public ActionResult GraphicDesign()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var mem = db.Volanteers.Where(n => n.position == 6).ToArray().ToList();
                return View(mem);
            }
        }
        public ActionResult Logistics()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var mem = db.Volanteers.Where(n => n.position == 7).ToArray().ToList();
                return View(mem);
            }
        }
        public ActionResult Development()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var mem = db.Volanteers.Where(n => n.position == 8).ToArray().ToList();
                return View(mem);
            }
        }
        public ActionResult Safety()
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var mem = db.Volanteers.Where(n => n.position == 9).ToArray().ToList();
                return View(mem);
            }
        }
        public ActionResult DownloadAllVol(int? Id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Id != null)
                {
                    try
                    {
                        var vol = db.Volanteers.Where(n => n.position == Id).ToList();
                        if (vol != null)
                        {
                            foreach (var item in vol)
                            {
                                db.Volanteers.Remove(item);
                            }
                            db.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    switch (Id)
                    {
                        case 1:
                            return RedirectToAction("RelationsNational");
                        case 2:
                            return RedirectToAction("RelationsInternational");
                        case 3:
                            return RedirectToAction("Sponsorship");
                        case 4:
                            return RedirectToAction("SocialEvents");
                        case 5:
                            return RedirectToAction("Marketing");
                        case 6:
                            return RedirectToAction("GraphicDesign");
                        case 7:
                            return RedirectToAction("Logistics");
                        case 8:
                            return RedirectToAction("Development");
                        case 9:
                            return RedirectToAction("Safety");
                            
                        default:
                            return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult DelVol(int? Id)
        {
            if (Session["Email"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Id != null)
                {
                    int? pId = null;
                    try
                    {
                        
                        var vol = db.Volanteers.Where(n => n.Id == Id).FirstOrDefault();
                        if (vol != null)
                        {
                            pId = vol.position;
                            db.Volanteers.Remove(vol);
                            db.SaveChanges();
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    switch (pId)
                    {
                        case 1:
                            return RedirectToAction("RelationsNational");
                        case 2:
                            return RedirectToAction("RelationsInternational");
                        case 3:
                            return RedirectToAction("Sponsorship");
                        case 4:
                            return RedirectToAction("SocialEvents");
                        case 5:
                            return RedirectToAction("Marketing");
                        case 6:
                            return RedirectToAction("GraphicDesign");
                        case 7:
                            return RedirectToAction("Logistics");
                        case 8:
                            return RedirectToAction("Development");
                        case 9:
                            return RedirectToAction("Safety");

                        default:
                            return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }
    }
}