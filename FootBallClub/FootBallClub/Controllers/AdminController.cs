using FootBallClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootBallClub.Controllers
{
    public class AdminController : Controller
    {
        ClubEntities club = new ClubEntities();
        // GET: Admin
        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {

            var result = from item in club.SignUps
                         where item.UserName == id
                         select item;
            SignUp personToUpdate = result.FirstOrDefault();
            return View(personToUpdate);
        }

        [HttpPost]
        public ActionResult Edit(SignUp sign, string id)
        {
            var result = from item in club.SignUps
                         where item.UserName == id
                         select item;
            SignUp personToUpdate = result.FirstOrDefault();
            personToUpdate.Name = sign.Name;
            personToUpdate.Salary = sign.Salary;
            personToUpdate.Email = sign.Email;
            personToUpdate.Password = sign.Password;
            club.SaveChanges();
            var test = club.SignUps.Where(x => x.Type == sign.Type).FirstOrDefault();
            if (test.Type == "Player")
            {
                var p = from item in club.Players
                        where item.UserName == id
                        select item;
                Player player = new Player();
                player = p.FirstOrDefault();
                player.Name = sign.Name;
                player.Password = sign.Password;
                player.Email = sign.Email;
                //player.Salary = sign.Salary;
                club.SaveChanges();
            }
            else if (test.Type == "Coach")
            {
                var c = from item in club.Coachs
                        where item.UserName == id
                        select item;
                Coach coach = new Coach();
                coach = c.FirstOrDefault();
                coach.Name = sign.Name;
                coach.Password = sign.Password;
                coach.Email = sign.Email;
                coach.Salary = sign.Salary;
                club.SaveChanges();

            }


            return RedirectToAction("UserList", "Home");

        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            SignUp sign = club.SignUps.Where(x => x.UserName == id).FirstOrDefault();
            return View(sign);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult ConfrimDelete(string id)
        {
            SignUp sign = club.SignUps.Where(x => x.UserName == id).FirstOrDefault();
            club.SignUps.Remove(sign);
            club.SaveChanges();

            return RedirectToAction("UserList", "Home");
        }
    }
}