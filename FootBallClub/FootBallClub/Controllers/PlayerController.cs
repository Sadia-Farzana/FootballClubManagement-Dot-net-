using FootBallClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootBallClub.Controllers
{
    public class PlayerController : Controller
    {
        ClubEntities club = new ClubEntities();
        public ActionResult Player()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PlayerListEdit(string id)
        {

            var result = from item in club.Players
                         where item.UserName == id
                         select item;
            Player personToUpdate = result.FirstOrDefault();
            return View(personToUpdate);
        }

        [HttpPost]
        public ActionResult PlayerListEdit(Player player, string id)
        {
            var result = from item in club.Players
                         where item.UserName == id
                         select item;
            Player personToUpdate = result.FirstOrDefault();
            personToUpdate.Name = player.Name;
            personToUpdate.Phone = player.Phone;
            personToUpdate.Address = player.Address;
            personToUpdate.Age = player.Age;
            personToUpdate.Position = player.Position;
            personToUpdate.Email = player.Email;
            personToUpdate.Password = player.Password;
            club.SaveChanges();
            SignUp sign = new SignUp();
            var s = from item in club.SignUps
                         where item.UserName == id
                         select item;
            sign = s.FirstOrDefault();
            sign.Name = player.Name;
            
            sign.Email = player.Email;
            sign.Password = player.Password;
            club.SaveChanges();
            return RedirectToAction("PlayerList", "Home");
        }
    }
}