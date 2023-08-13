using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using FIT5032_FinalProject2.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_FinalProject2.Controllers
{
    public class EvaluationsController : Controller
    {
        private EvaluationModel db = new EvaluationModel();

        // GET: Evaluations
        public ActionResult Index()
        {
            return View(db.Evaluations.ToList());
        }

        // GET: Evaluations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluations.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        // GET: Evaluations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evaluations/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Recovery,Waiting,satisfaction,UserID")] Evaluation evaluation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Evaluations.Add(evaluation);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(evaluation);
        //}
        [HttpPost]
        public String CreateEvaluation()
        {

            var evaluation = new Evaluation
            {
                Recovery = int.Parse(Request.Form["Recovery"]),
                Waiting = int.Parse(Request.Form["Waiting"]),
                satisfaction = int.Parse(Request.Form["satisfaction"]),
            };

            evaluation.UserID = User.Identity.GetUserId();
            ModelState.Clear();
            TryValidateModel(evaluation);


            var vx = Request.Files["Attachment"].ContentLength;

            // Store the attachment in local storage.
            var Str1 = Request.Files[0].FileName.Split('.');
            var FileType = Str1[Str1.Length - 1];
            var FilePath =
                Server.MapPath("~/Uploads/") +
                string.Format(@"{0}", Guid.NewGuid()) +
                "." + FileType;
            Request.Files[0].SaveAs(FilePath);
            
            
            
            if (ModelState.IsValid)
            {
                // Add the appointment into the database.
                db.Evaluations.Add(evaluation);
                db.SaveChanges();

                // Send confirmation email.
                var mail = new MailMessage();
                mail.To.Add(new MailAddress(Request.Form["EmailAddress"]));
                mail.From = new MailAddress("trevor18902742928@outlook.com");

                mail.Subject = "Evaluation Conformation";
                mail.Body =
                    "You made an evaluation:\n" +
                    "Recovery: " + Request.Form["Recovery"] + "\n" +
                    "Waiting: " + Request.Form["Waiting"] + "\n" +
                    "satisfaction: " + Request.Form["satisfaction"];
                mail.IsBodyHtml = false;

                var attachment = new System.Net.Mail.Attachment(FilePath);
                mail.Attachments.Add(attachment);

                var smtp = new SmtpClient();
                smtp.Host = "smtp.office365.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential
                    ("trevor18902742928@outlook.com", "*E1u5A_%8?U=xZ");

                smtp.Send(mail);



                return "Success";
            }

            return "Database Unavailable.";
        }




















// GET: Evaluations/Edit/5
public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluations.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        // POST: Evaluations/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Recovery,Waiting,satisfaction,UserID")] Evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evaluation);
        }

        // GET: Evaluations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluations.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        // POST: Evaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evaluation evaluation = db.Evaluations.Find(id);
            db.Evaluations.Remove(evaluation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
