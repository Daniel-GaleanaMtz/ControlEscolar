using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace PL_MVC.Controllers
{
    public class MateriaController : Controller
    {
        //
        // GET: /Materia/
        public ActionResult GetAll()
        {
            ML.Result resultMateriaAPI = new ML.Result();
            resultMateriaAPI.Objects = new List<Object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1040/");

                var responseTask = client.GetAsync("api/materia");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Materia resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                        resultMateriaAPI.Objects.Add(resultItemList);
                    }
                }
            }

            ML.Materia materia = new ML.Materia();
            materia.Materias = resultMateriaAPI.Objects;
            return View(materia);
        }

        [HttpGet]
        public ActionResult Delete(int IdMateria)
        {
            ML.Result resultMateriaList = new ML.Result();
            using (var materia = new HttpClient())
            {
                materia.BaseAddress = new Uri("http://localhost:1040/");
                var postTask = materia.DeleteAsync("api/materia/" + IdMateria);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Form(int? IdMateria) 
        {
            ML.Materia materia = new ML.Materia();
            materia.IdMateria = IdMateria.GetValueOrDefault();

            if (IdMateria == null)//Add
            {
                return View(materia);
            }
            else
            {
                ML.Result result = GetByIdAPI(IdMateria);

                if (result.Correct)
                {
                    materia.IdMateria = ((ML.Materia)result.Object).IdMateria;
                    materia.Nombre = ((ML.Materia)result.Object).Nombre;
                    materia.Costo = ((ML.Materia)result.Object).Costo;
                    return View(materia);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            var id = materia.IdMateria;
            ML.Result result = new ML.Result();

            if (materia.IdMateria == 0)//Add
            {
                using (var materiaItem = new HttpClient())
                {
                    materiaItem.BaseAddress = new Uri("http://localhost:1040/");

                    var postTask = materiaItem.PostAsJsonAsync<ML.Materia>("api/materia/", materia);
                    postTask.Wait();

                    var resultPost = postTask.Result;
                    if (resultPost.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "La materia se agregó correctamente ";
                        return PartialView("Modal");
                    }
                }

            }
            else
            {
                using (var materiaItem = new HttpClient())
                {
                    materiaItem.BaseAddress = new Uri("http://localhost:1040/");

                    var postTask = materiaItem.PutAsJsonAsync<ML.Materia>("api/materia/" + id, materia);
                    postTask.Wait();

                    var resultPost = postTask.Result;
                    if (resultPost.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "La materia se actualizó correctamente ";
                        return PartialView("Modal");
                    }
                }

            }

            //if (!result.Correct)
            //{
            //    ViewBag.Message = "No se pudo agregar correctamente el departamento " + result.ErrorMessage;
            //}

            return PartialView("Modal");
        }


        public static ML.Result GetByIdAPI(int? IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                string urlAPI = System.Configuration.ConfigurationManager.AppSettings["http://localhost:1040/"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:1040/");
                    var responseTask = client.GetAsync("api/materia/" + IdMateria);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;

                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Materia resultItemList = new ML.Materia();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Materia";
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }
	}
}