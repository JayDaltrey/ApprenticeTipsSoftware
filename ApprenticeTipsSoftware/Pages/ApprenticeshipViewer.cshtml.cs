using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprenticeTipsSoftware.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;

namespace ApprenticeTipsSoftware.Pages
{
    public class ApprenticeshipViewerModel : PageModel
    {

        public List<ApprenticeshipModel> Apprenticeships { get; set; }

        public string FormResultMessage { get; set; }
        public string FilterOptions { get; set; }

        [BindProperty]
        public string SelectedRoute { get; set; }

        [BindProperty]
        public string SelectedStatus { get; set; }

        [BindProperty]
        public string SelectedLevel { get; set; }

        [BindProperty]
        public string SelectedDuration { get; set; }


        public IActionResult OnPost()
        {
            var request = new ApprenticeshipFinderRequest();

            request.Route = SelectedRoute;
            request.Status = SelectedStatus;
            request.Level = SelectedLevel;
            request.Duration = SelectedDuration;

            request.BoolRoute = request.Route == "any" ? false : true;
            request.BoolStatus = request.Status == "any" ? false : true;
            request.BoolLevel = request.Level == "any" ? false : true;
            request.BoolDuration = request.Duration == "any" ? false : true;

            if(request.Duration == "any")
            {
                FilterOptions = $"Showing results for apprenticeships with a route of '{SelectedRoute}', a status of '{SelectedStatus}', a level of '{SelectedLevel}', and a duration of '{SelectedDuration}'";
            }
            else
            {
                FilterOptions = $"Showing results for apprenticeships with a route of '{SelectedRoute}', a status of '{SelectedStatus}', a level of '{SelectedLevel}', and a duration of '{SelectedDuration}' months";
            }

            Apprenticeships = GetApprenticeships(request);

            if (Apprenticeships.Count == 0)
            {
                FormResultMessage = "No results found, please try a different search";

                return Page();
            }

            return Page();

        }

        public IActionResult OnPostGoBack()
        {
            return RedirectToPage("/Index");
        }

        private static List<ApprenticeshipModel> GetApprenticeships(ApprenticeshipFinderRequest appRequest)
        {
            var appResponse = new ApprenticeshipFinderResponse();

            appResponse.Apprenticeships = new List<ApprenticeshipModel>();

            var client = new RestClient("https://localhost:44364/api/Apprenticeship/GetApprenticeships");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("ApiKey", "Th4ZbP42RkOnrT47AqEt");
            request.AddParameter("application/json", ParameterType.RequestBody);
            string json = JsonConvert.SerializeObject(appRequest);
            request.AddParameter("application/json",
                  json +
                 "\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            appResponse = JsonConvert.DeserializeObject<ApprenticeshipFinderResponse>(response.Content);
            Console.WriteLine(response.Content);

            return appResponse.Apprenticeships;
        }

    }
}
