using GalacticTourAgency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GalacticTourAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalacticToursController : ControllerBase
    {
        private static List<GalacticTour> _galacticTours = new List<GalacticTour>()
        {
            new GalacticTour { Id = 1, PlanetName = "Mars", Duration = "3 days", Price = 1000 },
            new GalacticTour { Id = 2, PlanetName = "Venus", Duration = "5 days", Price = 2000 },
            new GalacticTour { Id = 3, PlanetName = "Jupiter", Duration = "7 days", Price = 3000 }
        };
        [HttpGet]
        public IEnumerable<GalacticTour> GetAll()
        {
            return _galacticTours;
        }
        //IActionResult, farklı türlerde dönüş yapabilmek için kullanılırken, ActionResult<T> belirli bir dönüş türü gerektiren senaryolarda kullanılır.
        //Eğer veri döndürülüyorsa crud işlemlerinde ActionResult<T> kullanılır.
        //Yalnızca durum kodu döndürülüyorsa IActionResult kullanılır.
        //Aşağıda ActionResult kullandık çünkü döndüreceğim veri türü belirli bir türdür.

        [HttpGet("{id:int:min(1)}")]
        public ActionResult<GalacticTour> GetTour(int id)
        {
            var tour = _galacticTours.FirstOrDefault(x => x.Id == id);
            if (tour == null)
            {
                return NotFound($"Tur Id {id} bulunamadı");
            }
            return Ok(tour);
        }
        [HttpGet("planet/{planetName:alpha}")]
        public ActionResult<IEnumerable<GalacticTour>> GetToursByPlanetName(string planetName)
        {
            var planetTours = _galacticTours.Where(x => x.PlanetName.Equals(planetName, StringComparison.OrdinalIgnoreCase));
            if (!planetTours.Any())
            {
                return NotFound($"{planetName} gezegenine ait tur bulunamadı");
            }
            return Ok(planetTours);
        }
        [HttpGet("price-range")]
        public ActionResult<IEnumerable<GalacticTour>> GetToursByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var priceRangeTours = _galacticTours.Where(x => x.Price >= minPrice && x.Price <= maxPrice);
            if (!priceRangeTours.Any())
            {
                return NotFound($"Belirtilen fiyat aralığında tur bulunamadı");
            }
            return Ok(priceRangeTours);
        }
        [HttpPost]
        public ActionResult<GalacticTour> Create([FromBody] GalacticTour tour)
        {
            var id = _galacticTours.Max(x => x.Id) + 1;
            tour.Id = id;
            _galacticTours.Add(tour);
            return CreatedAtAction(nameof(GetTour), new { id = tour.Id }, tour);

        }
        [HttpPost("create-package")]
        public ActionResult<IEnumerable<GalacticTour>> CreateTourPackage([FromBody] GalacticTourPackage tourPackage)
        {
            var tour = new GalacticTour
            {
                Id = _galacticTours.Max(x => x.Id) + 1,
                PlanetName = tourPackage.Destination,
                Duration = $"{tourPackage.DurationInDays} days",
                Price = tourPackage.BasePrice * tourPackage.DurationInDays
            };
            _galacticTours.Add(tour);
            return CreatedAtAction(nameof(GetTour), new { id = tour.Id }, tour);
        }
        [HttpPut("update/{id:min(1)}/{newPlanetName}")]
        public IActionResult UpdateTourPlanet(int id, string newPlanetName)
        {
            var tour = _galacticTours.FirstOrDefault(x => x.Id == id);
            if (tour == null)
            {
                return NotFound($"Tur Id {id} bulunamadı");
            }
            tour.PlanetName = newPlanetName;
            return NoContent();
        }

        //İki farklı route tanımı yaptım biri id ile siliyor biri tur adı ile siliyor 

        [HttpDelete("{id:min(1)}")]
        [HttpDelete("cancel/{tourName}")]
        public IActionResult CancelTour(int? id, string tourName)
        {
            GalacticTour tourToRemove;
            if (id.HasValue)
            {
                tourToRemove = _galacticTours.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                tourToRemove = _galacticTours.FirstOrDefault(x => x.PlanetName.Equals(tourName, StringComparison.OrdinalIgnoreCase));
            }
            if (tourToRemove == null)
            {
                return NotFound($"Tur bulunamadı");
            }
            _galacticTours.Remove(tourToRemove);
            return NoContent();

        }
        [HttpPatch("reschedule/{id:min(1)}/{newDate:datetime}/")]
        public IActionResult RescheduleTour(int id, DateTime newDate, JsonPatchDocument<GalacticTour> patchDocument)
        {
            var tour = _galacticTours.FirstOrDefault(x => x.Id == id);
            if (tour == null)
            {
                return NotFound($"Tur Id {id} bulunamadı");
            }
            tour.DepartureDate = newDate;
            patchDocument.ApplyTo(tour);
            return NoContent();
        }
            [HttpPost("complex-search")]
            public ActionResult<IEnumerable<GalacticTour>> ComplexSearch([FromQuery] string name, [FromQuery] decimal? minPrice,
                [FromHeader(Name = "X-Planet")] string planet, [FromBody] SearchCriteria searchCriteria)
            {
                // if (!string.IsNullOrWhiteSpace(name))
                //        if(minPrice.HasValue)
                /**
                 * POST /api/GalacticTours/complex-search?name=Venus&minPrice=2000
                 * Headers: X-Planet: Venus
                 * Body:
                 * { 
                 *          "departureDate": "2022-12-01",
                 *          "duration": "5 days"
                 * }
                 */
                return Ok(Enumerable.Empty<GalacticTour>());
            }
        }
    }
