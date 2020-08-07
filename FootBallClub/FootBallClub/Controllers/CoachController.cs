using FootBallClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootBallClub.Controllers
{
    public class CoachController : Controller
    {
        
        public ActionResult Coach()
        {
            return View();
        }
        ClubEntities club = new ClubEntities();

        [HttpGet]
        public ActionResult CoachListEdit(string id)
        {

            var result = from item in club.Coachs
                         where item.UserName == id
                         select item;
            Coach personToUpdate = result.FirstOrDefault();
          
            return View(personToUpdate);
        }

        [HttpPost]
        public ActionResult CoachListEdit(Coach coach, string id)
        {
            var result = from item in club.Coachs
                         where item.UserName == id
                         select item;
            Coach personToUpdate = result.FirstOrDefault();
            personToUpdate.Name = coach.Name;
            personToUpdate.Phone = coach.Phone;
            personToUpdate.Address = coach.Address;
            personToUpdate.Age = coach.Age;
            personToUpdate.Designation = coach.Designation;
            personToUpdate.Email = coach.Email;
            personToUpdate.Password = coach.Password;
            club.SaveChanges();
            SignUp sign = new SignUp();
            var s = from item in club.SignUps
                    where item.UserName == id
                    select item;
            sign = s.FirstOrDefault();
            sign.Name = coach.Name;

            sign.Email = coach.Email;
            sign.Password = coach.Password;
            club.SaveChanges();
            return RedirectToAction("CoachList", "Home");
        }

    }
}