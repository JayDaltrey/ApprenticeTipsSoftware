using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprenticeTipsSoftware.Classes;
using ApprenticeTipsSoftware.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            var request = new ApprenticeshipFinder();
            var selectedFilter = new FilterModel();
            var retriever = new DatabaseRetriever();

            request.Route = SelectedRoute;
            request.Status = SelectedStatus;
            request.Level = SelectedLevel;
            request.Duration = SelectedDuration;

            selectedFilter.BoolRoute = request.Route == "any" ? false : true;
            selectedFilter.BoolStatus = request.Status == "any" ? false : true;
            selectedFilter.BoolLevel = request.Level == "any" ? false : true;
            selectedFilter.BoolDuration = request.Duration == "any" ? false : true;

            if(request.Duration == "any")
            {
                FilterOptions = $"Showing results for apprenticeships with a route of '{SelectedRoute}', a status of '{SelectedStatus}', a level of '{SelectedLevel}', and a duration of '{SelectedDuration}'";
            }
            else
            {
                FilterOptions = $"Showing results for apprenticeships with a route of '{SelectedRoute}', a status of '{SelectedStatus}', a level of '{SelectedLevel}', and a duration of '{SelectedDuration}' months";
            }

            Apprenticeships = retriever.GetApprenticeships(request, selectedFilter);

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

    }
}
