using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CIS560FinalProject.Pages.Analytics
{
    public class MetricsByCollegeFormModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public MetricsByCollegeFormModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
