using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab5.Data;
using Lab5.Models;
using Azure.Storage.Blobs;

namespace Lab5.Pages.AnswerImages
{
    public class CreateModel : PageModel
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string earthContainerName = "earthimages";
        private readonly string computerContainerName = "computerimages";
        private readonly Lab5.Data.AnswerImageDataContext _context;

        public CreateModel(Lab5.Data.AnswerImageDataContext context, BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AnswerImage AnswerImage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            var stream = System.IO.File.Create(null);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (file.Length > 0) {
                var filePath = Path.GetRandomFileName();
                using (stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
            }

            if (AnswerImage.Question.GetType().ToString() == "Earth")
            {
                try
                {
                    await _blobServiceClient.CreateBlobContainerAsync(earthContainerName);
                    BlobContainerClient.UploadBlob(earthContainerName, stream);
                   
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }
            }
            else if (AnswerImage.Question.GetType().ToString() == "Computer") 
            {
                try
                {
                    await _blobServiceClient.CreateBlobContainerAsync(computerContainerName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            
            _context.AnswerImages.Add(AnswerImage);
            await _context.SaveChangesAsync();
            BlobContainerClient.UploadBlob(AnswerImage.GetBlob);
            return RedirectToPage("./Index");
        }
    }
}
