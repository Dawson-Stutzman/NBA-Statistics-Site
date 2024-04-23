using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CIS560FinalProject.Pages.Analytics
{
    public class PlayerByAttributeFormModel : PageModel
    {
        public List<string> Dummy;
        public string ChosenAttribute;
        public void OnGet()
        {
            Dummy = new();
            Dummy.Add("Dummy1");
            Dummy.Add("Dummy2");
            ChosenAttribute = HttpContext.Request.Query["metric"].ToString();
            if (ChosenAttribute == "") { ChosenAttribute = "Metric"; }
            if (ChosenAttribute == "BirthDate") { ChosenAttribute = "Birth Date"; }

        }
    }
}
