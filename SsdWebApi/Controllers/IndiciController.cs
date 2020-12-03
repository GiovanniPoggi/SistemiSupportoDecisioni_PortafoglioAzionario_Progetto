using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SsdWebApi.Models;

namespace SsdWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndiciController : ControllerBase
    {
        private readonly IndiciContext _context;
        Persistence P;

        public IndiciController(IndiciContext context)
        { 
            _context = context;
            P = new Persistence(context);
        }

        //risponde a richiesta di tipo GET
        [HttpGet]
        public ActionResult<List<Indice>> GetAll() => 
            _context.indici.ToList();
        
        // GET by ID action: es. https://localhost:5001/api/indici/3
        [HttpGet("{id}", Name = "GetSerie")]
        public string GetSerie(int id)
        {
            string res = "{";
            if (id>8) id=8;
            string[] indices = new string[]{"id", "Data", "SP_500", "FTSE_MIB", "GOLD_SPOT", "MSCI_EM", "MSCI_EURO", "All_Bonds", "US_Treasury"};
            string attribute = indices[id];

            Forecast F = new Forecast();
            res += F.forecastSARIMAindex(attribute);
            res += "}";

            var index = P.readIndex(attribute);
            return res; //index.GetRange(0,10)
        }
    }
}