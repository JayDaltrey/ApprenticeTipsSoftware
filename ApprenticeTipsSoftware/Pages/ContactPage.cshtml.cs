using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprenticeTipsSoftware.Models;
using ApprenticeTipsSoftware.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;

namespace ApprenticeTipsSoftware.Pages
{
    public class ContactPageModel : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Email address is required"), MinLength(10, ErrorMessage = "Email address should contain at least 10 characters"), MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9.-]+[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email format")]
        public string EmailAddress { get; set; }

        [BindProperty, MinLength(10, ErrorMessage = "The message box should contain at least 10 characters"), MaxLength(300)]
        public string Comment { get; set; }

        [BindProperty, Required(ErrorMessage = "First name is required"), MinLength(3, ErrorMessage = "First name should contain at least 3 characters"), MaxLength(30)]
        public string FirstName { get; set; }

        [BindProperty, Required(ErrorMessage = "Surname is required"), MinLength(3, ErrorMessage = "Surname should contain at least 3 characters"), MaxLength(30)]
        public string Surname { get; set; }

        [BindProperty, Required(ErrorMessage = "A contact number is required"), MinLength(11, ErrorMessage = "Contact number should contain at least 11 characters"), MaxLength(30)]
        public string ContactNumber { get; set; }

        [BindProperty, Required]
        public string Qualification { get; set; }

        //Checkbox properties

        [BindProperty]
        public bool Agriculture { get; set; }

        //change all these to bools

        [BindProperty]
        public bool Business { get; set; }

        [BindProperty]
        public bool Care { get; set; }

        [BindProperty]
        public bool Catering { get; set; }

        [BindProperty]
        public bool Construction { get; set; }

        [BindProperty]
        public bool Creative { get; set; }

        [BindProperty]
        public bool Digital { get; set; }

        [BindProperty]
        public bool Education { get; set; }

        [BindProperty]
        public bool Engineering { get; set; }

        [BindProperty]
        public bool Hair { get; set; }

        [BindProperty]
        public bool Health { get; set; }

        [BindProperty]
        public bool Legal { get; set; }

        [BindProperty]
        public bool Protective { get; set; }

        [BindProperty]
        public bool Sales { get; set; }

        [BindProperty]
        public bool Transport { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var contact = new ContactModel();
                var request = new DetailAdderRequest();

                contact.EmailAddress = EmailAddress;
                contact.Comment = Comment;
                contact.FirstName = FirstName;
                contact.Surname = Surname;
                contact.ContactNumber = ContactNumber;
                contact.Qualification = Qualification;
                contact.Comment = Comment;
                contact.ContactNumber = ContactNumber;

                //checkbox properties

                contact.Agriculture = Agriculture ? 1 : 0;
                contact.Business = Business ? 1 : 0;
                contact.Care = Care ? 1 : 0;
                contact.Catering = Catering ? 1 : 0;
                contact.Construction = Construction ? 1 : 0;
                contact.Creative = Creative ? 1 : 0; 
                contact.Digital = Digital ? 1 : 0;
                contact.Education = Education ? 1 : 0;
                contact.Engineering = Engineering ? 1 : 0;
                contact.Hair = Hair ? 1 : 0;
                contact.Health = Health ? 1 : 0;
                contact.Legal = Legal ? 1 : 0;
                contact.Protective = Protective ? 1 : 0;
                contact.Sales = Sales ? 1 : 0;
                contact.Transport = Transport ? 1 : 0;

                request.Contact = contact;

                AddContactDetails(request);

                return RedirectToPage("/FormResult", new {FirstName});

            }

            return Page();
        }

        public IActionResult OnPostGoBack()
        {
            return RedirectToPage("/Index");
        }

        private void AddContactDetails(DetailAdderRequest AdderRequest)
        {
            var client = new RestClient("https://localhost:44364/api/Apprenticeship/AddDetailsToDB");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("ApiKey", "Th4ZbP42RkOnrT47AqEt");
            request.AddParameter("application/json", ParameterType.RequestBody);
            string json = JsonConvert.SerializeObject(AdderRequest);
            request.AddParameter("application/json",
                    json +
                   "\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

        }
    }
}


//private void AddContactDetailsToDB(DetailAdderRequest request)
//{
//    var DBinserter = new DatabaseInserter();

//    int currentId = DBinserter.InsertData("email, firstname, surname, phone, previous_level, comments", $"{request.Contact.EmailAddress}', '{request.Contact.FirstName}', '{request.Contact.Surname}', '{request.Contact.ContactNumber}', '{request.Contact.Qualification}', '{request.Contact.Comment}", "Contact", "webform");
//    DBinserter.InsertCheckboxData(request, currentId);

//}