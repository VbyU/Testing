using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Testing.Models;

namespace Testing.Controllers
{
    public class TestingController : ApiController
    {

        [AllowAnonymous]
        [HttpPost]
        [Route("api/testing")]
        public IHttpActionResult testing()
        {
            dbConnection db = new dbConnection();
            try
            {
                string[] result = db.listmovie();
                if (result[0] == "0")
                {
                    string[] arr = Regex.Split(result[1], "\r\n?|\n");
                    cinema[] data = new cinema[arr.Length];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string value = arr[i];
                        string[] temp = value.Split('|');
                        data[i] = new cinema();
                        data[i].title = temp[0];
                        data[i].genre = temp[1];
                        data[i].duration = temp[2];
                        data[i].director = temp[3];


                    }
                    return Json(data);
                }
                else
                {
                    string error = result[1];
                    return Json(error);
                }
            }catch(Exception e)
            {
                return Json(e.Message);
            }
            
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/testing2")]
        public IHttpActionResult testing2()
        {
            cinema[] data = new cinema[2];
            data[0] = new cinema();
            data[0].title = "testing";
            data[0].genre = "testing2";
            data[0].duration = "testing3";
            data[0].director = "testing4";
            data[1] = new cinema();
            data[2].title = "testing";
            data[3].genre = "testing2";
            data[4].duration = "testing3";
            data[5].director = "testing4";
            return Json(data);
        }
    }
}