using System.Collections;
using System.Runtime.CompilerServices;
using TestTask.Data.DTO;
using TestTask.Data.DTO.Request;
using TestTask.Data.DTO.Response;

namespace рroviders.fake.api
{
    public static class WildTicketGenerator 
    {
        static  string[] Cities { get;  } = { "Магадан", "Ереван", "Барнаул", "Монте-Карло", "Чертовицы", "Москва", "Шир", "Манхэттен", "Централ", "Луна", "Марс", "Питер" };
       
        public static ProviderOneSearchResponse Generate(ProviderOneSearchRequest request)
        {

            return new ProviderOneSearchResponse
            {
                Routes = Enumerable.Range(0, Random.Shared.Next(0, 5)).
                 Select(_ => new ProviderOneRoute
                 {
                     DateFrom = request.DateFrom.AddDays(Random.Shared.Next(0, 5)),
                     DateTo = (request.DateTo ?? request.DateFrom.AddDays(5)).AddDays(Random.Shared.Next(0, 5)),
                     Price = Random.Shared.Next(0, 10),
                     From = Cities[Random.Shared.Next(0, Cities.Length - 1)],
                     To = Cities[Random.Shared.Next(0, Cities.Length - 1)]
                 }).ToArray()
            };
        }


        public static ProviderTwoSearchResponse Generate(ProviderTwoSearchRequest request)
        {
            return new ProviderTwoSearchResponse
            {
                Routes = Enumerable.Range(0, Random.Shared.Next(0, 5)).
                Select(_ => new ProviderTwoRoute
                {
                    Arrival = new ProviderTwoPoint()
                    {
                        Date = request.DepartureDate,
                        Point = Cities[Random.Shared.Next(0, Cities.Length - 1)]
                    },
                    Departure = new ProviderTwoPoint()
                    {
                        Date = DateTime.Now.AddDays(Random.Shared.Next(0, 5)),
                        Point = Cities[Random.Shared.Next(0, Cities.Length - 1)]
                    },
                    Price = Random.Shared.Next(0, 10), // rubbles only, 0 - for free,  10 - too expensive (not for free)

                }).ToArray()
            };
        }

    }
}

namespace TestTask.Providers.One { }
