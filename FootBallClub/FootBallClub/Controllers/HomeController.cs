using FootBallClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootBallClub.Controllers
{
    public class HomeController : Controller
    {
        ClubEntities club = new ClubEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
            SignUp[] signups = club.SignUps.ToArray();
            return View();

        }
        [HttpPost]
        public ActionResult Signup(SignUp sign)
        {
            //Signup s = new Signup();
            club.SignUps.Add(sign);
            club.SaveChanges();
            var test = club.SignUps.Where(x => x.Type == sign.Type).FirstOrDefault();
            if (test.Type == "Player")
            {
                Player p = new Player();
                p.UserName = sign.UserName;
                p.Name = sign.Name;
                p.Password = sign.Password;
                p.Salary = sign.Salary;
                p.Email = sign.Email;
                club.Players.Add(p);
                club.SaveChanges();
                return RedirectToAction("PlayerList");
            }
            else
            {
                Coach c = new Coach();
                c.UserName = sign.UserName;
                c.Name = sign.Name;
                c.Password = sign.Password;
                c.Salary = sign.Salary;
                c.Email = sign.Email;
              
                club.Coachs.Add(c);
                club.SaveChanges();
                return RedirectToAction("CoachList");



            }

        }


        public ActionResult UserList()
        {
            return View(club.SignUps.ToList());

        }

        public ActionResult PlayerList()
        {
            return View(club.Players.ToList()); ;

        }

        public ActionResult CoachList()
        {
            return View(club.Coachs.ToList()); ;

        }

     
        [HttpGet]
        public ActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Login(SignUp log)
        {
            var logins = club.SignUps.Where(x => x.UserName == log.UserName && x.Password == log.Password).FirstOrDefault();
            if (logins != null)
            {
                if (logins.Type == "Player")
                {
                    
                    Session["Name"] = log.Name;
                    Session["UserName"] = log.UserName;
                    return RedirectToAction("Player","Player");
                }
                else if (logins.Type == "Coach")
                {
                   
                    Session["Name"] = log.Name;
                    Session["UserName"] = log.UserName;

                    return RedirectToAction("Coach","Coach");
                }
                else
                {
                    Session["Name"] = log.Name;
                    Session["UserName"] = log.UserName;

                    return View("Admin","Admin");
                }


            }
            else
            {
                log.ErrorMessage = "Username or Password is incorrect";
                return View("Login", log);

            }

        }
    

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }
    }
}