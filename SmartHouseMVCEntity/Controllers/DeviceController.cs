using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartHouseDll;
using SmartHouseMVCEntity.Models;

namespace SmartHouseMVCEntity.Controllers
{
    public class DeviceController : Controller
    {
        private House house;
        private string view;
        private HouseContext db = new HouseContext();

        public ActionResult Command(string room, string device, string command)
        {
            ReadHouse();

            if (command.Contains("_plus"))
                command = command.Replace("_plus", "+");

            house.Remote.Reader.RoomData = room;
            house.Remote.Reader.DeviceData = device;
            house.Remote.Reader.CommandData = command;

            house.Remote.PushButton();

            Session["home"] = house;
            SaveHouse();
            GetView();

            return View(view, house);
        }
        public ActionResult LigthSelect(string room, string device, string command)
        {
            ReadHouse();

            ILight light = house.GetDevice(room, device) as ILight;
            if (light != null)
            {
                light.Brightness = (LightsState)Enum.Parse(typeof(LightsState), command);
                
            }

            house.Remote.PushButton();

            Session["home"] = house;
            SaveHouse();
            GetView();
            return View(view, house);
        }
 
        
        private void GetView()
        {
            if (Request.Cookies.Get("view") == null)
            {
                view = "~/Views/Home/Index.cshtml";
            }
            else
            {
                view = Request.Cookies.Get("view").Value;
            }
        }

        private void ReadHouse()
        {
            house = db.Houses.FirstOrDefault();
            if (house != null)
            {
                if (house.Remote == null)
                {
                    house.Remote = new RemoteControl(new WebReader(house), null);
                }
            }
        }
        private void SaveHouse()
        {
            if (house != null)
            {
                db.SaveChanges();
            }
        }

	}
}