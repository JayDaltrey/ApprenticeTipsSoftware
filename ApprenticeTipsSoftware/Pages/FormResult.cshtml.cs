using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApprenticeTipsSoftware.Pages
{
    public class FormResultModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FirstName { get; set; }
    }
}
