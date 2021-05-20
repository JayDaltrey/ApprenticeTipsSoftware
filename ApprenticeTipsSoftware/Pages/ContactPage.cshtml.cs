using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprenticeTipsSoftware.Classes;
using ApprenticeTipsSoftware.Models;
using ApprenticeTipsSoftware.RequestResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public int Agriculture { get; set; }

        [BindProperty]
        public int Business { get; set; }

        [BindProperty]
        public int Care { get; set; }

        [BindProperty]
        public int Catering { get; set; }

        [BindProperty]
        public int Construction { get; set; }

        [BindProperty]
        public int Creative { get; set; }

        [BindProperty]
        public int Digital { get; set; }

        [BindProperty]
        public int Education { get; set; }

        [BindProperty]
        public int Engineering { get; set; }

        [BindProperty]
        public int Hair { get; set; }

        [BindProperty]
        public int Health { get; set; }

        [BindProperty]
        public int Legal { get; set; }

        [BindProperty]
        public int Protective { get; set; }

        [BindProperty]
        public int Sales { get; set; }

        [BindProperty]
        public int Transport { get; set; }

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

                //checkbox properties

                contact.Agriculture = Agriculture;
                contact.Business = Business;
                contact.Care = Care;
                contact.Catering = Catering;
                contact.Comment = Comment;
                contact.Construction = Construction;
                contact.ContactNumber = ContactNumber;
                contact.Creative = Creative;
                contact.Digital = Digital;
                contact.Education = Education;
                contact.Engineering = Engineering;
                contact.Hair = Hair;
                contact.Health = Health;
                contact.Legal = Legal;
                contact.Protective = Protective;
                contact.Sales = Sales;
                contact.Transport = Transport;

                request.Contact = contact;

                AddContactDetailsToDB(request);

                return RedirectToPage("/FormResult", new {FirstName});

            }

            return Page();
        }

        public IActionResult OnPostGoBack()
        {
            return RedirectToPage("/Index");
        }

        private void AddContactDetailsToDB(DetailAdderRequest request)
        {
            var DBinserter = new DatabaseInserter();

            int currentId = DBinserter.InsertData("email, firstname, surname, phone, previous_level, comments", $"{request.Contact.EmailAddress}', '{request.Contact.FirstName}', '{request.Contact.Surname}', '{request.Contact.ContactNumber}', '{request.Contact.Qualification}', '{request.Contact.Comment}", "Contact", "webform");
            DBinserter.InsertCheckboxData(request, currentId);


        }

    }
}
