using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Project.Domain;
using TechnicalTest.Project.Stores;

namespace TechnicalTest.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModalityController : ControllerBase
    {
        GenericStore store = new GenericStore();
        [Route("get_all_modality")]
        [HttpGet]
        public async Task<IEnumerable<Modality>> GetAllModality()
        {
            var modList = store.ReadAll<object>();
            var newList = new List<Modality>();
            foreach (var mod in modList)
            {
                string[] msplit = mod.ToString().Split(",");
                var newstr1 = msplit[0].Split(":");
                var newstr2 = msplit[1].Split(":");
                var newstr3 = msplit[2].Split(":");
                var detail = newstr1[1];
                var name = newstr2[1].Replace("\"", "");
                var type = newstr3[1];


                if(type.Contains("PaymentModality"))
                {
                    var newMod = new PaymentModality();
                    newMod.Name = name;
                    newMod.Type = "PaymentModality";
                    newMod.Detail = detail.Replace(" ", "");
                    newMod.Price = detail.Replace(" ", "");
                    newList.Add(newMod);
                }
                else
                {
                    var newMod = new TreatmentModality();
                    newMod.Name = name;
                    newMod.Type = "TreatmentModality";
                    newMod.Detail = detail.Replace("\"", "");
                    newMod.Description = detail.Replace("\"", "");
                    newList.Add(newMod);
                }

            }

            return newList;
        }

    }
}
